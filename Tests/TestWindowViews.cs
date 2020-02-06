using Tests.Lib.Fixtures ;
using Xunit ;

namespace Tests
{
    [Collection("WpfApp")]
    public class TestWindowViews
    {
        public WpfApplicationFixture WpfApplicationFixture { get ; }

        public AppContainerFixture AppContainerFixture { get ; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestWindowViews (WpfApplicationFixture wpfApplicationFixture, AppContainerFixture appContainerFixture)
        {
            WpfApplicationFixture = wpfApplicationFixture ;
            AppContainerFixture = appContainerFixture ;
        }

        public void TestContainer1()
        {

        }
    }
}
