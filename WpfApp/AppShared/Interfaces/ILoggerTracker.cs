using NLog ;

namespace AppShared.Interfaces
{
	public interface ILoggerTracker
	{
		void TrackLogger ( string loggerName , ILogger logger ) ;

		event LoggerRegisteredEventHandler LoggerRegistered ;
	}
}