﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// AppShared
// ResolveService.cs
// 
// 2020-01-28-2:52 AM
// 
// ---
#endregion
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using WpfApp.Core ;

namespace WpfApp
{
    public class ResolveServiceList : ObservableCollection < ResolveService >
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
        ///     class.
        /// </summary>
        public ResolveServiceList ( ) { }

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
        public ResolveServiceList ( List < ResolveService > list ) : base ( list ) { }

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
        public ResolveServiceList ( IEnumerable < ResolveService > collection ) :
            base ( collection )
        {
        }
    }
}