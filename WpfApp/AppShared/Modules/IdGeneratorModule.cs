﻿using System.Linq ;
using System.Runtime.Serialization ;
using AppShared.Interfaces ;
using AppShared.Services ;
using AppShared.Utils ;
using Autofac ;
using Autofac.Core ;
using NLog ;
using WpfApp.AppShared.Interfaces ;

namespace AppShared.Modules
{
    /// <summary></summary>
    /// <seealso cref="Autofac.Module" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for IdGeneratorModule
    public class IdGeneratorModule : Module
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Gets or sets the default object.</summary>
        /// <value>The default object.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DefaultObject
        public DefaultObjectIdProvider DefaultObject { get ; set ; }

        /// <summary>Gets or sets the generator.</summary>
        /// <value>The generator.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Generator
        public ObjectIDGenerator Generator { get ; set ; }

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        protected override void Load ( ContainerBuilder builder )
        {
            //var obIdGenerator = new ObjectIDGenerator();
            Logger.Trace ( $"Load {nameof ( IdGeneratorModule )}" ) ;


            Generator = new ObjectIDGenerator ( ) ;
            //builder.RegisterInstance ( generator ).As < ObjectIDGenerator > ( ) ;
//			builder.RegisterType < ObjectIDGenerator > ( ).InstancePerLifetimeScope ( ).AsSelf ( ) ;
            DefaultObject = new DefaultObjectIdProvider ( Generator ) ;
            builder.RegisterInstance ( DefaultObject )
                   .As < IObjectIdProvider > ( )
                   .SingleInstance ( ) ;
            // builder.RegisterType < DefaultObjectIdProvider > ( )
            //        .As < IObjectIdProvider > ( )
            //        .InstancePerLifetimeScope ( ) ;
        }

        
        /// <summary>
        /// Override to attach module-specific functionality to a
        /// component registration.
        /// </summary>
        /// <remarks>This method will be called for all existing <i>and future</i> component
        /// registrations - ordering is not important.</remarks>
        /// <param name="componentRegistry">The component registry.</param>
        /// <param name="registration">The registration to attach functionality to.</param>
        protected override void AttachToComponentRegistration (
            IComponentRegistry componentRegistry
          , IComponentRegistration    registration
        )
        {
            registration.Preparing  += RegistrationOnPreparing ;
            registration.Activating += RegistrationOnActivating ;
        }

        private void RegistrationOnActivating ( object sender , ActivatingEventArgs < object > e )
        {
            var inst = e.Instance ;

            Logger.Trace ( $"{nameof ( RegistrationOnActivating )} {e.Component.DebugFormat()}" ) ;
            if ( e.Component.Services.Any (
                                           service => {
                                               var typedService = service as TypedService ;
                                               // Logger.Trace ( typedService ) ;
                                               if ( typedService == null )
                                               {
                                                   return false ;
                                               }

                                               var typedServiceServiceType =
                                                   typedService.ServiceType ;
                                               return typedServiceServiceType
                                                      == typeof ( ObjectIDGenerator ) ;

                                           }
                                          ) )
            {
                Logger.Debug ( $"Departing {nameof(RegistrationOnActivating)} early.");
                return ;
            }

            //var provider = e.Context.Resolve < IObjectIdProvider > ( ) ;
            var provideObjectInstanceIdentifier =
                DefaultObject.ProvideObjectInstanceIdentifier (
                                                               inst
                                                             , e.Component
                                                             , e.Parameters
                                                              ) ;
            if ( inst is IHaveObjectId x )
            {
                x.InstanceObjectId = provideObjectInstanceIdentifier ;
            }
        }

        private void RegistrationOnPreparing ( object sender , PreparingEventArgs e )
        {
            // e.Parameters = e.Parameters.Union (
            //                                    new[]
            //                                    {
            // 	                                   new ResolvedParameter (
            // 	                                                          ( info , context )
            // 		                                                          => info.ParameterType
            // 		                                                             == typeof (
            // 			                                                             IObjectIdProvider )
            // 	                                                        , ( info , context ) => {
            // 		                                                          return
            // 			                                                          null ; //context.
            // 	                                                          }
            // 	                                                         )
            //                                    }
            //                                   ) ;
        }
    }
}