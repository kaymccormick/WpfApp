using AppShared.Modules ;
using Autofac ;
using WpfApp.Core.Logging ;

namespace CommonTests.Fixtures
{
    public class TestContainerFixture
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public TestContainerFixture ( ) { _init ( ) ; }

        public IContainer Container { get ; private set ; }

        private void _init ( )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
            var builder = new ContainerBuilder ( ) ;
            builder.RegisterModule < IdGeneratorModule > ( ) ;
            builder.RegisterType < RandomClass > ( ).As < IRandom > ( ) ;

            Container = builder.Build ( ) ;
        }
    }
}