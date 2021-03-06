using System.Linq ;
using System.Windows ;
using System.Windows.Controls ;
using NLog ;
using WpfApp.Core.Interfaces ;

namespace WpfApp.Core.Menus
{
    /// <summary></summary>
    /// <seealso cref="System.Windows.Controls.DataTemplateSelector" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for MenuMenuItemTemplateSelector
    public class MenuMenuItemTemplateSelector : DataTemplateSelector

    {
        private const string MenuItemTemplateNoChildrenTemplateName = "Menu_ItemTemplateNoChildren";
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>
        ///     When overridden in a derived class, returns a
        ///     <see cref="System.Windows.DataTemplate" /> based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>
        ///     Returns a <see cref="System.Windows.DataTemplate" /> or
        ///     <span class="keyword">
        ///         <span class="languageSpecificText">
        ///             <span class="cs">null</span><span class="vb">Nothing</span>
        ///             <span class="cpp">nullptr</span>
        ///         </span>
        ///     </span>
        ///     <span class="nu">
        ///         a null reference (<span class="keyword">Nothing</span>
        ///         in Visual Basic)
        ///     </span>
        ///     . The default value is
        ///     <span class="keyword">
        ///         <span class="languageSpecificText">
        ///             <span class="cs">null</span><span class="vb">Nothing</span>
        ///             <span class="cpp">nullptr</span>
        ///         </span>
        ///     </span>
        ///     <span class="nu">
        ///         a null reference (<span class="keyword">Nothing</span>
        ///         in Visual Basic)
        ///     </span>
        ///     .
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for SelectTemplate
        public override DataTemplate SelectTemplate ( object item , DependencyObject container )
        {
            var menuItem = container as MenuItem ;
            if ( container is ContentPresenter )
            {
                Logger.Debug ( "contentpresenter" ) ;
                return base.SelectTemplate ( item , container ) ;
            }
            // if ( menuItem == null )
            // {
            //     Logger.Warn( $"container is not a menuitem {container.GetType()}" );
            //     return base.SelectTemplate( item, container );
            // }

            if ( ! ( item is IMenuItem ) )
            {
                Logger.Warn ( "item is not a IMenuItem" ) ;
                return base.SelectTemplate ( item , container ) ;
            }

            Logger.Info ( $"menuItem is {menuItem}" ) ;
            Logger.Debug ( $"args are {item} {container}" ) ;
            Logger.Debug ( $"item type is {item.GetType ( )}" ) ;
            if ( container != null )
            {
                var r = container as FrameworkElement ;
                if ( item is IMenuItem x )
                {
                    Logger.Debug ( "item is IMenuItem" ) ;
                    if ( x.Children.Any ( ) )
                    {
                        const string key = "Menu_ItemTemplateChildren" ;
                        Logger.Info ( $"Selecting template {key} for {container}" ) ;
                        if ( r != null )
                        {
                            if ( r.FindResource ( key ) is DataTemplate dataTemplate )
                            {
                                Logger.Debug ( $"returning {key} {dataTemplate.DataTemplateKey}" ) ;
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
                                return dataTemplate ;
                            }
                        }
                    }

                    else
                    {
                        const string key = MenuItemTemplateNoChildrenTemplateName;

                        Logger.Info ( $"Selecting template {key} for {container}" ) ;

                        if ( menuItem != null )
                        {
                            var dataTemplate = menuItem.FindResource ( key ) as DataTemplate ;
                            Logger.Debug ( $"returning {key} {dataTemplate?.DataTemplateKey}" ) ;
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
                            return dataTemplate ;
                        }
                    }
                }
            }

            Logger.Debug ( "Returning result from base method" ) ;
            return base.SelectTemplate ( item , container ) ;
        }
    }
}