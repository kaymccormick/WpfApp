﻿using System ;
using System.Xaml ;
using System.Xaml.Schema ;
using AppShared.Xaml ;
using NLog ;

namespace AppShared
{
	// TODO try to implement this
	public class CustomResourceLoader : XamlValueConverter < LifetimeScopeExtension >
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:System.Xaml.Schema.XamlValueConverter`1" /> class, based on a
		///     converter implementing <see cref="T:System.Type" /> and the
		///     target/destination type of the
		///     <see cref="T:System.Xaml.Schema.XamlValueConverter`1" />.
		/// </summary>
		/// <param name="converterType">
		///     The <see cref="T:System.Type" /> that implements
		///     the converter behavior.
		/// </param>
		/// <param name="targetType">
		///     The target/destination
		///     <see cref="T:System.Xaml.XamlType" /> of the
		///     <see cref="T:System.Xaml.Schema.XamlValueConverter`1" />.
		/// </param>
		public CustomResourceLoader ( Type converterType , XamlType targetType ) :
			base ( converterType , targetType )
		{
			Logger.Warn ( $"{nameof ( CustomResourceLoader )} - {converterType} - {targetType}" ) ;
		}


		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:System.Xaml.Schema.XamlValueConverter`1" /> class, based on a
		///     converter implementing <see cref="T:System.Type" /> the target/destination
		///     type of the <see cref="T:System.Xaml.Schema.XamlValueConverter`1" />, and a
		///     string name.
		/// </summary>
		/// <param name="converterType">
		///     The <see cref="T:System.Type" /> that implements
		///     the converter behavior.
		/// </param>
		/// <param name="targetType">
		///     The target/destination
		///     <see cref="T:System.Xaml.XamlType" /> of the
		///     <see cref="T:System.Xaml.Schema.XamlValueConverter`1" />.
		/// </param>
		/// <param name="name">The string name.</param>
		/// <exception cref="T:System.ArgumentException">
		///     All three parameters are
		///     <see langword="null" /> (at least one is required to be non-null).
		/// </exception>
		public CustomResourceLoader ( Type converterType , XamlType targetType , string name ) :
			base ( converterType , targetType , name )
		{
			Logger.Warn (
			             $"{nameof ( CustomResourceLoader )} - {converterType} - {targetType} ={name}"
			            ) ;
		}

		/// <summary>Returns an instance of the converter implementation.</summary>
		/// <returns>
		///     An instance of the converter implementation, or
		///     <see langword="null" />.
		/// </returns>
		/// <exception cref="T:System.Xaml.XamlSchemaException">
		///     Converter did not implement
		///     the correct base type.
		/// </exception>
		protected override LifetimeScopeExtension CreateInstance ( )
		{
			Logger.Warn ( $"{nameof ( CreateInstance )}" ) ;
			return base.CreateInstance ( ) ;
		}
	}
}