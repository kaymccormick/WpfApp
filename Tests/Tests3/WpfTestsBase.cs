#region header
// Kay McCormick (mccor)
// 
// WpfApp
// Tests
// WpfTestsBase.cs
// 
// 2020-02-04-5:13 PM
// 
// ---
#endregion
using AppShared.Infos ;
using Autofac ;
using TestLib ;
using TestLib.Fixtures ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
    public class WpfTestsBase
    {
        protected AppContainerFixture _containerFixture ;

        protected WpfTestsBase (
            WpfApplicationFixture fixture
          , AppContainerFixture   containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper     outputHelper
        )
        {

        }

        public ILifetimeScope containerScope { get => _containerFixture.LifetimeScope ; }

        public IMyServices myServices { get ; set ; }

        public ContextStack < InfoContext > MyStack { get ; set ; }

        public WpfApplicationFixture Fixture { get ; set ; }
    }
}