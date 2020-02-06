﻿using NLog ;
using NLog.Config ;

namespace WpfApp.Core.Logging
{
    /// <summary></summary>
    /// <seealso cref="NLog.Config.LoggingConfiguration" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for CodeConfiguration
    public class CodeConfiguration : LoggingConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:NLog.Config.LoggingConfiguration" /> class.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public CodeConfiguration ( ) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:NLog.Config.LoggingConfiguration" /> class.
        /// </summary>
        public CodeConfiguration ( LogFactory logFactory ) : base ( logFactory ) { }
    }
}