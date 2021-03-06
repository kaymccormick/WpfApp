﻿using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows ;
using Autofac ;
using JetBrains.Annotations ;
using KayMcCormick.Test.Common ;
using KayMcCormick.Test.Common.Fixtures ;
using NLog ;
using Tests.CollectionDefinitions ;
using Tests.Lib.Fixtures ;
using WpfApp.Core.Interfaces ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main
{
    /// <summary>Tests fpr the configured autofac container. Uses collection "WpfApp." <seealso cref="WpfApplication"/></summary>
    [ Collection ( @"WpfApp" ) ] [ BeforeAfterLogger ] [ LogTestMethod ]
    public class ContainerHelperTests : IClassFixture < LoggingFixture > , IDisposable
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        static ContainerHelperTests ( ) { Debug.WriteLine ( "Initialization" ) ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public ContainerHelperTests (
            LoggingFixture        loggingFixture
          , ITestOutputHelper     output
          , AppContainerFixture   appContainerFixture
        )
        {
            loggingFixture.SetOutputHelper ( output ) ;

            LoggingFixture = loggingFixture ;

            Scope  = appContainerFixture.BeginLifetimeScope ( nameof ( ContainerHelperTests ) ) ;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        /// <summary>Gets the logging fixture.</summary>
        /// <value>The logging fixture.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LoggingFixture
        public LoggingFixture LoggingFixture { get ; }


        /// <summary>Gets the scope.</summary>
        /// <value>The scope.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Scope
        public ILifetimeScope Scope { get ; }

        /// <summary>Containers the test resolve i menu item list.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ContainerTestResolveIMenuItemList
        // [ Fact ] [ UsedImplicitly ] [ Trait ( "Working" , "false" ) ]
        public void ContainerTestResolveIMenuItemList ( )
        {
            var menuItemList = Scope.Resolve < IMenuItemCollection > ( ) ;
            Assert.NotNull ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList.First ( ).Children ) ;
        }

        /// <summary>Resolves the windows.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ResolveWindows
        [ Fact ]
        public void ResolveWindows ( )
        {
            var enumerable = Scope.Resolve < IEnumerable < Lazy < Window > > > ( ) ;
            var l = enumerable.ToList ( ) ;
            Assert.NotEmpty ( l ) ;
            Assert.All ( l , Assert.NotNull ) ;
        }

        /// <summary>Tests the nothing.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestNothing
        [Fact ]
        public void TestNothing ( )
        {
            Logger.Info(Scope.Tag.ToString);
        }

        #if MENUS_ENABLE
        /// <summary>Tests the resolve top level menu.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestResolveTopLevelMenu
        /// 
        [ Fact ]
        public void TestResolveTopLevelMenu ( )
        {
            var topLevelMenus = Scope.Resolve < IEnumerable < ITopLevelMenu > > ( ).ToList ( ) ;
            Assert.NotEmpty ( topLevelMenus ) ;
            foreach ( var topLevelMenu in topLevelMenus )
            {
                Logger.Debug ( $"{topLevelMenu.GetXMenuItem ( ).Header}" ) ;
            }
        }
        #endif
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( )
        {
            // LoggingFixture?.Dispose ( ) ;
            LoggingFixture.SetOutputHelper ( null ) ;
            Scope?.Dispose ( ) ;
        }
    }
}