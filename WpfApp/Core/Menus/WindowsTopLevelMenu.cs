using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Windows ;
using Autofac.Features.Metadata ;
using JetBrains.Annotations ;
using NLog ;
using WpfApp.Core.Interfaces ;

namespace WpfApp.Core.Menus
{
    /// <summary>
    ///     Definition of primary top level menu
    /// </summary>
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        private readonly IMenuItem          _xMenuItem ;
        private readonly Func < IMenuItem > _xMenuItemCreator ;

        private readonly ILogger Logger ;

        /// <summary>Initializes a new instance of the <see cref="WindowsTopLevelMenu"/> class.</summary>
        /// <param name="windows">The windows.</param>
        /// <param name="xMenuItemCreator">The x menu item creator.</param>
        /// <param name="func">The function.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public WindowsTopLevelMenu (
            IEnumerable < Meta < Lazy < Window > > > windows
          , Func < IMenuItem >                       xMenuItemCreator
          , [ NotNull ] Func < Type , ILogger >                  func
        )
        {
            if ( func == null )
            {
                throw new ArgumentNullException ( nameof ( func ) ) ;
            }

            Logger = func ( typeof ( WindowsTopLevelMenu ) ) ;

            _xMenuItemCreator = xMenuItemCreator ;
            _xMenuItem        = xMenuItemCreator ( ) ;
            Windows           = windows ;
        }

        /// <summary>Gets the windows.</summary>
        /// <value>The windows.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Windows
        public IEnumerable < Meta < Lazy < Window > > > Windows { get ; }

        /// <summary>Gets the x menu item.</summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetXMenuItem
        public IMenuItem GetXMenuItem ( )
        {
            var root = _xMenuItem ;
            _xMenuItem.Header   = "Windows" ;
            _xMenuItem.Children = Windows.Select ( Selector ).ToList ( ) ;
            return root ;
        }

        private IMenuItem Selector ( Meta < Lazy < Window > > window , int i )
        {
            var m = _xMenuItemCreator ( ) ;
            if ( window.Metadata.ContainsKey ( "WindowTitle" ) == false )
            {
                Logger.Warn ( $"No window title for window {window}" ) ;
                m.Header = window.Value.ToString ( ) ;
            }
            else
            {
                m.Header = ( string ) window.Metadata[ "WindowTitle" ] ;
            }

            m.Command          = WpfAppCommands.OpenWindow ;
            m.CommandParameter = window ;
            return m ;
        }
    }
}