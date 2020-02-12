#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// LoggingEntityMetadataAttribute.cs
// 
// 2020-01-22-7:29 AM
// 
// ---
#endregion

using System ;
using System.ComponentModel.Composition ;

namespace WpfApp.Core.Logging
{
    /// <summary></summary>
    /// <seealso cref="System.Attribute" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for LoggingEntityMetadataAttribute
    [ MetadataAttribute ]
    public class LoggingEntityMetadataAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Attribute" />
        ///     class.
        /// </summary>
        public LoggingEntityMetadataAttribute ( Type loggingType ) { LoggingType = loggingType ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        /// <summary>Gets the type of the logging.</summary>
        /// <value>The type of the logging.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LoggingType
        public Type LoggingType { get ; }
    }
}