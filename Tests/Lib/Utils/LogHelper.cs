using System.Collections ;
using System.Collections.Generic;
using System.Reflection ;

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
            if ( method.DeclaringType != null )
            {
                r[ "TestClass" ] = method.DeclaringType.ToString ( ) ;
            }

            r[ "TestMethodLifecycle" ] = stage ;
            return r ;
        }
    }
}
