using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Windows.Markup ;
using Autofac ;
using Common.Converters ;
using JetBrains.Annotations ;
using NLog ;

namespace Common
{
    /// <summary></summary>
    /// <seealso cref="System.Windows.Markup.IAddChild" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestO
    [ ContentProperty ( "Content" ) ]
    public class TestO : IAddChild

    {
        /// <summary>The logger</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public TestO ( ) { Logger.Debug ( "constructeD" ) ; }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Content
        [ TypeConverter ( typeof ( RegistrationSourceConverter ) ) ]
        [ UsedImplicitly ]
        public List < object > Content { get ; } = new List < object > ( ) ;

        /// <summary>Gets or sets the lifetime scope.</summary>
        /// <value>The lifetime scope.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LifetimeScope
        public ILifetimeScope LifetimeScope { get ; set ; }

        /// <summary>Adds a child object.</summary>
        /// <param name="value">The child object to add.</param>
        public void AddChild ( object value )
        {
            Content.Add ( value ) ;
            Logger.Info ( "${value}" ) ;
            // throw new System.NotImplementedException ( ) ;
        }

        /// <summary>Adds the text content of a node to the object.</summary>
        /// <param name="text">The text to add to the object.</param>
        public void AddText ( string text ) { throw new NotImplementedException ( ) ; }
    }
}