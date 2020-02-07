using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Reflection ;
using System.Windows ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using Autofac.Core.Resolving ;
using Autofac.Extras.AttributeMetadata ;
using Castle.DynamicProxy ;
using NLog ;
using WpfApp.Core.Attributes ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Logging ;
using WpfApp.Modules ;
using WpfApp.Proxy ;

namespace WpfApp.Core.Util
{
    /// <summary>
    ///     static helper class for autofac container
    /// </summary>
    public static class ContainerHelper
    {
        /// <summary>The assemblies for scanning property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for AssembliesForScanningProperty
        public const string AssembliesForScanningProperty = "AssembliesForScanning" ;
            
        internal static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator ( ) ;

        private static readonly Logger Logger = LogManager.GetLogger ( "ContainerHelper" ) ;
        private static readonly Random Random = new Random ( ) ;

        /// <summary>The intercept property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InterceptProperty
        public const string InterceptProperty = "Intercept" ;

        /// <summary>Setups the container.</summary>
        /// <param name="containerHelperSettings"></param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SetupContainer
        // ReSharper disable once UnusedMember.Global
        public static ILifetimeScope SetupContainer (
            ContainerHelperSettings containerHelperSettings = null
        )
        {
            return SetupContainer ( out _ , containerHelperSettings ) ;
        }

        /// <summary>Setups the container.</summary>
        /// <param name="container">The container.</param>
        /// <param name="containerHelperSettings"></param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SetupContainer
        public static ILifetimeScope SetupContainer (
            out IContainer          container
          , ContainerHelperSettings containerHelperSettings = null
        )
        {
            var scan = GetAssembliesForScanningViaTypes ( ) ;
            return SetupContainer ( out container , scan , containerHelperSettings ) ;
        }


