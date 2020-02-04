using System ;
using System.Collections.Generic ;
using Autofac.Builder ;
using Autofac.Core ;
using Autofac.Core.Registration ;
using Castle.DynamicProxy ;
using Common.Model ;
using NLog ;

namespace Common.Utils
{
    /// <summary>Interceptor for Autofac container builder. Used for diagnostics.
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for BuilderInterceptor
    public class BuilderInterceptor : IInterceptor
    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Initializes a new instance of the <see cref="BuilderInterceptor"/> class.</summary>
        /// <param name="proxyGenerator">The proxy generator.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public BuilderInterceptor ( ProxyGenerator proxyGenerator )
        {
            _proxyGenerator = proxyGenerator ;
        }

        private ProxyGenerator _proxyGenerator ;

        /// <summary>Gets the invocations.</summary>
        /// <value>The invocations.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Invocations
        public List < MethodInvocation > Invocations { get ; } = new List < MethodInvocation > ( ) ;

        /// <summary>Intercepts the specified invocation.</summary>
        /// <param name="invocation">The invocation.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Intercept
        public void Intercept ( IInvocation invocation )
        {
            var i = new MethodInvocation ( invocation.Method , invocation.Arguments ) ;
            Invocations.Add ( i ) ;
            invocation.Proceed ( ) ;
            i.OriginalReturnValue = invocation.ReturnValue ;
            try
            {
                if ( i.OriginalReturnValue is DeferredCallback cb )
                {
                    var cbAction = cb.Callback ;
                    var ret = CreateCallbackProxy ( _proxyGenerator , cbAction , cb ) ;
                    invocation.ReturnValue = ret ;
                    i.ReturnValue          = ret ;
                    return ;
                }

                var classProxyWithTarget =
                    _proxyGenerator.CreateClassProxyWithTarget (
                                                               invocation.ReturnValue
                                                             , new BuilderInterceptor (
                                                                                       _proxyGenerator
                                                                                      )
                                                              ) ;
                invocation.ReturnValue = classProxyWithTarget ;
            }
            catch ( Exception ex )
            {
                Logger.Warn ( ex , ex.Message ) ;
            }
        }

        private static object CreateCallbackProxy (
            ProxyGenerator                proxyGenerator
          , Action < IComponentRegistry > callback
           ,
            // ReSharper disable once UnusedParameter.Global
            DeferredCallback defer
        )

        {
            var x = proxyGenerator.CreateClassProxy (
                                                     typeof ( DeferredCallback )
                                                   , new object[] { callback }
                                                   , new BuilderInterceptor ( proxyGenerator )
                                                    ) ;
            return x ;
        }
    }
}