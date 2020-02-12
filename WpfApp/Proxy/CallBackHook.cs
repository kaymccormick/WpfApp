#region header
// Kay McCormick (mccor)
// 
// WpfApp
// WpfApp
// CallBackHook.cs
// 
// 2020-02-06-9:57 PM
// 
// ---
#endregion
using System ;
using System.Reflection ;
using Castle.DynamicProxy ;
using NLog ;

namespace WpfApp.Proxy
{
    internal class CallBackHook : IProxyGenerationHook
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>
        ///   Invoked by the generation process to notify that the whole process has completed.
        /// </summary>
        public void MethodsInspected ( ) { }

        /// <summary>
        ///   Invoked by the generation process to notify that a member was not marked as virtual.
        /// </summary>
        /// <param name="type">The type which declares the non-virtual member.</param>
        /// <param name="memberInfo">The non-virtual member.</param>
        /// <remarks>
        ///   This method gives an opportunity to inspect any non-proxyable member of a type that has
        ///   been requested to be proxied, and if appropriate - throw an exception to notify the caller.
        /// </remarks>
        public void NonProxyableMemberNotification ( Type type , MemberInfo memberInfo )
        {
            Logger.Warn( $"cant proxy {memberInfo.Name}" ) ;
        }

        /// <summary>
        ///   Invoked by the generation process to determine if the specified method should be proxied.
        /// </summary>
        /// <param name="type">The type which declares the given method.</param>
        /// <param name="methodInfo">The method to inspect.</param>
        /// <returns>True if the given method should be proxied; false otherwise.</returns>
        public bool ShouldInterceptMethod ( Type type , MethodInfo methodInfo )
        {
            Logger.Info( $"can proxy {methodInfo.Name}" ) ;
            return true ;
        }
    }
}