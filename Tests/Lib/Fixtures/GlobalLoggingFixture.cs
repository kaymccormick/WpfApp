﻿#region header
// Kay McCormick (mccor)
// 
// WpfApp
// Tests
// GlobalLoggingFixture.cs
// 
// 2020-02-06-3:12 AM
// 
// ---
#endregion
using System.Diagnostics ;
using System.Threading.Tasks ;
using DynamicData ;
using JetBrains.Annotations ;
using NLog ;
using NLog.Layouts ;
using WpfApp.Core.Logging ;
using Xunit ;
using Xunit.Abstractions ;
using DiagnosticMessage = Xunit.Sdk.DiagnosticMessage ;

namespace Tests.Lib.Fixtures
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggingFixture
    [ UsedImplicitly ]
    public class GlobalLoggingFixture : IAsyncLifetime
    {
        private                 XunitSinkTarget _xunitSinkTarget ;
        private static readonly Logger          Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public GlobalLoggingFixture ( IMessageSink sink )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured (
                                                            message => sink.OnMessage (
                                                                                       new Xunit.
                                                                                           DiagnosticMessage (
                                                                                                              message
                                                                                                             )
                                                                                      )
                                                           ) ;
            Sink = sink ;

            var l = AppLoggingConfigHelper.SetupJsonLayout ( ) ;

            sink.OnMessage (
                            new Xunit.DiagnosticMessage ( "Constructing GlobalLoggingFixture." )
                           ) ;
            _xunitSinkTarget = new XunitSinkTarget ( "Xunitsink" )
                               {
                                   Sink = sink
                                   // , Layout = Layout.FromString (
                                   // "${longdate}|${level:uppercase=true}|${logger}|${message}"
                                   //
                                 , Layout = l
                               } ;
            AppLoggingConfigHelper.AddTarget ( _xunitSinkTarget ) ;
            Logger.Warn ( $"{nameof ( GlobalDiagnosticsContext )} logger added." ) ;
        }

        private IMessageSink Sink { get ; }

        // ReSharper disable once IdentifierTypo
        private void LogMethod ( string message )
        {
            Sink.OnMessage ( new DiagnosticMessage ( message ) ) ;
            Debug.WriteLine ( message ) ;
        }

        /// <summary>
        /// Called immediately after the class has been created, before it is used.
        /// </summary>
        public Task InitializeAsync ( ) { return Task.CompletedTask ; }

        /// <summary>
        /// Called when an object is no longer needed. Called just before <see cref="M:System.IDisposable.Dispose" />
        /// if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( )
        {
            AppLoggingConfigHelper.RemoveTarget ( _xunitSinkTarget.Name ) ;
            return Task.CompletedTask ;
        }
    }
}