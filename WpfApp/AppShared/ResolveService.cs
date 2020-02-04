#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// AppShared
// ResolveService.cs
// 
// 2020-01-28-2:53 AM
// 
// ---
#endregion
using System ;
using System.Windows ;
using Autofac.Core ;
using NLog ;
using WpfApp ;

namespace AppShared
{
	public class ResolveService : FrameworkElement
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public static readonly DependencyProperty
			LifetimeScopeProperty = Props.LifetimeScopeProperty ;

		public static readonly RoutedEvent ServiceInstanceChangedEvent =
			EventManager.RegisterRoutedEvent (
			                                  "ServiceInstanceChanged"
			                                , RoutingStrategy.Direct
			                                , typeof ( RoutedPropertyChangedEventHandler < object >
			                                  )
			                                , typeof ( ResolveService )
			                                 ) ;

		public static readonly DependencyProperty ServiceInstanceProperty =
			DependencyProperty.Register (
			                             "ServiceInstance"
			                           , typeof ( object )
			                           , typeof ( ResolveService )
			                           , new FrameworkPropertyMetadata (
			                                                            null
			                                                          , FrameworkPropertyMetadataOptions
				                                                           .None
			                                                          , OnServiceInstanceChanged
			                                                          , CoerceValueCallback
			                                                           )
			                            ) ;

		public Type ServiceType { get ; set ; }

		public object ServiceInstance
		{
			get => GetValue ( ServiceInstanceProperty ) ;
			set => SetValue ( ServiceInstanceProperty , value ) ;
		}

		public Service Service { get ; set ; }

		private static void OnServiceInstanceChanged (
			DependencyObject                   d
		  , DependencyPropertyChangedEventArgs e
		)
		{
			var s = d as ResolveService ;
			s?.RaiseEvent (
			               new RoutedPropertyChangedEventArgs < object > ( e.OldValue , e.NewValue )
			              ) ;
		}

		private static object CoerceValueCallback ( DependencyObject d , object baseValue )
		{
			return baseValue ;
		}


		// ReSharper disable once EventNeverSubscribedTo.Global
		public event RoutedPropertyChangedEventHandler < object > ServiceInstanceChanged
		{
			add => AddHandler ( ServiceInstanceChangedEvent , value ) ;
			remove => RemoveHandler ( ServiceInstanceChangedEvent , value ) ;
		}

		/// <summary>
		///     Raises the
		///     <see cref="E:System.Windows.FrameworkElement.Initialized" /> event. This
		///     method is invoked whenever
		///     <see cref="P:System.Windows.FrameworkElement.IsInitialized" /> is set to
		///     <see langword="true " />internally.
		/// </summary>
		/// <param name="e">
		///     The <see cref="T:System.Windows.RoutedEventArgs" /> that
		///     contains the event data.
		/// </param>
		protected override void OnInitialized ( EventArgs e )
		{
			base.OnInitialized ( e ) ;
			Logger.Debug ( $"{nameof ( OnInitialized )}" ) ;
			Logger.Debug (
			              "Value of Lifetimescopeproperty is " + GetValue ( LifetimeScopeProperty )
			             ) ;
		}
	}
}