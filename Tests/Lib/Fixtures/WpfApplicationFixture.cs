﻿using System ;
using System.Threading.Tasks ;
using System.Windows ;
using System.Windows.Threading ;
using JetBrains.Annotations ;
using Xunit ;

namespace Tests.Lib.Fixtures
{
    /// <summary></summary>
    /// <seealso cref="Xunit.IAsyncLifetime" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for WpfApplicationFixture
    [ UsedImplicitly ]
    [Obsolete]
    public class WpfApplicationFixture : IAsyncLifetime, IWpfApplicationHelper
    {
        private readonly WpfApplicationHelper _wpfApplicationHelper = new WpfApplicationHelper ( ) ;

        /// <summary>Gets or sets the base pack URI.</summary>
        /// <value>The base pack URI.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for BasePackUri
        // ReSharper disable once UnusedMember.Global
        public Uri BasePackUri => _wpfApplicationHelper.BasePackUri ;

        /// <summary>Gets or sets my application.</summary>
        /// <value>My application.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MyApp
        Application IWpfApplicationHelper.MyApp
        {
            get => _wpfApplicationHelper.MyApp ;
            set => _wpfApplicationHelper.MyApp = value ;
        }

        /// <summary>Gets or sets the op.</summary>
        /// <value>The op.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Op
        public DispatcherOperation Op
        {
            get => _wpfApplicationHelper.Op ;
            set => _wpfApplicationHelper.Op = value ;
        }

        /// <summary>Makes the window wrap.</summary>
        /// <param name="genericType">Type of the generic.</param>
        /// <param name="wrappedType">Type of the wrapped.</param>
        /// <exception cref="System.ArgumentNullException">genericType
        /// or
        /// wrappedType</exception>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MakeWindowWrap
        public void MakeWindowWrap ( Type genericType , Type wrappedType ) { _wpfApplicationHelper.MakeWindowWrap ( genericType , wrappedType ) ; }

        /// <summary>Gets or sets the base pack URI.</summary>
        /// <value>The base pack URI.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for BasePackUri
        Uri IWpfApplicationHelper.BasePackUri
        {
            get => _wpfApplicationHelper.BasePackUri ;
            set => _wpfApplicationHelper.BasePackUri = value ;
        }

        /// <summary>Gets or sets my application.</summary>
        /// <value>My application.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MyApp
        // ReSharper disable once UnusedMember.Global
        public Application MyApp => _wpfApplicationHelper?.MyApp ;

        /// <summary>
        ///     Called immediately after the class has been created, before it is
        ///     used.
        /// </summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InitializeAsync
        public Task InitializeAsync ( ) { return _wpfApplicationHelper.InitializeAsync ( ) ; }

        /// <summary>
        ///     Called when an object is no longer needed. Called just before
        ///     <see cref="M:System.IDisposable.Dispose" />
        ///     if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( ) { return _wpfApplicationHelper.DisposeAsync ( ) ; }
    }
}