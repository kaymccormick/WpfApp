using System ;
using System.Linq ;
using System.Windows ;
using System.Windows.Baml2006 ;
using System.Windows.Markup ;
using Autofac ;
using NLog ;
using Tests.Lib ;
using Tests.Lib.Attributes ;
using Tests.Lib.Fixtures ;
using Tests.Lib.Utils ;
using Tests.Misc ;
using WpfApp.Core.Interfaces ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main
{
    /// <summary>
    /// </summary>
    [ Collection ( "WpfApp" ) ]
    [ LogTestMethod ] [ BeforeAfterLogger ]
    public class WpfTests : WpfTestsBase
    {
        public WpfTests (
            WpfApplicationFixture wpfAppFixture
          , AppContainerFixture   containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper     outputHelper
        ) : base ( wpfAppFixture , containerFixture , utilsContainerFixture , outputHelper )
        {
            Logger.Debug ( $"{nameof ( WpfTests )} constructor" ) ;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        [ Fact ]
        [ Trait ( "Experimental" , "true" ) ]
        public void Test1 ( )
        {
            var menuItemList = ContainerScope.Resolve < IMenuItemList > ( ) ;
            Assert.NotNull ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList ) ;

            if ( Fixture.MyApp != null )
            {
                Fixture.MyApp.Resources[ "MyMenuItemList" ] = menuItemList ;
                var found = Fixture.MyApp.FindResource ( "MyMenuItemList" ) ;
                Assert.NotNull ( found ) ;
                var x = string.Join ( ", " , menuItemList.First ( ).Children ) ;
                Logger.Debug ( $"found {found}, {x}" ) ;
            }

            var uri = new Uri ( Fixture.BasePackUri , ResourcesPaths.MenuResourcesPath ) ;
            Logger.Debug ( $"{uri}" ) ;

            var stream = Application.GetResourceStream ( uri ) ;
            if ( stream != null )
            {
                Logger.Info ( stream.ContentType ) ;
                object o ;
                using ( var baml2006Reader = new Baml2006Reader ( stream.Stream ) )
                {
                    o = XamlReader.Load ( baml2006Reader ) ;
                }

                Assert.NotNull ( o ) ;
                //var o = Application.LoadComponent( uri );
                var menuResources = o as ResourceDictionary ;
                Assert.NotNull ( menuResources ) ;
                //var stack = InstanceFactory.CreateContextStack < InfoContext >();
                var stack = MyStack ;
                var entry =
                    MyServices.InfoContextFactory ( nameof ( menuResources ) , menuResources ) ;
                stack.Push ( entry ) ;

                foreach ( var q in menuResources.Keys )
                {
                    var resource = menuResources[ q ] ;
                    stack.Push ( MyServices.InfoContextFactory ( "key" , q ) ) ;
                    Logger.Debug ( $"{q}: {resource}" ) ;
                    DumpHelper.DumpResource ( stack , resource , MyServices.InfoContextFactory ) ;
                    stack.Pop ( ) ;
                }
            }
        }
    }

#pragma warning disable 1591
}