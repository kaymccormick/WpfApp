﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// Common
// LogFactoryInterceptor.cs
// 
// 2020-01-28-11:01 PM
// 
// ---
#endregion
using Castle.DynamicProxy ;

namespace WpfApp.Core.Logging
{
    public class LogFactoryInterceptor : IInterceptor
    {
        public LogFactoryInterceptor (
            ProxyGenerator              generator
          , LogDelegates.LogMethod useLogMethod
        )
        {
            Generator    = generator ;
            UseLogMethod = useLogMethod ;
        }

        public ProxyGenerator Generator { get ; }

        public LogDelegates.LogMethod UseLogMethod { get ; }

        /// <summary>Intercepts the specified invocation.</summary>
        /// <param name="invocation">The invocation.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Intercept
        public void Intercept ( IInvocation invocation )
        {
            UseLogMethod ( $"Method name is {invocation.Method.Name}" ) ;
            if ( invocation.Method.Name == "GetLogger" )
            {
                invocation.Proceed ( ) ;
                var inter = new LoggerInterceptor (
                                                   UseLogMethod
                                                              ) ;
                var proxy = Generator.CreateClassProxyWithTarget (
                                                                  invocation.ReturnValue
                                                                , inter
                                                                 ) ;
                invocation.ReturnValue = proxy ;
            }
            else
            {
                invocation.Proceed ( ) ;
            }
        }
    }
}