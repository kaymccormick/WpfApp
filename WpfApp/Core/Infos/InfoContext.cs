﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// InfoContext.cs
// 
// 2020-01-20-7:03 AM
// 
// ---
#endregion

using System ;
using System.Collections ;
using System.Collections.Generic ;

namespace WpfApp.Core.Infos
{
    /// <summary></summary>
    /// <seealso cref="object" />
    /// <seealso cref="object" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for InfoContext
    public class InfoContext : Tuple < string , object > , IEnumerable < object >
    {
        /// <summary>Factory delegate for producing InfoContext objects.</summary>
        /// <param name="name">The name.</param>
        /// <param name="objectContext">The object context.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Factory
        public delegate InfoContext Factory ( string name , object objectContext ) ;

        /// <summary>Initializes a new instance of the <see cref="InfoContext" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="objectContext">The object context.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public InfoContext ( string name , object objectContext ) : base ( name , objectContext )
        {
            // Name = name;
            // ObjectContext = objectContext;
        }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Name
        public string Name => Item1 ;

        /// <summary>Gets the object context.</summary>
        /// <value>The object context.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ObjectContext
        public object ObjectContext => Item2 ;

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator < object > GetEnumerator ( )
        {
            yield return Name ;
            yield return ObjectContext ;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>
        ///     An <see cref="System.Collections.IEnumerator" /> object that can be
        ///     used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator ( ) { return GetEnumerator ( ) ; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString ( )
        {
            var methodInfo =
                ObjectContext.GetType ( )
                             .GetMethod (
                                         "ToString"
                                       , Type.EmptyTypes
                                        ) ; //, BindingFlags.Public | BindingFlags.Instance);
            var s = methodInfo != null && methodInfo.DeclaringType == typeof ( object )
                        ? ObjectContext.GetType ( ).Name
                        : ObjectContext.ToString ( ) ;

            return Name + "=" + s ;
        }
    }
}