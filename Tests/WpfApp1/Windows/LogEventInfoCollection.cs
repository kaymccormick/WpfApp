﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// LogEventInfoCollection.cs
// 
// 2020-01-25-9:36 PM
// 
// ---
#endregion
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using DynamicData.Annotations ;
using NLog ;

namespace WpfApp1.Windows
{
    public class LogEventInfoCollection : ObservableCollection < LogEventInfo >
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
        ///     class.
        /// </summary>
        public LogEventInfoCollection ( ) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
        ///     class that contains elements copied from the specified list.
        /// </summary>
        /// <param name="list">The list from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="list" />
        ///     parameter cannot be <see langword="null" />.
        /// </exception>
        public LogEventInfoCollection ( [ NotNull ] List < LogEventInfo > list ) : base ( list ) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
        ///     class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The
        ///     <paramref name="collection" /> parameter cannot be
        ///     <see langword="null" />.
        /// </exception>
        public LogEventInfoCollection ( [ NotNull ] IEnumerable < LogEventInfo > collection ) :
            base ( collection )
        {
        }
    }
}