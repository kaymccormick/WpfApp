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
        public WpfApplicationFixture WpfAppFixture { get ; }

        public UtilsContainerFixture UtilsContainerFixture { get ; }

        public ITestOutputHelper OutputHelper { get ; }

        protected readonly AppContainerFixture ContainerFixture ;

        protected WpfTestsBase (
            WpfApplicationFixture wpfAppFixture
          , AppContainerFixture   containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper     outputHelper
        )
        {
            WpfAppFixture = wpfAppFixture ;
            UtilsContainerFixture = utilsContainerFixture ;
            OutputHelper = outputHelper ;
            ContainerFixture = containerFixture ;
        }

        public ILifetimeScope containerScope { get => ContainerFixture.LifetimeScope ; }

        public IMyServices myServices { get ; set ; }

        public ContextStack < InfoContext > MyStack { get ; set ; }

        public WpfApplicationFixture Fixture { get ; set ; }
    }
}