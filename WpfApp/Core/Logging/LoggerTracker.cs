using System.Collections.Concurrent ;
using NLog ;
using WpfApp.Core.Interfaces ;

namespace WpfApp.Core.Logging
{
    /// <summary></summary>
    /// <seealso cref="ILoggerTracker" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggerTracker
    public class LoggerTracker : ILoggerTracker
    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _Logger
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>The loggers</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for loggers
        private readonly ConcurrentDictionary < string , ILogger > loggers =
            new ConcurrentDictionary < string , ILogger > ( ) ;

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        public ILogger Logger => _Logger ;

        /// <summary>Tracks the logger.</summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="logger">The logger.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TrackLogger
        public void TrackLogger ( string loggerName , ILogger logger )
        {
            // race condition
            if ( loggers.TryGetValue ( loggerName , out _ ) )
            {
                Logger.Debug ( $"logger {loggerName} exists already." ) ;
            }
            else
            {
                loggers.TryAdd ( loggerName , logger ) ;
                OnLoggerRegistered ( new LoggerEventArgs ( logger ) ) ;
            }
        }

        /// <summary>Occurs when [logger registered].</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LoggerRegistered
        public event LoggerRegisteredEventHandler LoggerRegistered ;

        /// <summary>Raises the <see cref="E:LoggerRegistered" /> event.</summary>
        /// <param name="args">
        ///     The <see cref="LoggerEventArgs" /> instance containing
        ///     the event data.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for OnLoggerRegistered
        protected virtual void OnLoggerRegistered ( LoggerEventArgs args )
        {
            var handler = LoggerRegistered ;
            handler?.Invoke ( this , args ) ;
        }
    }
}