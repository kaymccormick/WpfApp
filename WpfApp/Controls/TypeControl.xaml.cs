﻿using System ;
using System.CodeDom ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Documents ;
using System.Windows.Markup ;
using System.Windows.Navigation ;
using Microsoft.CSharp ;
using NLog ;

namespace WpfApp.Controls
{
    /// <summary>
    ///     Interaction logic for TypeControl.xaml
    /// </summary>
    public partial class TypeControl : UserControl
    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>The rendered type property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RenderedTypeProperty
        public static readonly DependencyProperty
            RenderedTypeProperty = Props.RenderedTypeProperty ;

        /// <summary>The target name property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TargetNameProperty
        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.Register (
                                         "TargetName"
                                       , typeof ( string )
                                       , typeof ( TypeControl )
                                       , new PropertyMetadata ( default ( string ) )
                                        ) ;

        /// <summary>The target property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TargetProperty
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register (
                                         "Target"
                                       , typeof ( Frame )
                                       , typeof ( TypeControl )
                                       , new PropertyMetadata ( default ( Frame ) )
                                        ) ;

        /// <summary>The detailed property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DetailedProperty
        public static readonly DependencyProperty DetailedProperty =
            DependencyProperty.Register (
                                         "Detailed"
                                       , typeof ( bool )
                                       , typeof ( TypeControl )
                                       , new PropertyMetadata ( default ( bool ) )
                                        ) ;

        /// <summary>The target detailed property</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TargetDetailedProperty
        public static readonly DependencyProperty TargetDetailedProperty =
            DependencyProperty.Register (
                                         "TargetDetailed"
                                       , typeof ( bool )
                                       , typeof ( TypeControl )
                                       , new PropertyMetadata ( default ( bool ) )
                                        ) ;

        /// <summary>Initializes a new instance of the <see cref="TypeControl" /> class.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public TypeControl ( )
        {
            RenderedTypeChanged += OnRenderedTypeChanged ;
            InitializeComponent ( ) ;
            PopulateControl ( GetValue ( RenderedTypeProperty ) as Type ) ;
        }

        /// <summary>Gets or sets the type of the rendered.</summary>
        /// <value>The type of the rendered.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RenderedType
        public Type RenderedType
        {
            // ReSharper disable once UnusedMember.Global
            get => GetValue ( RenderedTypeProperty ) as Type ;
            set => SetValue ( RenderedTypeProperty , value ) ;
        }

        /// <summary>Gets or sets the name of the target.</summary>
        /// <value>The name of the target.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TargetName
        // ReSharper disable once UnusedMember.Global
        public string TargetName
        {
            get => ( string ) GetValue ( TargetNameProperty ) ;
            set => SetValue ( TargetNameProperty , value ) ;
        }

