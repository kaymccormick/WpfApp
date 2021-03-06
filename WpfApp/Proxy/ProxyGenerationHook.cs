﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// ProxyGenerationHook.cs
// 
// 2020-01-18-3:40 PM
// 
// ---
#endregion

using System ;
using System.Diagnostics.CodeAnalysis ;
using System.Reflection ;
using Castle.DynamicProxy ;
using NLog ;

namespace WpfApp.Proxy
{
    /// <summary></summary>
    /// <seealso cref="Castle.DynamicProxy.IProxyGenerationHook" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for ProxyGenerationHook
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        [ SuppressMessage (
                              "Code Quality"
                            , "IDE0052:Remove unread private members"
                            , Justification = "<Pending>"
                          ) ]
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>Invoked by the generation process to notify that a member was not marked as virtual.</summary>
        /// <param name="type">The type which declares the non-virtual member.</param>
        /// <param name="memberInfo">The non-virtual member.</param>
        /// <remarks>This method gives an opportunity to inspect any non-proxyable member of a type that has
        /// been requested to be proxied, and if appropriate - throw an exception to notify the caller.</remarks>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for NonProxyableMemberNotification
        public void NonProxyableMemberNotification ( Type type , MemberInfo memberInfo )
        {
            Logger.Debug (
                          $"{nameof ( NonProxyableMemberNotification )}: type={type}, memberInfo={memberInfo}"
                         ) ;
        }

        /// <summary>Boolean to indicate whether the methods should be intercepted.</summary>
        /// <param name="type">The type.</param>
        /// <param name="memberInfo">The member information.</param>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ShouldInterceptMethod
        public bool ShouldInterceptMethod ( Type type , MethodInfo memberInfo )
        {
            Logger.Debug (
                          $"{nameof ( ShouldInterceptMethod )}: type={type}, memberInfo={memberInfo}"
                         ) ;
            return memberInfo != null && memberInfo.Name.StartsWith ( "get_" , StringComparison.Ordinal ) ;
        }

        /// <summary>Invoked by the generation process to notify that the whole process has completed.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MethodsInspected
        public void MethodsInspected ( ) { Logger.Debug ( $"{nameof ( MethodsInspected )}" ) ; }
    }
}