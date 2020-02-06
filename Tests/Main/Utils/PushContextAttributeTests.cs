using NLog ;
using TestLib.Attributes ;
using WpfApp.Core.Context ;
using Xunit ;

namespace Tests.Main.Utils
{
    [ LogTestMethod ] [ BeforeAfterLogger ]
    public class PushContextAttributeTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        [ Fact ]
        [ PushContext ( "context1" ) ]
        public void AfterTest ( ) { }

        [ Fact ]
        public void BeforeTest ( ) { }

        [ Fact ]
        [ PushContext ( "test" , "123" ) ]
        public void PushContextAttributeTest ( ) { Logger.Debug ( "test" ) ; }
    }
}