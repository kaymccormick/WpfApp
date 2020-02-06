using System ;
using System.Windows.Markup ;
using System.Xaml ;
using NLog ;
using WpfApp.Core.Logging ;

namespace WpfApp.Core.Xaml
{
    [ MarkupExtensionReturnType ( typeof ( AppLogger ) ) ]
    // ReSharper disable once UnusedType.Global
    internal class AppLoggerExtension : MarkupExtension
    {
        /// <summary>
        ///     Initializes a new instance of a class derived from
        ///     <see cref="T:System.Windows.Markup.MarkupExtension" />.
        /// </summary>
        public AppLoggerExtension ( )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
            Logger = LogManager.GetCurrentClassLogger ( ) ;
        }

        /// <summary>
        ///     Initializes a new instance of a class derived from
        ///     <see cref="T:System.Windows.Markup.MarkupExtension" />.
        /// </summary>
        public AppLoggerExtension ( string arg ) : this ( )
        {
            Arg = arg ;
            LogManager.GetCurrentClassLogger ( ).Info ( $"{arg}" ) ;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public ILogger Logger { get ; set ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public string Arg { get ; set ; }

        /// <summary>
        ///     When implemented in a derived class, returns an object that is
        ///     provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">
        ///     A service provider helper that can provide
        ///     services for the markup extension.
        /// </param>
        /// <returns>
        ///     The object value to set on the property where the extension is
        ///     applied.
        /// </returns>
        public override object ProvideValue ( IServiceProvider serviceProvider )
        {
            var service = serviceProvider.GetService ( typeof ( IProvideValueTarget ) ) ;
            if ( service is IProvideValueTarget provideValueTarget )
            {
                Console.WriteLine ( provideValueTarget.TargetObject ) ;
            }

            if ( service is IRootObjectProvider provide )
            {
                Console.WriteLine ( provide.RootObject ) ;
            }

            return new AppLogger ( LogManager.GetCurrentClassLogger ( ) ) ;
        }
    }
}