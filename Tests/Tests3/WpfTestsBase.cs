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
using Tests.Lib.Fixtures ;
using WpfApp.AppShared.Infos ;
using Xunit.Abstractions ;

namespace Tests.Tests3
{
    public class WpfTestsBase
    {
        protected readonly AppContainerFixture ContainerFixture ;

        protected WpfTestsBase (
            WpfApplicationFixture wpfAppFixture
          , AppContainerFixture   containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper     outputHelper
        )
        {
            WpfAppFixture         = wpfAppFixture ;
            UtilsContainerFixture = utilsContainerFixture ;
            OutputHelper          = outputHelper ;
            ContainerFixture      = containerFixture ;
        }

        public WpfApplicationFixture WpfAppFixture { get ; }

        public UtilsContainerFixture UtilsContainerFixture { get ; }

        public ITestOutputHelper OutputHelper { get ; }

        protected ILifetimeScope ContainerScope => ContainerFixture.LifetimeScope ;

        protected IMyServices MyServices { get ; set ; }

        protected ContextStack < InfoContext > MyStack { get ; set ; }

        protected WpfApplicationFixture Fixture { get ; set ; }
    }
}