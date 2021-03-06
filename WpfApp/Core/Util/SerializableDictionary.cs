﻿using System ;
using System.Collections.Generic ;
using System.Runtime.Serialization ;
using System.Xml ;
using System.Xml.Schema ;
using System.Xml.Serialization ;

namespace WpfApp.Core.Util
{
    /// <summary></summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="System.Collections.Generic.Dictionary{TKey, TValue}" />
    /// <seealso cref="System.Xml.Serialization.IXmlSerializable" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for SerializableDictionary`2
    [XmlRoot ( "dictionary" ) ]
    [Serializable]
    public class SerializableDictionary < TKey , TValue > : Dictionary < TKey , TValue >
      , IXmlSerializable
    {
        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary ( ) { }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary`2 whose elements are copied to the new System.Collections.Generic.Dictionary`2.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary ( IDictionary < TKey , TValue > dictionary ) :
            base ( dictionary )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="dictionary">The System.Collections.Generic.IDictionary`2 whose elements are copied to the new System.Collections.Generic.Dictionary`2.</param>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary (
            IDictionary < TKey , TValue > dictionary
          , IEqualityComparer < TKey >    comparer
        ) : base ( dictionary , comparer )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary ( IEqualityComparer < TKey > comparer ) : base ( comparer )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="capacity">The initial number of elements that the System.Collections.Generic.Dictionary`2 can contain.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary ( int capacity ) : base ( capacity ) { }

        /// <summary>Initializes a new instance of the <see cref="SerializableDictionary{TKey, TValue}"/> class.</summary>
        /// <param name="capacity">The initial number of elements that the System.Collections.Generic.Dictionary`2 can contain.</param>
        /// <param name="comparer">
        /// The System.Collections.Generic.IEqualityComparer`1 implementation to use when comparing keys, or <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> to use the default System.Collections.Generic.EqualityComparer`1 for the type of the key.
        /// </param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public SerializableDictionary ( int capacity , IEqualityComparer < TKey > comparer ) :
            base ( capacity , comparer )
        {
        }

        #region IXmlSerializable Members

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SerializableDictionary ( SerializationInfo info , StreamingContext context ) : base ( info , context )
        {
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the <span class="keyword">IXmlSerializable</span> interface, you should return <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> (<span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetSchema
        public XmlSchema GetSchema ( ) { return null ; }

        /// <summary>Generates an object from its XML representation.</summary>
        /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ReadXml
        public void ReadXml ( XmlReader reader )
        {
            var keySerializer = new XmlSerializer ( typeof ( TKey ) ) ;
            var valueSerializer = new XmlSerializer ( typeof ( TValue ) ) ;

            var wasEmpty = reader.IsEmptyElement ;
            reader.Read ( ) ;

            if ( wasEmpty )
            {
                return ;
            }

            while ( reader.NodeType != XmlNodeType.EndElement )
            {
                reader.ReadStartElement ( "item" ) ;

                reader.ReadStartElement ( "key" ) ;
                var key = ( TKey ) keySerializer.Deserialize ( reader ) ;
                reader.ReadEndElement ( ) ;

                reader.ReadStartElement ( "value" ) ;
                var value = ( TValue ) valueSerializer.Deserialize ( reader ) ;
                reader.ReadEndElement ( ) ;

                Add ( key , value ) ;

                reader.ReadEndElement ( ) ;
                reader.MoveToContent ( ) ;
            }

            reader.ReadEndElement ( ) ;
        }

        /// <summary>Converts an object into its XML representation.</summary>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for WriteXml
        public void WriteXml ( XmlWriter writer )
        {
            var keySerializer = new XmlSerializer ( typeof ( TKey ) ) ;
            var valueSerializer = new XmlSerializer ( typeof ( TValue ) ) ;

            foreach ( var key in Keys )
            {
                writer.WriteStartElement ( "item" ) ;

                writer.WriteStartElement ( "key" ) ;
                keySerializer.Serialize ( writer , key ) ;
                writer.WriteEndElement ( ) ;

                writer.WriteStartElement ( "value" ) ;
                var value = this[ key ] ;
                valueSerializer.Serialize ( writer , value ) ;
                writer.WriteEndElement ( ) ;

                writer.WriteEndElement ( ) ;
            }
        }
        #endregion
    }
}