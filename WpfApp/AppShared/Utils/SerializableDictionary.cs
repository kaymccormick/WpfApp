﻿using System.Collections.Generic ;
using System.Xml ;
using System.Xml.Schema ;
using System.Xml.Serialization ;

namespace AppShared.Utils
{
	[ XmlRoot ( "dictionary" ) ]
	public class SerializableDictionary < TKey , TValue > : Dictionary < TKey , TValue >
	  , IXmlSerializable
	{
		public SerializableDictionary ( ) { }

		public SerializableDictionary ( IDictionary < TKey , TValue > dictionary ) :
			base ( dictionary )
		{
		}

		public SerializableDictionary (
			IDictionary < TKey , TValue > dictionary
		  , IEqualityComparer < TKey >    comparer
		) : base ( dictionary , comparer )
		{
		}

		public SerializableDictionary ( IEqualityComparer < TKey > comparer ) : base ( comparer )
		{
		}

		public SerializableDictionary ( int capacity ) : base ( capacity ) { }

		public SerializableDictionary ( int capacity , IEqualityComparer < TKey > comparer ) :
			base ( capacity , comparer )
		{
		}

		#region IXmlSerializable Members
		public XmlSchema GetSchema ( ) { return null ; }

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