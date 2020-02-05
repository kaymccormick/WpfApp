namespace TestLib.Fixture
{
    public class MyServicesFixture
#pragma warning restore 1591
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public MyServicesFixture ( IMyServices myServices ) { MyServices = myServices ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IMyServices MyServices { get ; }
    }
}