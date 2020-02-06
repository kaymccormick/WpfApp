using System.Windows ;
using WpfApp.Core.Attributes ;

namespace WpfApp.Controls.Windows
{
    /// <summary>
    ///     Interaction logic for AppSettingsWindow.xaml
    /// </summary>
    [ WindowMetadata ( "Application Settings" ) ]
    public partial class AppSettingsWindow : Window
    {
        public AppSettingsWindow ( ) { InitializeComponent ( ) ; }
    }
}