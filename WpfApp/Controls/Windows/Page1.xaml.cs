﻿using System ;
using System.Collections.ObjectModel ;
using System.Reflection ;
using System.Windows.Controls ;
using System.Windows.Data ;
using DynamicData ;
using NLog ;

namespace WpfApp.Controls.Windows
{
    /// <summary>
    ///     Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>The assembly list</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for AssemblyList
        public ObservableCollection < Assembly > AssemblyList { get ; } =
            new ObservableCollection < Assembly > ( AppDomain.CurrentDomain.GetAssemblies ( ) ) ;

        /// <summary>Initializes a new instance of the <see cref="Page1"/> class.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public Page1 ( )
        {
            InitializeComponent ( ) ;
            AssemblyList.Clear ( ) ;
            AssemblyList.AddRange ( AppDomain.CurrentDomain.GetAssemblies ( ) ) ;
            foreach ( var assembly in AssemblyList )
            {
                Logger.Debug ( assembly ) ;
            }

            //var xx = Resources[ "assemblySource" ] as CollectionViewSource ;
            CollectionViewSource.GetDefaultView ( AssemblyList ).Refresh ( ) ;

            //typeBox.Items.Add ( typeof ( List < String > ) ) ;
        }
    }
}