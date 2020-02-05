﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MyInterceptor.cs
// 
// 2020-01-18-3:40 PM
// 
// ---
#endregion

using System.Collections ;
using System.Diagnostics ;
using System.Linq ;
using Castle.DynamicProxy ;

namespace WpfApp1.Util
{
    public class LoggerInterceptor : IInterceptor
    {
        public void Intercept ( IInvocation invocation )
        {
            var q = invocation.InvocationTarget.ToString ( ) ;
            var s = invocation.InvocationTarget.GetType ( ).ToString ( ) ;
            if ( q != s )
            {
                // ReSharper disable once RedundantAssignment
                q += $" [{s}]" ;
            }

            var args = string.Join (
                                    ", "
                                  , invocation
                                   .Arguments.AsQueryable ( )
                                   .Select (
                                            o => o is ICollection
                                                     ? o.GetType ( ).ToString ( )
                                                     : $"{o} {o.GetType ( )}"
                                           )
                                   ) ;
            Debug.WriteLine ( $"{s}.{invocation.Method.Name} ({args})" ) ;

            invocation.Proceed ( ) ;
        }
    }
}