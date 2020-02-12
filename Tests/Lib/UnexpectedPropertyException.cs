﻿using System ;
using System.Reflection ;

namespace Tests.Lib
{
    /// <summary></summary>
    /// <seealso cref="System.Exception" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for UnexpectedPropertyException
    [Serializable]
    public class UnexpectedPropertyException : Exception
    {
        /// <summary>Gets a message that describes the current exception.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Message
        public override string Message { get ; }

        /// <summary>Gets the member information.</summary>
        /// <value>The member information.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MemberInfo
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public MemberInfo MemberInfo { get ; }

        /// <summary>Initializes a new instance of the <see cref="UnexpectedPropertyException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        public UnexpectedPropertyException ( string message , MemberInfo memberInfo )
        {
            Message   = message ;
            MemberInfo = memberInfo ;
        }
    }
}