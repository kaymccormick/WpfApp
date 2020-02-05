﻿using System.Diagnostics ;
using JetBrains.Annotations ;
using WpfApp.Core.Logging ;
using Xunit.Abstractions ;
using Xunit.Sdk ;

namespace TestLib.Fixtures
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggingFixture
    [UsedImplicitly ]
    public class LoggingFixture
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public LoggingFixture ( IMessageSink sink )
        {
            Sink = sink ;
            AppLoggingConfigHelper.EnsureLoggingConfigured ( LogMethod ) ;
            // Debug.WriteLine("MY LogFactory is o " + NLog.LogManager.LogFactory.ToString());
        }

        private IMessageSink Sink { get ; }

        // ReSharper disable once IdentifierTypo
        private void LogMethod ( string message , string callerfilepath , string callermembername )
        {
            Sink.OnMessage ( new DiagnosticMessage ( message ) ) ;
            Debug.WriteLine ( message ) ;
        }
    }
}