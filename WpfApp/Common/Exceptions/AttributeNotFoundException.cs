﻿using System ;

namespace Common.Exceptions
{
    /// <summary></summary>
    /// <seealso cref="System.Exception" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for AttributeNotFoundException
    public class AttributeNotFoundException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AttributeNotFoundException" /> class.
        /// </summary>
        /// <param name="attName">Name of the att.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public AttributeNotFoundException ( string attName ) { AttributeName = attName ; }

        /// <summary>Gets the name of the attribute.</summary>
        /// <value>The name of the attribute.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for AttributeName
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public string AttributeName { get ; private set ; }
    }
}