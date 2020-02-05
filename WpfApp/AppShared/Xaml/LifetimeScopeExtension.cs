﻿using System ;
using System.Windows ;
using System.Windows.Markup ;
using Autofac ;
using NLog ;
using WpfApp.Core.Interfaces.Interfaces ;

namespace AppShared.Xaml
{
    [ MarkupExtensionReturnType ( typeof ( ILifetimeScope ) ) ]
    public class LifetimeScopeExtension : MarkupExtension
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;


        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="LifetimeScopeExtension" /> class.
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public LifetimeScopeExtension ( ) { Logger.Info ( nameof ( LifetimeScopeExtension ) ) ; }

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
            // todo fixme
            var haveLifetimeScope = Application.Current as IHaveLifetimeScope ;
            if ( haveLifetimeScope?.LifetimeScope != null )
            {
                return haveLifetimeScope.LifetimeScope ;
            }

            return "unknown" ;
        }
    }
}