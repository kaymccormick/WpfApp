#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MenuModule.cs
// 
// 2020-01-25-2:03 PM
// 
// ---
#endregion
using System.Collections.Generic ;
using System.Linq ;
using System.Reflection ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Builder ;
using Autofac.Extras.DynamicProxy ;
using Common.Logging ;
using Common.Model ;
using Common.Utils ;
using NLog ;
using Module = Autofac.Module ;

namespace Common.Menus
{
    /// <summary>Autofac module supporting menu system.</summary>
    /// <seealso cref="Autofac.Module" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for MenuModule
    public class MenuModule : Module
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load ( ContainerBuilder builder )
        {
            var intercept = ( bool ) builder.Properties[ ContainerHelper.InterceptProperty ] ;
            var ass =
                builder.Properties[ ContainerHelper.AssembliesForScanningProperty ] as
                    ICollection < Assembly > ;
            builder.RegisterAssemblyTypes ( ass.ToArray ( ) )
                   .Where (
                           t => {
                               var isAssignableFrom =
                                   typeof ( ITopLevelMenu ).IsAssignableFrom ( t ) ;
                               if ( isAssignableFrom )
                               {
                                   Logger.Debug ( "Scanned and registering " + t.ToString ( ) ) ;
                               }

                               return isAssignableFrom ;
                           }
                          )
                   .As < ITopLevelMenu > ( ) ;
            #region Menu Item Lists
            Logger.Debug ( "Registering MenuItemList" ) ;
            var q = builder.RegisterType < MenuItemList > ( )
                            //.AsImplementedInterfaces ( )
                           .WithMetadata < ResourceMetadata > (
                                                               m => m.For (
                                                                           rn => rn.ResourceName
                                                                         , "MenuItemList"
                                                                          )
                                                              )
                           .PreserveExistingDefaults ( )
                           .As < IMenuItemList > ( ) ;
            if ( intercept )
            {
                Logger.Debug ( "enabling interception" ) ;
                q.EnableInterfaceInterceptors ( ).InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
            }

            Logger.Debug ( "registering XMenuItem" ) ;
            var y = builder.RegisterType < XMenuItem > ( )
                           .As < IMenuItem > ( )
                           .AsImplementedInterfaces ( )
                           .PreserveExistingDefaults ( ) ;
            if ( intercept )
            {
                Logger.Debug ( "enabling interception" ) ;
                y.EnableInterfaceInterceptors ( ).InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
            }
            #endregion
        }
    }
}