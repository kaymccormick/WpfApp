﻿using System ;
using System.Windows ;
using System.Windows.Markup ;
using AppShared.Interfaces ;
using Autofac ;
using NLog ;

namespace AppShared.Xaml
{
	[ MarkupExtensionReturnType ( typeof ( ILifetimeScope ) ) ]
	public class LifetimeScopeExtension : MarkupExtension
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		/// <summary>
		///     Initializes a new instance of a class derived from
		///     <see cref="T:System.Windows.Markup.MarkupExtension" />.
		/// </summary>
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
			var haveLifetimeScope = Application.Current as IHaveLifetimeScope ;
			if ( haveLifetimeScope?.LifetimeScope != null )
			{
				return haveLifetimeScope.LifetimeScope ;
			}

			return "unknown" ;
		}
	}
}