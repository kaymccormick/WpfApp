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
using Autofac ;
using Autofac.Extras.DynamicProxy ;
using NLog ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Logging ;
using WpfApp.Core.Model ;
using WpfApp.Core.Util ;
using WpfApp.Core.Utils ;
using WpfApp.Proxy ;
using Module = Autofac.Module ;

namespace WpfApp.Core.Menus
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
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        protected override void Load ( ContainerBuilder builder )
        {
            var intercept = ( bool ) builder.Properties[ ContainerHelper.InterceptProperty ] ;
            var ass =
                ( ICollection < Assembly > ) builder.Properties[ ContainerHelper.AssembliesForScanningProperty ] ;
            if ( ass != null )
            {
                builder.RegisterAssemblyTypes ( ass.ToArray ( ) )
                       .Where (
                               t => {
                                   var isAssignableFrom =
                                       typeof ( ITopLevelMenu ).IsAssignableFrom ( t ) ;
                                   if ( isAssignableFrom )
                                   {
                                       Logger.Debug ( "* Scanned and registering " + t ) ;
                                   }

                                   return isAssignableFrom ;
                               }
                              )
                       .As < ITopLevelMenu > ( ) ;
            }

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