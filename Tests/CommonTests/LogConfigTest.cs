﻿using System.Diagnostics ;
using System.Linq ;
using Common.Logging ;
using NLog ;
using NLog.Config ;
using NLog.Targets ;
using TestLib.Attributes ;
using WpfApp.Core.Logging ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LogConfigTest
    [ BeforeAfterLogger ] [ LogTestMethod ]
    public class LogConfigTest
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        /// <param name="output"></param>
        public LogConfigTest ( ITestOutputHelper output )
        {
            _output    = output ;
            _logMethod = LogMethod ;
        }

        /// <summary>The output</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _output
        private readonly ITestOutputHelper _output ;

        /// <summary>The log method</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _logMethod
        private readonly LogDelegates.LogMethod _logMethod ;

        /// <summary>Logging method</summary>
        /// <param name="message">The message.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerMemberName">The caller member name.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LogMethod
        private void LogMethod ( string message )
        {
            _output.WriteLine ( message ) ;
            Debug.WriteLine ( message ) ;
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        /// <summary>Checks the log configuration.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CheckLogConfig
        private void CheckLogConfig ( LoggingConfiguration configuration )
        {
            Assert.NotEmpty ( configuration.AllTargets ) ;
            Assert.NotEmpty (
                             configuration.AllTargets.Select ( target => target is DebugTarget )
                            ) ;
            Assert.NotEmpty (
                             configuration.AllTargets.Select (
                                                              target => target is NLogViewerTarget
                                                             )
                            ) ;
            Assert.NotEmpty (
                             configuration.AllTargets.Select ( target => target is ChainsawTarget )
                            ) ;
            Assert.NotEmpty (
                             configuration.AllTargets.Select ( target => target is MyCacheTarget )
                            ) ;

            // var q =
            // from target in configuration.AllTargets
            // join rule in configuration.LoggingRules on target ;
        }

        /// <summary>Tests the ensure configuration two arguments.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestEnsureConfigTwoArgs
        [ Fact ]
        public void TestEnsureConfigTwoArgs ( )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( _logMethod ) ;
            CheckLogConfig ( LogManager.Configuration ) ;
        }
    }
}