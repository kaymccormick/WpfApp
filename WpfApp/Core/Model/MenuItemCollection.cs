using System ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.Linq ;
using NLog ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Logging ;

namespace WpfApp.Core.Model
{
    /// <summary></summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    /// <seealso cref="IMenuItemCollection" />
    /// <seealso cref="IHaveLogger" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for MenuItemList
    [ LoggingEntityMetadata ( typeof ( MenuItemCollection ) ) ]
    public class MenuItemCollection : ObservableCollection < IMenuItem > , IMenuItemCollection , IHaveLogger
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuItemCollection" />
        ///     class.
        /// </summary>
        /// <param name="topLevelMenus">The top level menus.</param>
        /// <param name="loggerFunc">The logger function.</param>
        public MenuItemCollection (
            IEnumerable < ITopLevelMenu > topLevelMenus
          , Func < Type , ILogger >       loggerFunc
        ) : base ( topLevelMenus.Select ( menu => menu.GetXMenuItem ( ) ) )
        {
            Logger = loggerFunc ( typeof ( MenuItemCollection ) ) ;
            Logger.Info ( $"Creating {nameof ( MenuItemCollection )} [ Count = {Count} ] " ) ;
        }

        /// <summary>Gets or sets the logger.</summary>
        /// <value>The logger.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        public ILogger Logger { get ; set ; }
    }
}