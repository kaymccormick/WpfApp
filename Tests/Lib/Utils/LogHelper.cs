using System;
using System.Collections ;
using System.Collections.Generic;
using System.Collections.Specialized ;
using System.Linq;
using System.Reflection ;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Lib.Utils
{
    public enum TestMethodLifecycle
    {
        Before,
        After
    }

    public static class LogHelper
    {
        public static IDictionary TestMethodProperties (
            MethodInfo          method
          , TestMethodLifecycle stage
        )
        {
            IDictionary r = new Dictionary < string , object > ( ) ;
            r[ "TestMethodName" ]      = method.Name ;
            r[ "TestClass" ]           = method.DeclaringType.ToString ( ) ;
            r[ "TestMethodLifecycle" ] = stage ;
            return r ;
        }
    }
}
