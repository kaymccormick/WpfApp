﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ContainerHelperTests2.cs
// 
// 2020-01-31-6:06 PM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Activators.Reflection ;
using Common.Utils ;
using JetBrains.Annotations ;
using Logging ;
using NLog ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using Tests.Lib.Attributes ;
using WpfApp ;
using WpfApp.Core.Logging ;
using Xunit ;
using Xunit.Abstractions ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;

namespace WpfApp1Tests3
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for ContainerHelperTests2
    [ WpfTestApplication ]
    [ Collection ( "WpfApp" ) ]
    [LogTestMethod, BeforeAfterLogger]
    public class ContainerHelperTests2 // : IClassFixture <LoggingFixture>
    {
        static ContainerHelperTests2 ( ) { Debug.WriteLine ( "Initialization" ) ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public ContainerHelperTests2 (
            // ReSharper disable once UnusedParameter.Local
            LoggingFixture        loggingFixture
          , WpfApplicationFixture wpfApplicationFixture
          , ITestOutputHelper     output
        )
        {
            UseLogMethod = LogMethod ;
            UseLogMethod ( $"my logger is type {Logger.GetType ( )}" ) ;
            WpfApplicationFixture = wpfApplicationFixture ;
            Output                = output ;
        }

        /// <summary>Gets the WPF application fixture.</summary>
        /// <value>The WPF application fixture.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for WpfApplicationFixture
        public WpfApplicationFixture WpfApplicationFixture { get ; }

        internal ITestOutputHelper Output { get ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }
        private static readonly Logger Logger = LoggerProxyHelper.GetCurrentClassLogger ( ) ;

        internal LogDelegates.LogMethod UseLogMethod { get ; }

        private void DumpContainer ( IContainer container )
        {

            foreach ( var comp in container.ComponentRegistry.Registrations )
            {
                
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

        // ReSharper disable once UnusedMember.Local
        private void AddTestLoggingTarget ( string setupContainerTest2Name )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( LogMethod ) ;
            var fileTarget = new FileTarget ( "test target" ) ;
            fileTarget.FileName = Layout.FromString ( "test-" + setupContainerTest2Name + ".txt" ) ;
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
        ///     Test resolution of MainWindow
        /// </summary>
        // [ WpfFact , AppInitialize , Trait ( "VS" , "false" ) ]
        public void ResolveMainWindow ( )
        {
            // if ( Attribute.GetCustomAttribute (
            //                                    GetType ( )
            //                                       .GetMethod (
            //                                                   nameof (
            //                                                       ResolveMainWindow
            //                                                   )
                                                             // ) ?? throw new InvalidOperationException ( )
            //                                  , typeof ( AppInitializeAttribute )
            //                                   ) is AppInitializeAttribute customAttribute )
            // {
            //     customAttribute.MyApp = WpfApplicationFixture.MyApp as App ;
            // }

            // Assert.NotNull ( WpfApplicationFixture ) ;
            // Assert.NotNull ( WpfApplicationFixture.BasePackUri ) ;
            // var uri = new Uri ( WpfApplicationFixture.BasePackUri , "Application/App.xaml" ) ;
            // Logger.Info ( uri ) ;
            //
            // foreach ( DictionaryEntry resource in ( WpfApplicationFixture.MyApp as App ).Resources )
            // {
            // 	Logger.Info ( $"{resource.Key}" ) ;
            // } 

            var c = ContainerHelper.SetupContainer ( out _ ) ;
            var mainWindow = c.Resolve < MainWindow > ( ) ;
            Assert.NotNull ( mainWindow ) ;
        }

        /// <summary>Tests the push context.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestPushContext
        [WpfFact ]
        public void TestPushContext ( )
        {
            var c = ContainerHelper.SetupContainer ( out _ ) ;
            Assert.NotNull ( c) ;
        }
        


        /// <summary>Containers the test resolve i menu item list.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ContainerTestResolveIMenuItemList
        [Fact ]
        [ UsedImplicitly ]
        [ Trait ( "Working" , "false" ) ]
        public void ContainerTestResolveIMenuItemList ( )
        {
            var c = ContainerHelper.SetupContainer ( out var container ) ;
            DumpContainer ( container ) ;
            var menuItemList = c.Resolve < IMenuItemList > ( ) ;
            Assert.NotNull ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList.First ( ).Children ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        [ Fact ]
        public void ResolveWindows ( )
        {
            var c = ContainerHelper.SetupContainer ( out _ ) ;
            var enumerable = c.Resolve < IEnumerable < Lazy < Window > > > ( ) ;
            var l = enumerable.ToList ( ) ;
            Assert.NotEmpty ( l ) ;
            // Assert.Equal ( 3 , l.Count ) ;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }
        [ Fact ]
        public void SetupContainerTest ( )
        {
            var c = ContainerHelper.SetupContainer ( out _ ) ;
            Logger.Info ( $"{c}" ) ;
            Assert.NotNull ( c ) ;
        }

        /// <summary>Setups the container test2.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SetupContainerTest2
        [Fact ]
        public void SetupContainerTest2 ( )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
            // AddTestLoggingTarget ( nameof ( SetupContainerTest2 ) ) ;
            Logger.Warn ( "I am here" ) ;
            var c = ContainerHelper.SetupContainer ( out _ ) ;
            Assert.NotNull ( c ) ;
        }

        /// <summary>Tests the resolve top level menu.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestResolveTopLevelMenu
        [Fact ]
        public void TestResolveTopLevelMenu ( )
        {
            var c = ContainerHelper.SetupContainer ( out _ ) ;
            var topLevelMenus = c.Resolve < IEnumerable < ITopLevelMenu > > ( ).ToList ( ) ;
            Assert.NotEmpty ( topLevelMenus ) ;
            foreach ( var topLevelMenu in topLevelMenus )
            {
                Logger.Debug ( $"{topLevelMenu.GetXMenuItem ( ).Header}" ) ;
            }
        }
    }
}