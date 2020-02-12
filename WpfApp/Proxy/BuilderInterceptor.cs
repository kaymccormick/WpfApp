using System ;
using System.Collections.Generic ;
using Autofac.Builder ;
using Autofac.Core ;
using Castle.DynamicProxy ;
using NLog ;
using WpfApp.Core.Model ;

namespace WpfApp.Proxy
{
    /// <summary>Interceptor for Autofac container builder. Used for diagnostics.</summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for BuilderInterceptor
    public class BuilderInterceptor : ContainerBaseInterceptor
    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Gets the invocations.</summary>
        /// <value>The invocations.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Invocations
        public List < MethodInvocation > Invocations { get ; } = new List < MethodInvocation > ( ) ;

        /// <summary>Intercepts the specified invocation.</summary>
        /// <param name="invocation">The invocation.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Intercept
        public override void Intercept ( IInvocation invocation )
        {
            if ( invocation != null )
            {
                var i = new MethodInvocation ( invocation.Method , invocation.Arguments ) ;
                Invocations.Add ( i ) ;
                invocation.Proceed ( ) ;
                i.OriginalReturnValue = invocation.ReturnValue ;
                try
                {
                    
                    if ( i.OriginalReturnValue is DeferredCallback cb )
                    {
                        object origArg = null ;
                        if ( invocation.Method.Name == "RegisterCallback" )
                        {
                            origArg = invocation.Arguments[ 0 ] ;
                        }


                        Logger.Info ( $"{invocation.Method.Name} returning deferred callback" ) ;
                        var cbAction = cb.Callback ;
                        // ReSharper disable once ConvertToLocalFunction
                        Action < IComponentRegistry > newCbAction = reg => {
                            var p2 = Generator
                               .CreateInterfaceProxyWithTarget < IComponentRegistry > (
                                                                                       reg
                                                                                     , new
                                                                                           RegistryInterceptor (Generator )
                                                                                      ) ;
                            Logger.Info ( "in callback" ) ;
                            if ( origArg != null )
                            {
                                if ( origArg is Delegate d )
                                {
                                    DumpDelegate(d);
                                    // var method = d.Method ;
                                    // var target = d.Target ;
                                    // Logger.Info (
                                    //              $"{target} ({target.GetType ( )}) {method.Name}"
                                    //             ) ;
                                }
                            }


                            Logger.Info ( $"{origArg}" ) ;
                            cbAction ( p2 ) ;
                        } ;
                        cb.Callback = newCbAction ;
                        // var ret = CreateCallbackProxy ( _proxyGenerator , cbAction , cb ) ;
                        // invocation.ReturnValue = ret ;
                        i.ReturnValue = invocation.ReturnValue ;
                        return ;
                    }
                    
                    var classProxyWithTarget =
                        Generator.CreateClassProxyWithTarget (
                                                                    invocation.ReturnValue
                                                                  , new BuilderInterceptor (
                                                                                            Generator
                                                                                           )
                                                                   ) ;
                    invocation.ReturnValue = classProxyWithTarget ;
                }
                catch ( Exception ex )
                {
                    Logger.Warn ( ex , ex.Message ) ;
                }
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static object CreateCallbackProxy (
            ProxyGenerator                proxyGenerator
          , Action < IComponentRegistry > callback
           ,
            // ReSharper disable once UnusedParameter.Global
            // ReSharper disable once UnusedParameter.Local
            DeferredCallback defer
        )

        {
            Logger.Info ( "Creating deffered callback proxy" ) ;
            var x = proxyGenerator.CreateClassProxy (
                                                     typeof ( DeferredCallback )
                                                   , new ProxyGenerationOptions (
                                                                                 new
                                                                                     CallBackHook ( )
                                                                                )
                                                   , new object[] { callback }
                                                   , new CallbackInterceptor ( proxyGenerator)
                                                    ) ;
            return x ;
        }

        /// <summary>Initializes a new instance of the <see cref="System.Object" /> class.</summary>
        public BuilderInterceptor ( ProxyGenerator generator ) : base ( generator )
        {
        }
    }
}