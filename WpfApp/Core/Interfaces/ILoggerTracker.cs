using NLog ;

namespace WpfApp.Core.Interfaces
{
    public interface ILoggerTracker
    {
        void TrackLogger ( string loggerName , ILogger logger ) ;

        event LoggerRegisteredEventHandler LoggerRegistered ;
    }
}