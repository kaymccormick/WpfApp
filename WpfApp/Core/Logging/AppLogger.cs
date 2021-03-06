﻿using System.Collections.Generic ;
using System.Xaml ;
using NLog ;

namespace WpfApp.Core.Logging
{
    /// <summary>
    ///     Experimental class used as an attempt to initialize loggers
    ///     through XAML.
    /// </summary>
    /// <seealso cref="System.Xaml.IAttachedPropertyStore" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for AppLogger
    public class AppLogger : IAttachedPropertyStore
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public AppLogger ( )
        {
            LogManager.GetCurrentClassLogger ( ).Info ( "q??" ) ;
            System.Diagnostics.Debug.WriteLine ( "debug output for AppLogger here" ) ;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public AppLogger ( ILogger loggerInstance ) { LoggerInstance = loggerInstance ; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public AppLogger ( string arg ) { Arg = arg ; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        /// <summary>Gets or sets the argument.</summary>
        /// <value>The argument.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Arg
        public string Arg { get ; set ; }

        /// <summary>Gets or sets the logger instance.</summary>
        /// <value>The logger instance.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LoggerInstance
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ILogger LoggerInstance { get ; }

        /// <summary>
        ///     Copies all attachable member/value pairs from this attachable member
        ///     store into a destination array.
        /// </summary>
        /// <param name="array">
        ///     The destination array. The array is a generic array, should
        ///     be passed undimensioned, and should have components of
        ///     <see cref="System.Xaml.AttachableMemberIdentifier" /> and
        ///     <see langword="object" />.
        /// </param>
        /// <param name="index">The source index where copying should begin.</param>
        public void CopyPropertiesTo (
            KeyValuePair < AttachableMemberIdentifier , object >[] array
          , int                                                    index
        )
        {
            AttachablePropertyServices.CopyPropertiesTo ( this , array , index ) ;
        }

        /// <summary>
        ///     Removes the entry for the specified attachable member from this
        ///     attachable member store.
        /// </summary>
        /// <param name="attachableMemberIdentifier">
        ///     The XAML type system identifier for
        ///     the attachable member entry to remove.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if an attachable member entry for
        ///     <paramref name="attachableMemberIdentifier" /> was found in the store
        ///     and
        ///     removed; otherwise, <see langword="false" />.
        /// </returns>
        public bool RemoveProperty ( AttachableMemberIdentifier attachableMemberIdentifier )
        {
            return AttachablePropertyServices.RemoveProperty ( this , attachableMemberIdentifier ) ;
        }

        /// <summary>
        ///     Sets a value for the specified attachable member in the specified
        ///     store.
        /// </summary>
        /// <param name="attachableMemberIdentifier">
        ///     The XAML type system identifier for
        ///     the attachable member entry to set.
        /// </param>
        /// <param name="value">The value to set.</param>
        public void SetProperty (
            AttachableMemberIdentifier attachableMemberIdentifier
          , object                     value
        )
        {
            AttachablePropertyServices.SetProperty ( this , attachableMemberIdentifier , value ) ;
        }

        /// <summary>
        ///     Attempts to get a value for the specified attachable member in the
        ///     specified store.
        /// </summary>
        /// <param name="attachableMemberIdentifier">
        ///     The XAML type system identifier for
        ///     the attachable member entry to get.
        /// </param>
        /// <param name="value">
        ///     Out parameter. When this method returns, contains the
        ///     destination object for the value if
        ///     <paramref name="attachableMemberIdentifier" /> exists in the store and
        ///     has
        ///     a value.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if an attachable member entry for
        ///     <paramref name="attachableMemberIdentifier" /> was found in the store
        ///     and a
        ///     value was posted to <paramref name="value" />; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public bool TryGetProperty (
            AttachableMemberIdentifier attachableMemberIdentifier
          , out object                 value
        )
        {
            return AttachablePropertyServices.TryGetProperty (
                                                              this
                                                            , attachableMemberIdentifier
                                                            , out value
                                                             ) ;
        }

        /// <summary>
        ///     Gets the count of the attachable member entries in this attachable
        ///     member store.
        /// </summary>
        /// <returns>The integer count of entries in the store.</returns>
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public int PropertyCount { get ; set ; }

        // [ TypeConverter ( typeof ( LoggerInstanceConverter ) ) ]
        // ReSharper disable once UnusedMember.Global
        /// <summary>Gets the logger instance.</summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetLoggerInstance
        public static object GetLoggerInstance ( object target ) { return null ; }
    }
}