using System.Runtime.CompilerServices ;

namespace WpfApp.Core.Logging
{
    public class LogDelegates
    {
        // public delegate void LogMethod (
        // string                      message
        // , [ CallerFilePath ]   string callerFilePath   = ""
        // , [ CallerMemberName ] string callerMemberName = ""
        // ) ;
        public delegate void LogMethod (
            string                      message
          , [ CallerFilePath ]   string callerFilePath   = ""
          , [ CallerMemberName ] string callerMemberName = ""
        ) ;
    }
}