using System ;
using System.ComponentModel.Composition ;

namespace AppShared.Attributes
{
    [ MetadataAttribute ]
    public class InterfaceMetadataAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Attribute" />
        ///     class.
        /// </summary>
        public InterfaceMetadataAttribute ( Type interfaceType ) { InterfaceType = interfaceType ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public Type InterfaceType { get ; set ; }
    }
}