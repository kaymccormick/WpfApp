﻿using System ;
using System.Runtime.CompilerServices ;

namespace WpfApp.Application
{
    /// <summary></summary>
    /// <seealso cref="System.EventArgs" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for DebugEventArgs
    public class DebugEventArgs : EventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.EventArgs"/> class.</summary>
        /// <param name="message"></param>
        /// <param name="callerMemberName"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        public DebugEventArgs (
            string                      message
          , [ CallerMemberName ] string callerMemberName = ""
          , [ CallerFilePath ]   string callerFilePath   = ""
          , [ CallerLineNumber ] int    callerLineNumber = 0
        )
        {
            Message = message ;
            CallerMemberName = callerMemberName ;
            CallerFilePath = callerFilePath ;
            CallerLineNumber = callerLineNumber ;
        }

        /// <summary>Gets the message.</summary>
        /// <value>The message.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Message
        public string Message { get ; }

        /// <summary>Gets the name of the caller member.</summary>
        /// <value>The name of the caller member.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CallerMemberName
        public string CallerMemberName { get ; }

        /// <summary>Gets the caller file path.</summary>
        /// <value>The caller file path.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CallerFilePath
        public string CallerFilePath { get ; }

        /// <summary>Gets the caller line number.</summary>
        /// <value>The caller line number.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CallerLineNumber
        public int CallerLineNumber { get ; }
    }
}