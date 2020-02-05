﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MyLogBuilder.cs
// 
// 2020-01-22-2:09 PM
// 
// ---
#endregion

using NLog ;
using NLog.Fluent ;

namespace AppShared.Interfaces
{
    public class MyLogBuilder : LogBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MyLogBuilder" /> class.
        /// </summary>
        /// <param name="logger">The <see cref="NLog.Logger" /> to send the log event.</param>
        public MyLogBuilder ( ILogger logger ) : base ( logger ) { }


        /// <summary>
        ///     Initializes a new instance of the <see cref="MyLogBuilder" /> class.
        /// </summary>
        /// <param name="logger">The <see cref="NLog.Logger" /> to send the log event.</param>
        /// <param name="logLevel">The <see cref="NLog.LogLevel" /> for the log event.</param>
        public MyLogBuilder ( ILogger logger , LogLevel logLevel ) : base ( logger , logLevel ) { }
    }
}