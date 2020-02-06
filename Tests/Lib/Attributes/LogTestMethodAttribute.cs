﻿using System ;
using System.Diagnostics ;
using System.Reflection ;
using NLog ;
using NLog.Fluent ;
using Tests.Lib.Utils ;
using Xunit.Sdk ;

namespace Tests.Lib.Attributes
{
    /// <summary></summary>
    /// <seealso cref="Xunit.Sdk.BeforeAfterTestAttribute" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LogTestMethodAttribute
    public class LogTestMethodAttribute : BeforeAfterTestAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
        /// <summary>
        ///     This method is called after the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void After ( MethodInfo methodUnderTest )
        {
            new LogBuilder ( Logger ).Level ( LogLevel.Info )
                                     .Message (
                                               $"{nameof ( After )} test method {methodUnderTest.Name}"
                                              )
                                     .Properties (
                                                  LogHelper.TestMethodProperties (
                                                                                  methodUnderTest
                                                                                , TestMethodLifecycle
                                                                                     .After
                                                                                 )
                                                 )
                                     .Write ( ) ;
        }

        /// <summary>
        ///     This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void Before ( MethodInfo methodUnderTest )
        {
            new LogBuilder ( Logger ).Level ( LogLevel.Info )
                                     .Message (
                                               $"{nameof ( Before)} test method {methodUnderTest.Name}"
                                              )
                                     .Properties (
                                                  LogHelper.TestMethodProperties (
                                                                                  methodUnderTest
                                                                                , TestMethodLifecycle
                                                                                     .Before
                                                                                 )
                                                 )
                                     .Write ( ) ;
        }
    }
}