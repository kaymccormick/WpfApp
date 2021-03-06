#region header
// Kay McCormick (mccor)
// 
// WpfApp
// WpfApp
// RegistryInterceptor.cs
// 
// 2020-02-06-9:56 PM
// 
// ---
#endregion
using System ;
using System.Linq ;
using Castle.DynamicProxy ;
using NLog ;

namespace WpfApp.Proxy
{
    /// <summary></summary>
    /// <seealso cref="Castle.DynamicProxy.IInterceptor" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for RegistryInterceptor
    public class RegistryInterceptor : ContainerBaseInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Intercepts the specified invocation.</summary>
        /// <param name="invocation">The invocation.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Intercept
        public override void Intercept ( IInvocation invocation )
        {
            Logger.Info ( invocation.Method.Name ) ;
            for ( var i = 0 ; i < invocation.Arguments.Length ; i ++ )
            {
                var arg = invocation.Arguments[ i ] ;
                if ( arg is Delegate d )
                {
                    DumpDelegate ( d , $"[{i}]" ) ;
                }
                else
                {
                    Logger.Info ( $"[{i}] {NameForType ( arg.GetType ( ) )}" ) ;
                    Logger.Info ( $"[{i}]: = {arg}" ) ;
                }
            }

            if ( invocation.Arguments.Any ( ) )
            {
                // Logger.Warn (
                // string.Join (
                // ", "
                // , invocation.Arguments.Select ( o => o.ToString ( ) )
                // )
                // ) ;
            }

            invocation.Proceed ( ) ;
        }

        /// <summary>Initializes a new instance of the <see cref="System.Object" /> class.</summary>
        public RegistryInterceptor ( ProxyGenerator generator ) : base ( generator )
        {
        }
    }
}