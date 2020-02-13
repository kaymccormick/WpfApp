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
using KayMcCormick.Logging.Common ;
#if ENABLE_BUILDERPROXY
using Castle.DynamicProxy ;
#endif
using NLog ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Logging ;
using WpfApp.Modules ;
using WpfApp.Proxy ;

namespace WpfApp.Core.Container
{
    /// <summary>
    ///     static helper class for autofac container. Contains static methods to help bootstrap the container. <see cref="ContainerHelper.SetupContainer(container, assembliesToScan, containerHelperSettings)"/>.
    /// </summary>
    public static class ContainerHelper
    {
        /// <summary>The assemblies for scanning property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for AssembliesForScanningProperty
        public const string AssembliesForScanningProperty = "AssembliesForScanning" ;
#if ENABLE_BUILDERPROXY
        internal static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator ( ) ;
#endif

        private static readonly Logger Logger = LogManager.GetLogger ( "ContainerHelper" ) ;
        private static readonly Random Random = new Random ( ) ;

        /// <summary>The current lifetime scope</summary>
        [ ThreadStatic ]
        // ReSharper disable once NotAccessedField.Global
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static ILifetimeScope CurrentLifetimeScope ;
#pragma warning restore CA2211 // Non-constant fields should not be visible

        [ ThreadStatic ]
#pragma warning disable 169
        private static Stack < ILifetimeScope > _lifetimeScopes ;
#pragma warning restore 169

        /// <summary>Property name used to propagate the value of <see cref="DoInterception"/>.</summary>
        public const string InterceptProperty = "Intercept" ;


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
            if ( assembliesToScan == null )
            {
                assembliesToScan = GetAssembliesForScanningViaTypes ( ) ;
            }

            if ( containerHelperSettings != null )
            {
                Logger.Info ( "Applying container helper settings from app.config" ) ;
                ApplySettings ( containerHelperSettings ) ;
            }
            else
            {
                Logger.Debug ( "No containerHelperSettings to apply." ) ;
            }

            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;

#if ENABLE_BUILDERPROXY
BuilderInterceptor builderInterceptor = null ;
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

            // Set the property in order to propagate the settings.
            builder.Properties[ InterceptProperty ] = DoInterception ;

            var toScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray ( ) ;


            builder.Properties[ AssembliesForScanningProperty ] = GetAssembliesForScanning ( ) ;
            #region Autofac Modules
            builder.RegisterModule < AttributedMetadataModule > ( ) ;
            builder.RegisterModule < IdGeneratorModule > ( ) ;
#if MENUS_ENABLE
            builder.RegisterModule < > ( ) ;
#endif
            #endregion

            var i = 0 ;

            void LogStuff ( ref int index )
            {
#if ENABLE_BUILDERPROXY
                builderInterceptor?.Invocations.ForEach (
                                                         invocation
                                                             => Logger.Debug (
                                                                              $"{index}]: {invocation.Method.Name} ({string.Join ( ", " , invocation.Arguments )}) => {invocation.OriginalReturnValue}"
                                                                             )
                                                        ) ;
#endif
                index ++ ;
            }

            LogStuff ( ref i ) ;

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
            #endregion
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
            var r = false ; //typeof ( ITabGuest ).IsAssignableFrom ( arg ) ;
            if ( DoTraceConditionalRegistration )
            {
                Logger.Trace ( $"Conditional registration for {arg} is {r}" ) ;
            }

            return r ;
        }

        /// <summary>Gets or sets a value indicating whether install interceptors for built objects.</summary>
        /// <value>
        ///   <see language="true"/> to perform interception; otherwise, <see language="false"/>.</value>
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
        ///     Gets or sets a value indicating whether to trace conditional
        ///     registration.
        /// </summary>
        /// <value>
        ///     <see language="true"/> to trace conditional registration; otherwise,
        ///     <see language="false"/>.
        /// </value>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public static bool DoTraceConditionalRegistration { get ; set ; }

        /// <summary>Gets or sets a value indicating whether to proxy the ContainerBuilder.</summary>
        /// <value>
        ///     <see language="true"/> to proxy builder; otherwise, <see language="false"/>.
        /// </value>
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
        ///   <see language="true"/> if [get assemblies via references]; otherwise, <see language="false"/>.</value>
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
            throw new NotImplementedException();
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
            Logger.Info(
                          $"{nameof ( SetupContainerOnCurrentScopeEnding )} {e.LifetimeScope.Tag}"
                         ) ;
            CurrentLifetimeScope = null ;
        }

        private static void SetupContainerOnChildLifetimeScopeBeginning (
            object                          sender
          , LifetimeScopeBeginningEventArgs e
        )
        {
            var n = Random.Next ( 1024 ) ;
            Logger.Info( $"Child lifetime scope beginning {n}:  {e.LifetimeScope.Tag}" ) ;
            e.LifetimeScope.ChildLifetimeScopeBeginning +=
                SetupContainerOnChildLifetimeScopeBeginning ;
            CurrentLifetimeScope = e.LifetimeScope ;
        }

        private static string DescribeComponent ( IComponentRegistration eventArgsComponent )
        {
            var debugDesc = "no description" ;
            const string key = "DebugDescription" ;
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
}