        /// <summary>Gets or sets the target.</summary>
        /// <value>The target.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Target
        public Frame Target
        {
            get => ( Frame ) GetValue ( TargetProperty ) ;
            set => SetValue ( TargetProperty , value ) ;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="TypeControl" /> is detailed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if detailed; otherwise, <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Detailed
        public bool Detailed
        {
            get => ( bool ) GetValue ( DetailedProperty ) ;
            set => SetValue ( DetailedProperty , value ) ;
        }

        /// <summary>Gets or sets a value indicating whether [target detailed].</summary>
        /// <value>
        ///     <c>true</c> if [target detailed]; otherwise, <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TargetDetailed
        public bool TargetDetailed
        {
            get => ( bool ) GetValue ( TargetDetailedProperty ) ;
            set => SetValue ( TargetDetailedProperty , value ) ;
        }

        /// <summary>Gets or sets the flow document.</summary>
        /// <value>The flow document.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for flowDocument
        public FlowDocument FlowDocument { get ; set ; }


        /// <summary>Occurs when [rendered type changed].</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RenderedTypeChanged
        public event RoutedPropertyChangedEventHandler < Type > RenderedTypeChanged
        {
            add => AddHandler ( Props.RenderedTypeChangedEvent , value ) ;
            remove => RemoveHandler ( Props.RenderedTypeChangedEvent , value ) ;
        }

        /// <summary>Called when [rendered type changed].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedPropertyChangedEventArgs{T}" />
        ///     instance containing the event data.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for OnRenderedTypeChanged
        private void OnRenderedTypeChanged (
            object                                  sender
          , RoutedPropertyChangedEventArgs < Type > e
        )
        {
            PopulateControl ( e.NewValue ) ;
        }

        /// <summary>Populates the control.</summary>
        /// <param name="myType">My type.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for PopulateControl
        private void PopulateControl ( Type myType )
        {
            IAddChild addChild ;
            if ( Detailed )
            {
                var paragraph = new Paragraph ( ) ;
                FlowDocument = new FlowDocument ( paragraph ) ;
                var reader = new FlowDocumentReader { Document = FlowDocument } ;
                addChild = paragraph ;
                Content  = reader ;
            }
            else
            {
                addChild = new TextBlock ( ) ;
                Content  = addChild ;

                // Container.Children.Clear();
                // Container.Children.Add ( block ) ;
            }

            if ( myType == null )
            {
                return ;
            }


            if ( Detailed )
            {
                var elem = new List { MarkerStyle = TextMarkerStyle.None } ;

                var baseType = myType.BaseType ;
                while ( baseType != null )
                {
                    var paragraph = new Paragraph ( ) ;
                    var listItem = new ListItem ( paragraph ) ;

                    GenerateControlsForType ( baseType , paragraph , false ) ;
                    elem.ListItems.Add ( listItem ) ;
                    //Container.Children.Insert ( 0 , new TextBlock ( new Hyperlink()) ( baseType.Name ) ) ) ;
                    baseType = baseType.BaseType ;
                }

                FlowDocument.Blocks.InsertBefore ( FlowDocument.Blocks.FirstBlock , elem ) ;
            }

            var p = new Span ( ) ;
            GenerateControlsForType ( myType , p , true ) ;
            addChild.AddChild ( p ) ;
            // Viewer.Document.Blocks.Add ( block ) ;
            // Container.Children.Add ( ) ;
        }

        /// <summary>Generates the type of the controls for.</summary>
        /// <param name="myType">My type.</param>
        /// <param name="addChild">The add child.</param>
        /// <param name="toolTip">if set to <c>true</c> [tool tip].</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GenerateControlsForType
        private void GenerateControlsForType ( Type myType , IAddChild addChild , bool toolTip )
        {
            // TextBlock tb = new TextBlock();
            // var old = addChild ;
            // addChild = tb ;


            var hyperLink = new Hyperlink ( new Run ( myType.Name ) ) ;
            Uri.TryCreate (
                           "obj://" + Uri.EscapeUriString ( myType.Name )
                         , UriKind.Absolute
                         , out var res
                          ) ;

            hyperLink.NavigateUri = res ;
            // hyperLink.Command          = MyAppCommands.VisitTypeCommand ;
            // hyperLink.CommandParameter = myType ;
            if ( toolTip )
            {
                hyperLink.ToolTip = new ToolTip { Content = ToolTipContent ( myType ) } ;
            }

            hyperLink.RequestNavigate += HyperLinkOnRequestNavigate ;
            addChild.AddChild ( hyperLink ) ;
            if ( myType.IsGenericType )
            {
                addChild.AddText ( "<" ) ;
                var i = 0 ;
                foreach ( var arg in myType.GenericTypeArguments )
                {
                    GenerateControlsForType ( arg , addChild , true ) ;
                    if ( i < myType.GenericTypeArguments.Length )
                    {
                        addChild.AddText ( ", " ) ;
                    }
                }

                addChild.AddText ( ">" ) ;
            }

            //old.AddChild ( tb ) ;
        }

        /// <summary>Tools the content of the tip.</summary>
        /// <param name="myType">My type.</param>
        /// <param name="pp">The pp.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ToolTipContent
        private object ToolTipContent ( Type myType , StackPanel pp = null )
        {
            var provider = new CSharpCodeProvider ( ) ;
            var codeTypeReference = new CodeTypeReference ( myType ) ;
            var q = codeTypeReference ;
            var toolTipContent = new TextBlock
                                 {
                                     Text = provider.GetTypeOutput ( q ) , FontSize = 20
                                     //, Margin = new Thickness ( 15 )
                                 } ;
            if ( pp == null )
            {
                pp = new StackPanel { Orientation = Orientation.Vertical } ;
            }

            pp.Children.Insert ( 0 , toolTipContent ) ;
            var @base = myType.BaseType ;
            if ( @base != null )
            {
                ToolTipContent ( @base , pp ) ;
            }

            return pp ;
        }

        /// <summary>Names for type.</summary>
        /// <param name="myType">My type.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for NameForType
        // ReSharper disable once UnusedMember.Local
        private string NameForType ( Type myType )
        {
            var provider = new CSharpCodeProvider ( ) ;
            if ( myType.IsGenericType )
            {
                var type = myType.GetGenericTypeDefinition ( ) ;
                myType = type ;
            }

            var codeTypeReference = new CodeTypeReference ( myType ) ;
            var q = codeTypeReference ;
            //myType.GetGenericTypeParameters()
            return provider.GetTypeOutput ( q ) ;
            // return myType.IsGenericType ? myType.GetGenericTypeDefinition ( ).Name : myType.Name ;
        }

        /// <summary>Navigate handler for type hyperlinks.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.Navigation.RequestNavigateEventArgs" />
        ///     instance
        ///     containing the event data.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for HyperLinkOnRequestNavigate
        private void HyperLinkOnRequestNavigate ( object sender , RequestNavigateEventArgs e )
        {
            //
            // if(findName == null)
            // {
            // 	Logger.Debug ( "Cant find " + findName) ;
            // }
            Logger.Debug ( $"{nameof ( HyperLinkOnRequestNavigate )}: Uri={e.Uri}" ) ;
            var uie = ( ContentElement ) sender ;
            try
            {
                var navigationService = NavigationService ( ) ;

                if ( navigationService != null )
                {
                    var targetDetailed = Detailed || TargetDetailed ;
                    var value = uie.GetValue ( Props.RenderedTypeProperty ) as Type ;
                    var typeControl2 = new TypeControl2 ( ) ;
                    typeControl2.SetValue ( Props.RenderedTypeProperty , value ) ;
                    var navigationState = new NavState
                                          {
                                              Detailed = targetDetailed , RenderedType = value
                                          } ;
                    if ( ! navigationService.Navigate ( typeControl2 , navigationState ) )
                    {
                        Logger.Error ( "nav cancelled" ) ;
                    }

                    e.Handled = true ;
                }
                else
                {
                    Logger.Info ( "find other way to navigate type" ) ;
                }
            }
            catch ( Exception ex )
            {
                Logger.Warn ( ex , ex.Message ) ;
            }
        }

        /// <summary>Get navigation service.</summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for NavigationService
        private NavigationService NavigationService ( )
        {
            var navigationService = Target != null
                                        ? System.Windows.Navigation.NavigationService
                                                .GetNavigationService (
                                                                       Target.Content as
                                                                           DependencyObject
                                                                       ?? throw new
                                                                           InvalidOperationException ( )
                                                                      )
                                        : System.Windows.Navigation.NavigationService
                                                .GetNavigationService ( this ) ;
            return navigationService ;
        }
    }

    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for NavState
    internal class NavState
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="NavState" /> is detailed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if detailed; otherwise, <c>false</c>.
        /// </value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Detailed
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool Detailed { get ; set ; }

        /// <summary>Gets or sets the type of the rendered.</summary>
        /// <value>The type of the rendered.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for RenderedType
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Type RenderedType { get ; set ; }
    }
}