        /// <summary>Setups the container.</summary>
        /// <param name="container">The container.</param>
        /// <param name="assembliesToScan">The assemblies to scan.</param>
        /// <param name="containerHelperSettings"></param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SetupContainer
        public static ILifetimeScope SetupContainer (
            out IContainer           container
          , IEnumerable < Assembly > assembliesToScan
          , ContainerHelperSettings  containerHelperSettings
        )
        {
            ApplySettings ( containerHelperSettings ) ;

            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
            BuilderInterceptor builderInterceptor = null ;
#if ENABLE_BUILDERPROXY
            if ( DoProxyBuilder )
            {
                Logger.Info ( "Proxying container builder for debug purposes." ) ;
                var proxyGenerator = ProxyGenerator ;
                builderInterceptor = new BuilderInterceptor ( proxyGenerator ) ;
                var proxy =
                    proxyGenerator.CreateClassProxy < ContainerBuilder > ( builderInterceptor ) ;

                builder = proxy ;
            }
else
            {
                builder = new ContainerBuilder ( ) ;
            }
#else
            var builder = new ContainerBuilder ( ) ;
#endif

            builder.Properties[ InterceptProperty ] = DoInterception ;

            var toScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray ( ) ;


            builder.Properties[ AssembliesForScanningProperty ] = GetAssembliesForScanning ( ) ;
            #region Autofac Modules
            builder.RegisterModule < AttributedMetadataModule > ( ) ;

            // builder.RegisterModule < MenuModule > ( ) ;

            // TODO does order matter?
            builder.RegisterModule < IdGeneratorModule > ( ) ;
            #endregion

            var i = 0 ;

            void LogStuff ( int index )
            {
                builderInterceptor?.Invocations.ForEach (
                                                         invocation
                                                             => Logger.Debug (
                                                                              $"{index}]: {invocation.Method.Name} ({string.Join ( ", " , invocation.Arguments )}) => {invocation.OriginalReturnValue}"
                                                                             )
                                                        ) ;
            }


            LogStuff ( i ) ;
            // ReSharper disable once RedundantAssignment
            i += 1 ;

            #region Currently unused?
            //builder.RegisterType<SystemParametersControl> ( ).As<ISettingsPanel> ( );
            #endregion

            #region Assembly scanning
            builder.RegisterAssemblyTypes ( toScan )
                   .Where ( MainScanningPredicate )
                   .AsImplementedInterfaces ( ) ;
            builder.RegisterAssemblyTypes ( toScan.ToArray ( ) )
                   .Where (
                           delegate ( Type t ) {
                               var isAssignableFrom = typeof ( Window ).IsAssignableFrom ( t ) ;
                               TraceConditionalRegistration (
                                                             t
                                                           , typeof ( Window )
                                                           , isAssignableFrom
                                                            ) ;
                               return isAssignableFrom ;
                           }
                          )
                   .AsSelf ( )
                   .As < Window > ( )
                   .OnActivating (
                                  args => {
                                      var argsInstance = args.Instance ;

                                      if ( argsInstance is IHaveLogger haveLogger )
                                      {
                                          haveLogger.Logger =
                                              args.Context.Resolve < ILogger > (
                                                                                new TypedParameter (
                                                                                                    typeof
                                                                                                    ( Type
                                                                                                    )
                                                                                                  , argsInstance
                                                                                                       .GetType ( )
                                                                                                   )
                                                                               ) ;
                                      }
                                  }
                                 ) ;

            // builder.RegisterAssemblyTypes ( executingAssembly )
            //        .Where ( predicate : t => typeof ( ITopLevelMenu ).IsAssignableFrom ( c : t ) )
            //        .As < ITopLevelMenu > ( ) ;
            #endregion

            // builder.RegisterType < AppTraceListener > ( )
            //        .As < TraceListener > ( )
            //        .InstancePerLifetimeScope ( ) ;
            #region Interceptors
            if ( DoInterception )
            {
                builder.RegisterType < LoggingInterceptor > ( ).AsSelf ( ) ;
            }
            #endregion

            #region Logging
            builder.RegisterType < LoggerTracker > ( )
                   .As < ILoggerTracker > ( )
                   .InstancePerLifetimeScope ( ) ;

            // builder.RegisterType < LogFactory > ( ).AsSelf ( ) ;

            builder.Register (
                              ( c , p ) => {
                                  var loggerName = "unset" ;
                                  try
                                  {
                                      loggerName = p.TypedAs < Type > ( ).FullName ;
                                  }
                                  catch ( Exception ex )
                                  {
                                      Console.WriteLine ( ex.ToString ( ) ) ;
                                  }

                                  var tracker = c.Resolve < ILoggerTracker > ( ) ;
                                  Logger.Trace ( $"creating logger loggerName = {loggerName}" ) ;
                                  var logger = LogManager.GetLogger ( loggerName ) ;
                                  tracker.TrackLogger ( loggerName , logger ) ;
                                  return logger ;
                              }
                             )
                   .As < ILogger > ( ) ;
            #endregion


            #region Callbacks
            builder.RegisterBuildCallback ( c => Logger.Info ( "Container built." ) ) ;
            builder.RegisterCallback (
                                      registry => {
                                          registry.Registered += ( sender , args ) => {
                                              Logger.Trace (
                                                            "Registered "
                                                            + args.ComponentRegistration.Activator
                                                                  .LimitType
                                                           ) ;
                                              args.ComponentRegistration.Activated += (
                                                  o
                                                , eventArgs
                                              ) => {
                                                  Logger.Trace (
                                                                $"Activated {DescribeComponent ( eventArgs.Component )} (sender={o}, instance={eventArgs.Instance})"
                                                               ) ;
                                              } ;
                                          } ;
                                      }
                                     ) ;
            #endregion

            #region Container Build
            var setupContainer = builder.Build ( ) ;
            container = setupContainer ;
            setupContainer.ChildLifetimeScopeBeginning +=
                SetupContainerOnChildLifetimeScopeBeginning ;
            #endregion
            setupContainer.CurrentScopeEnding        += SetupContainerOnCurrentScopeEnding ;
            setupContainer.ResolveOperationBeginning += SetupContainerOnResolveOperationBeginning ;


            var beginLifetimeScope = setupContainer.BeginLifetimeScope ( "initial scope" ) ;

            return beginLifetimeScope ;
        }

        private static void ApplySettings ( ContainerHelperSettings s )
        {
            if ( s != null )
            {
                DoInterception                 = s.DoInterception ;
                DoTraceConditionalRegistration = s.DoTraceConditionalRegistration ;
            }
        }

        private static void SetupContainerOnResolveOperationBeginning (
            object                             sender
          , ResolveOperationBeginningEventArgs e
        )
        {
            e.ResolveOperation.CurrentOperationEnding += ResolveOperationOnCurrentOperationEnding ;
            e.ResolveOperation.InstanceLookupBeginning +=
                ResolveOperationOnInstanceLookupBeginning ;
            Logger.Info ( $"{nameof ( SetupContainerOnResolveOperationBeginning )} " ) ;
        }

