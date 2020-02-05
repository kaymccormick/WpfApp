using AppShared.Modules ;
using Autofac ;
using WpfApp.Core.Logging ;

namespace CommonTests.Fixtures
{
	public class ContainerFixture
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public ContainerFixture ( ) { _init ( ) ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
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

	public interface IRandom
	{
	}

	public class RandomClass : IRandom
	{
	}
}