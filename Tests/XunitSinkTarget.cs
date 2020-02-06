#region header
// Kay McCormick (mccor)
// 
// WpfApp
// Tests
// XunitSinkTarget.cs
// 
// 2020-02-06-2:12 AM
// 
// ---
#endregion
using NLog ;
using NLog.Targets ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests
{
    [ Target ( "Xunit" ) ]
    public class XunitSinkTarget : TargetWithLayout
    {
        public IMessageSink Sink { get ; set ; }

        protected override void InitializeTarget ( ) { base.InitializeTarget ( ) ; }

        public XunitSinkTarget ( string name ) : base ( )
        {
            OptimizeBufferReuse = false ;
            if ( name != null )
            {
                Name = name ;
            }
        }

        /// <summary>
        ///     Writes logging event to the log target. Must be overridden in inheriting
        ///     classes.
        /// </summary>
        /// <param name="logEvent">Logging event to be written out.</param>
        protected override void Write ( LogEventInfo logEvent )
        {
            string logMessage ;

            logMessage = RenderLogEvent ( Layout , logEvent ) ;

            Sink.OnMessage ( new Xunit.Sdk.DiagnosticMessage ( logMessage ) ) ;
        }

    }
}