        private static void ResolveOperationOnInstanceLookupBeginning (
            object                           sender
          , InstanceLookupBeginningEventArgs e
        )
        {
            Logger.Info ( $"{nameof ( ResolveOperationOnInstanceLookupBeginning )}" ) ;
        }

        private static void ResolveOperationOnCurrentOperationEnding (
            object                          sender
          , ResolveOperationEndingEventArgs e
        )
        {
            Logger.Info ( $"{nameof ( ResolveOperationOnCurrentOperationEnding )}" ) ;
        }


        private static bool MainScanningPredicate ( Type arg )
        {
            var r = false;//typeof ( ITabGuest ).IsAssignableFrom ( arg ) ;
            if ( DoTraceConditionalRegistration )
            {
                Logger.Trace ( $"Conditional registration for {arg} is {r}" ) ;
            }

            return r ;
        }

        /// <summary>Gets or sets a value indicating whether [do interception].</summary>
        /// <value>
        ///   <c>true</c> if [do interception]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoInterception
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public static bool DoInterception { get ; set ; } = true ;

        private static void TraceConditionalRegistration (
            Type type
          , Type type1
          , bool isAssignableFrom
        )
        {
            if ( DoTraceConditionalRegistration )
            {
                Logger.Trace (
                              $"Conditional registration for {type} is {isAssignableFrom} [{type1}]"
                             ) ;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [do trace conditional
        ///     registration].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [do trace conditional registration]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoTraceConditionalRegistration
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public static bool DoTraceConditionalRegistration { get ; set ; }

        /// <summary>Gets or sets a value indicating whether [do proxy builder].</summary>
        /// <value>
        ///     <c>true</c> if [do proxy builder]; otherwise, <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoProxyBuilder
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        // ReSharper disable once UnusedMember.Global
        public static bool DoProxyBuilder { get ; set ; } = true ;

        /// <summary>Gets the assemblies for scanning.</summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetAssembliesForScanning
        public static ICollection < Assembly > GetAssembliesForScanning ( )
        {
            if ( GetAssembliesViaReferences )
            {
                return GetAssembliesForScanningByReferences ( ) ;
            }

            return GetAssembliesForScanningViaTypes ( ) ;
        }

        /// <summary>Gets or sets a value indicating whether [get assemblies via references].</summary>
        /// <value>
        ///   <c>true</c> if [get assemblies via references]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetAssembliesViaReferences
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public static bool GetAssembliesViaReferences { get ; set ; }

        /// <summary>Gets the assemblies for scanning.</summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetAssembliesForScanning
        public static ICollection < Assembly > GetAssembliesForScanningByReferences ( )
        {
            Logger.Debug (
                          "Getting assemblies to scan based on AssemblyContainerScan attribute."
                         ) ;

            var assembliesForScanning = AppDomain.CurrentDomain.GetAssemblies ( )
                                                 .Where (
                                                         ( assembly , i1 ) => Attribute.IsDefined (
                                                                                                   assembly
                                                                                                 , typeof
                                                                                                   ( AssemblyContainerScanAttribute
                                                                                                   )
                                                                                                  )
                                                        ) ;
            var forScanning = assembliesForScanning.ToList ( ) ;
            Logger.Info (
                         "Assemblies "
                         + string.Join (
                                        ", "
                                      , forScanning.Select (
                                                            ( assembly , i1 )
                                                                => assembly.GetName ( ).Name
                                                           )
                                       )
                        ) ;
            return forScanning ;
        }

        /// <summary>Gets the assemblies for scanning via types.</summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetAssembliesForScanningViaTypes
        public static ICollection < Assembly > GetAssembliesForScanningViaTypes ( )
        {
            Logger.Debug (
                          "Getting assemblies to scan based on AssemblyContainerScan attribute."
                         ) ;

            Type[] ary = { typeof ( IHaveObjectId ) , typeof ( ContainerHelper ) } ;
            var forScanning = ary.Select ( ( type , i ) => type.Assembly ).ToList ( ) ;
            Logger.Info (
                         "Assemblies "
                         + string.Join (
                                        ", "
                                      , forScanning.Select (
                                                            ( assembly , i1 )
                                                                => assembly.GetName ( ).Name
                                                           )
                                       )
                        ) ;
            return forScanning ;
        }

        private static void SetupContainerOnCurrentScopeEnding (
            object                       sender
          , LifetimeScopeEndingEventArgs e
        )
        {
            Logger.Error (
                          $"{nameof ( SetupContainerOnCurrentScopeEnding )} {e.LifetimeScope.Tag}"
                         ) ;
        }

        private static void SetupContainerOnChildLifetimeScopeBeginning (
            object                          sender
          , LifetimeScopeBeginningEventArgs e
        )
        {
            var n = Random.Next ( 1024 ) ;
            Logger.Error ( $"Child lifetime scope beginning {n}:  {e.LifetimeScope.Tag}" ) ;
            e.LifetimeScope.ChildLifetimeScopeBeginning +=
                SetupContainerOnChildLifetimeScopeBeginning ;
        }

        private static string DescribeComponent ( IComponentRegistration eventArgsComponent )
        {
            var debugDesc = "no description" ;
            var key = "DebugDescription" ;
            if ( eventArgsComponent.Metadata.ContainsKey ( key ) )
            {
                debugDesc = eventArgsComponent.Metadata[ key ].ToString ( ) ;
            }

            return $" CompReg w({eventArgsComponent.Id}, {debugDesc})" ;
        }
#if DOIT
        private static DeferredCallback ConfigurationAction ( ContainerBuilder obj )
        {
            return ;
            // return obj.RegisterCallback ( CreateChildLifetimeContext ) ;
        }
            private static void CreateChildLifetimeContext ( IComponentRegistryBuilder componentRegistryBuilder )
        {
            // var setupContainer = componentRegistry ;
#if USEHANDLER
            setupContainer.ResolveOperationBeginning += ( sender , args ) => {
                args.ResolveOperation.InstanceLookupBeginning += ( o , eventArgs ) => {
                    eventArgs.InstanceLookup.InstanceLookupEnding +=
 ( sender1 , endingEventArgs ) => {
                        if ( endingEventArgs.NewInstanceActivated )
                        {
                            Logger.Debug ( "New instance activated" ) ;
                        }
                    } ;
                } ;
            } ;
#endif

            var registry = componentRegistry ;
            foreach ( var componentRegistryRegistration in registry.Registrations )
            {
                if ( IsMyRegistration ( componentRegistryRegistration ) )
                {
                    Logger.Warn ( "is my registration" ) ;
                }
                else
                {
                    Logger.Error ( "is not my registration" ) ;
                }

                var seen = new HashSet < object > ( ) ;
                Dump ( componentRegistryRegistration , seen ) ;

                if ( componentRegistryRegistration.Activator is ReflectionActivator rf )
                {
                    Logger.Debug ( rf.LimitType.ToString ( ) ) ;
                    var x = new DelegateActivator (
                                                   rf.LimitType
     , ( context , parameters ) => {
                                                       Logger.Debug (
                                                                     "delegate activation of reflection component success."
                                                                    ) ;
                                                       var r = rf.ActivateInstance (
                                                                                    context
                                      , parameters
                                                                                   ) ;
                                                       Logger.Debug ( "got " + r ) ;
                                                       if ( r is IHaveLogger haveLogger )
                                                       {
                                                           Logger.Debug (
                                                                         "has IHaveLogger interface."
                                                                        ) ;
                                                           if ( haveLogger.Logger == null )
                                                           {
                                                               Logger.Debug (
                                                                             "logger is null, resolving"
                                                                            ) ;
                                                               haveLogger.Logger =
                                                                   context.Resolve < ILogger > (
                                                                                                new
                                                                                                    TypedParameter (
                                                                                                                    typeof
                                                                                                                    ( Type
                                                                                                                    )
                                                                      , r
                                                                                                                       .GetType ( )
                                                                                                                   )
                                                                                               ) ;
                                                           }
                                                       }

                                                       return r ;
                                                   }
                                                  ) ;

                    IComponentRegistration componentRegistration = new ComponentRegistration (
                                                                                              Guid
                                                                                                 .NewGuid ( )
                                                , x
                                                , componentRegistryRegistration
                                                                                                 .Lifetime
                                                , componentRegistryRegistration
                                                                                                 .Sharing
                                                , componentRegistryRegistration
                                                                                                 .Ownership
                                                , componentRegistryRegistration
                                                                                                 .Services
                                                , componentRegistryRegistration
                                                                                                 .Metadata
                                                , componentRegistryRegistration
                                                                                             ) ;

                    Logger.Debug ( "wrapping reflection with delegate" ) ;
                    try
                    {
                        registry.Register ( componentRegistration ) ;
                    }
                    catch ( Exception ex )
                    {
                        Logger.Debug ( ex , "failure is " + ex.Message ) ;
                    }
                }
                else if ( componentRegistryRegistration.Activator is DelegateActivator d )
                {
                    if ( ! ( d is MyActivator ) )
                    {
                        if ( componentRegistryRegistration.Ownership
                             == InstanceOwnership.ExternallyOwned )
                        {
                            Logger.Debug ( "Externally owned component registration." ) ;
                        }
                        else
                        {
                            var x = new DelegateActivator (
                                                           d.LimitType
             , ( context , parameters ) => {
                                                               Logger.Debug ( "activating !!" ) ;
                                                               var r = d.ActivateInstance (
                                                                                           context
                                             , parameters
                                                                                          ) ;
                                                               Logger.Debug ( "got " + r ) ;
                                                               return r ;
                                                           }
                                                          ) ;


                            IComponentRegistration componentRegistration =
                                new ComponentRegistration (
                                                           Guid.NewGuid ( )
             , x
             , componentRegistryRegistration.Lifetime
             , componentRegistryRegistration.Sharing
             , componentRegistryRegistration.Ownership
             , componentRegistryRegistration.Services
             , componentRegistryRegistration.Metadata
             , componentRegistryRegistration
                                                          ) ;


                            Logger.Debug ( "wrapping delegate activator" ) ;
                            try
                            {
                                registry.Register ( componentRegistration ) ;
                            }
                            catch ( Exception ex )
                            {
                                Logger.Debug ( ex , "failure is " + ex.Message ) ;
                            }
                        }
                    }
                }

                componentRegistryRegistration.Preparing += ( sender , args ) => {
                } ;

                componentRegistryRegistration.Activated += ( sender , args ) => {
                    Logger.Debug ( $"Activated {args.Instance}" ) ;
                } ;
            }
        
        }

        private static bool IsMyRegistration (
            IComponentRegistration componentRegistryRegistration
        )
        {
            return GetIsMyAssembly ( componentRegistryRegistration.Activator.LimitType.Assembly ) ;
        }

        private static bool GetIsMyAssembly ( Assembly limitTypeAssembly )
        {
            Logger.Debug ( $"{limitTypeAssembly.GetName ( )}" ) ;
            if ( AssemblyName.ReferenceMatchesDefinition (
                                                          limitTypeAssembly.GetName ( )
            , Assembly
                                                         .GetExecutingAssembly ( )
                                                         .GetName ( )
                                                         ) )
            {
                return true ;
            }

            return false ;
        }
#endif
        // ReSharper disable once UnusedMember.Global
        /// <summary>Dumps the specified component registry registration.</summary>
        /// <param name="componentRegistryRegistration">
        ///     The component registry
        ///     registration.
        /// </param>
        /// <param name="seenObjects">The seen objects.</param>
        /// <param name="outFunc">The out function.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Dump
        public static void Dump (
            IComponentRegistration componentRegistryRegistration
          , HashSet < object >     seenObjects
          , Action < string >      outFunc
        )
        {
            var activatorLimitType = componentRegistryRegistration.Activator.LimitType ;

            if ( seenObjects.Contains ( componentRegistryRegistration ) )
            {
                return ;
            }

            seenObjects.Add ( componentRegistryRegistration ) ;
            outFunc ( "Id = "             + componentRegistryRegistration.Id ) ;
            outFunc ( "Activator type = " + componentRegistryRegistration.Activator.GetType ( ) ) ;



            outFunc ( "LimitType = " + activatorLimitType ) ;


            foreach ( var service in componentRegistryRegistration.Services )
            {
                outFunc ( "Service is " + service.Description ) ;
            }

            if ( componentRegistryRegistration.Target == null )
            {
                outFunc ( "Target registration is null." ) ;
            }
            else if ( Equals (
                              componentRegistryRegistration
                            , componentRegistryRegistration.Target
                             ) )
            {
                outFunc ( "Target is same registration." ) ;
            }
            else
            {
                Dump ( componentRegistryRegistration.Target , seenObjects , outFunc ) ;
            }
        }
    }

    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for ContainerHelperSettings
    public class ContainerHelperSettings
    {
        /// <summary>Gets or sets a value indicating whether [do interception].</summary>
        /// <value>
        ///   <c>true</c> if [do interception]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoInterception
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool DoInterception { get ; set ; }

        /// <summary>Gets or sets a value indicating whether [do trace conditional registration].</summary>
        /// <value>
        ///   <c>true</c> if [do trace conditional registration]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoTraceConditionalRegistration
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool DoTraceConditionalRegistration { get ; set ; }
    }
}
