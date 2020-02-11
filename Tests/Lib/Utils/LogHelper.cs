﻿using System.Collections ;
using System.Collections.Generic;
using System.Reflection ;

namespace Tests.Lib.Utils
{
    /// <summary>Indicates the lifecycle state for a test method.</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestMethodLifecycle
    public enum TestMethodLifecycle
    {
        /// <summary>The before</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Before
        Before,
        /// <summary>The after</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for After
        After
    }

    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LogHelper
    public static class LogHelper
    {
        /// <summary>Supplied structured logging properties for a particular test method indicated by <seealso cref="method"/>.. Supplied properties "TestMethodName", "TestClass",</summary>
        /// <param name="method">The method.</param>
        /// <param name="stage">The stage.</param>
        /// <returns></returns>
        /// Return a dictionary with keys
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestMethodProperties
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
