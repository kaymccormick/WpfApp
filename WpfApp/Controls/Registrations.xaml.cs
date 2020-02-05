﻿using System.Windows ;
using System.Windows.Controls ;
using Autofac ;
using Common ;
using NLog ;
using WpfApp ;

namespace WpfControlLibrary1
{
    /// <summary>
    ///     Interaction	logic for Registrations.xaml
    /// </summary>
    public partial class Registrations : UserControl , ITabGuest
    {
        /// <summary>The lifetime scope property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LifetimeScopeProperty
        public static readonly DependencyProperty LifetimeScopeProperty =
            Props.LifetimeScopeProperty ;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Registrations" />
        ///     class.
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public Registrations ( )
        {
            // InitializeComponent ( ) ;
//			AddHandler (  )

            AddHandler (
                        Props.LifetimeScopeChangedEvent
                      , new RoutedPropertyChangedEventHandler < ILifetimeScope > ( Target )
                       ) ;
        }

        private void Target ( object sender , RoutedPropertyChangedEventArgs < ILifetimeScope > e )
        {
            // Logger.Debug ( $"LifetimeScopeChanged {sender} {e.NewValue}" ) ;
        }
    }
}