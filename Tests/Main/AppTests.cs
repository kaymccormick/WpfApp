﻿using System ;
using NLog ;
using NLog.Layouts ;
using Tests.Lib.Fixtures ;
using WpfApp.Application ;
using WpfApp.Core.Container ;
using WpfApp.Debug ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main
{
    /// <summary>Tests for primary application class <see cref="App"/>.</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for AppTests
    [ Collection ( "GeneralPurpose" ) ]
    public class AppTests : IClassFixture < LoggingFixture > , IDisposable
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        private static   Logger            Logger = LogManager.GetCurrentClassLogger ( ) ;
        private readonly ITestOutputHelper _output ;
        private readonly LoggingFixture    _loggingFixture ;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AppTests ( ITestOutputHelper output , LoggingFixture loggingFixture )
        {
            _output         = output ;
            _loggingFixture = loggingFixture ;
            loggingFixture.SetOutputHelper ( output ) ;
            _loggingFixture.Layout = Layout.FromString ( "${message}" ) ;
        }

        /// <summary>Tests application of configuration in the app.config file.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestApplyConfiguration
        [ WpfFact ]
        public void TestApplyConfiguration ( )
        {
            using var app = new App ( DebugEventHandler ) ;
            Assert.NotNull ( app ) ;
            Assert.Collection (
                               app.ConfigSettings
                             , o => Assert.IsType < ContainerHelperSettings > ( o )
                              ) ;
        }

       

        private void DebugEventHandler ( object sender , DebugEventArgs e )
        {
            _output.WriteLine ( e.Message ) ;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( )
        {
            // _loggingFixture?.Dispose ( ) ;
            _loggingFixture.SetOutputHelper ( null ) ;
        }
    }
}