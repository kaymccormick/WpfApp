﻿using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Activators.Reflection ;
using Common.Logging ;
using Common.Utils ;
using JetBrains.Annotations ;
using NLog ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using TestLib ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using Tests.Lib.Attributes ;
using WpfApp.Core.Logging ;
using Xunit ;
using Xunit.Abstractions ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;

namespace CommonTests
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for ContainerHelperTests
    [ WpfTestApplication , Collection ( "WpfApp" ) , BeforeAfterLogger , LogTestMethod ]
    public class ContainerHelperTests // : IClassFixture <LoggingFixture>
    {
        static ContainerHelperTests ( ) { Debug.WriteLine ( "Initialization" ) ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public ContainerHelperTests (
            LoggingFixture        loggingFixture
          , ITestOutputHelper     output
        )
        {
            UseLogMethod = LogMethod ;
            UseLogMethod ( $"my logger is type {Logger.GetType ( )}" ) ;
            LoggingFixture        = loggingFixture ;
            // WpfApplicationFixture = wpfApplicationFixture ;
            Output                = output ;
        }

        public LoggingFixture LoggingFixture { get ; }

        public WpfApplicationFixture WpfApplicationFixture { get ; }

        public ITestOutputHelper Output { get ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        public LogDelegates.LogMethod UseLogMethod { get ; set ; }

        private void DumpContainer ( IContainer container )
        {
            var seen = new HashSet < object > ( ) ;
#if DUMP1
            foreach ( var componentRegistryRegistration in container.ComponentRegistry.Registrations )
            {
                ContainerHelper.Dump ( componentRegistryRegistration , seen, s => Output.WriteLine(s)) ;
            }
#endif

            foreach ( var comp in container.ComponentRegistry.Registrations )
            {
                if ( comp.Activator is ReflectionActivator a )
                {
                }

                var s = $"{comp.Activator.LimitType}: " ;
                foreach ( var service in comp.Services )
                {
                    if ( service is TypedService ts )
                    {
                        Output.WriteLine ( $"service {s} - {ts.ServiceType}" ) ;
                    }
                    else
                    {
                        Output.WriteLine ( $"service is {service}" ) ;
                    }
                }
            }
        }

        private void AddTestLoggingTarget ( string setupContainerTest2Name )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( LogMethod ) ;
            var fileTarget = new FileTarget ( "test target" )
                             {
                                 FileName =
                                     Layout.FromString (
                                                        "test-" + setupContainerTest2Name + ".txt"
                                                       )
                             } ;
            LogManager.LogFactory.Configuration.AddTarget ( fileTarget ) ;
            LogManager.LogFactory.Configuration.LoggingRules.Insert (
                                                                     0
                                                                   , new LoggingRule (
                                                                                      "*"
                                                                                    , LogLevel.Trace
                                                                                    , fileTarget
                                                                                     )
                                                                    ) ;
            LogManager.LogFactory.ReconfigExistingLoggers ( ) ;
        }

        private void LogMethod ( string message , string callerFilePath , string callerMemberName )
        {
            Debug.WriteLine ( message ) ;
            Output?.WriteLine ( message ) ;
        }

        /// <summary>
        /// Test resolution of MainWindow
        /// </summary>
        //[ WpfFact ]
        //[ AppInitialize ]
        //Trait("VS", "false")]
        private void ResolveMainWindow ( )
        {
            // var customAttribute = Attribute.GetCustomAttribute (
            //                                                     GetType ( )
            //                                                        .GetMethod (
            //                                                                    nameof (
            //                                                                        ResolveMainWindow
            //                                                                    )
            //                                                                   )
            //                                                   , typeof ( AppInitializeAttribute )
            //                                                    ) as AppInitializeAttribute ;

            // customAttribute.MyApp = WpfApplicationFixture.MyApp as App ;

            Assert.NotNull ( WpfApplicationFixture ) ;

            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            // var mainWindow = c.Resolve < MainWindow > ( ) ;
            // Assert.NotNull ( mainWindow ) ;
        }

        /// <summary>Tests the push context.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestPushContext
        [WpfFact ]
        public void TestPushContext ( )
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
        }


        /// <summary>Containers the test resolve i menu item list.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ContainerTestResolveIMenuItemList
        // [ Fact , UsedImplicitly , Trait ( "Working" , "false" ) ]
        public void ContainerTestResolveIMenuItemList ( )
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            DumpContainer ( container ) ;
            var menuItemList = c.Resolve < IMenuItemList > ( ) ;
            Assert.NotNull ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList.First ( ).Children ) ;
        }

        [ Fact ]
        public void ResolveWindows ( )
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            var enumerable = c.Resolve < IEnumerable < Lazy < Window > > > ( ) ;
            var l = enumerable.ToList ( ) ;
            Assert.NotEmpty ( l ) ;
            Assert.All(l, lazy => {
                Assert.NotNull ( lazy ) ;
            });
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }
        [ Fact ]
        public void SetupContainerTest ( )
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            Logger.Info ( $"{c}" ) ;
            Assert.NotNull ( c ) ;
        }

        [ Fact ]
        public void SetupContainerTest2 ( )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
            // AddTestLoggingTarget ( nameof ( SetupContainerTest2 ) ) ;
            Logger.Warn ( "I am here" ) ;
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            Assert.NotNull ( c ) ;
        }

        [ Fact ]
        public void TestResolveTopLevelMenu ( )
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer ( out container ) ;
            var topLevelMenus = c.Resolve < IEnumerable < ITopLevelMenu > > ( ).ToList ( ) ;
            Assert.NotEmpty ( topLevelMenus ) ;
            foreach ( var topLevelMenu in topLevelMenus )
            {
                Logger.Debug ( $"{topLevelMenu.GetXMenuItem ( ).Header}" ) ;
            }
        }
    }
}