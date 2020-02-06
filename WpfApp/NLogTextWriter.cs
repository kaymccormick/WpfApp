﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// NLogTextWriter.cs
// 
// 2020-01-25-1:10 PM
// 
// ---
#endregion
using System.IO ;
using System.Text ;
using NLog ;

namespace WpfApp
{
    public class NLogTextWriter : TextWriter
    {
        public NLogTextWriter ( Logger logger ) { Logger = logger ; }

        private Logger Logger { get ; }

        /// <summary>
        ///     When overridden in a derived class, returns the character encoding in
        ///     which the output is written.
        /// </summary>
        /// <returns>The character encoding in which the output is written.</returns>
        public override Encoding Encoding { get ; } = Encoding.UTF8 ;

        /// <summary>
        ///     Writes a string followed by a line terminator to the text string or
        ///     stream.
        /// </summary>
        /// <param name="value">
        ///     The string to write. If <paramref name="value" /> is
        ///     <see langword="null" />, only the line terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">
        ///     The
        ///     <see cref="T:System.IO.TextWriter" /> is closed.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        public override void WriteLine ( string value ) { Logger.Warn ( value ) ; }
    }
}