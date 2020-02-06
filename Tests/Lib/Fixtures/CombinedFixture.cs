﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ContainerFixture.cs
// 
// 2020-01-19-5:59 PM
// 
// ---
#endregion

using System ;
using System.Threading.Tasks ;
using JetBrains.Annotations ;
using NLog ;
using Xunit ;

namespace Tests.Lib.Fixtures
{
    [ UsedImplicitly ]
    public class CombinedFixture : IAsyncLifetime
    {
        // ReSharper disable once UnusedMember.Local
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;


        /// <summary>
        ///     Called immediately after the class has been created, before it is used.
        /// </summary>
        public Task InitializeAsync ( ) { return Task.CompletedTask ; }

        /// <summary>
        ///     Called when an object is no longer needed. Called just before
        ///     <see cref="M:System.IDisposable.Dispose" />
        ///     if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( ) { throw new NotImplementedException ( ) ; }
    }
}