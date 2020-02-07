#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// XunitTarget.cs
// 
// 2020-01-22-9:21 AM
// 
// ---
#endregion

using NLog ;
using NLog.Targets ;
using Xunit.Abstractions ;

namespace Tests
{
    [ Target ( "Xunit" ) ]
    public class XunitTarget : TargetWithLayout
    {
        public ITestOutputHelper OutputHelper { get ; set ;  }

        public XunitTarget ( string name = null )
        {
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
protected override void Write(LogEventInfo logEvent)
{
    var logMessage = RenderLogEvent(Layout, logEvent) ;

    OutputHelper.WriteLine ( logMessage) ;
}
    }
}