﻿using System ;
using System.Linq ;
using System.Reflection ;
using System.Threading.Tasks ;
using System.Windows ;
using JetBrains.Annotations ;
using KayMcCormick.Dev.Test.Metadata ;
using Xunit ;
using Application = System.Windows.Application ;

namespace TestLib.Fixtures
{
    /// <summary></summary>
    /// <seealso cref="Xunit.IAsyncLifetime" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for WpfApplicationFixture
    [UsedImplicitly]
    public class WpfApplicationFixture : IAsyncLifetime
    {
        private readonly WpfApplicationHelper _wpfApplicationHelper ;

        /// <summary>Gets or sets the base pack URI.</summary>
        /// <value>The base pack URI.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for BasePackUri
        public Uri BasePackUri
        {
            get => _wpfApplicationHelper.BasePackUri ;
            set => _wpfApplicationHelper.BasePackUri = value ;
        }

        /// <summary>Gets or sets my application.</summary>
        /// <value>My application.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MyApp
        public Application MyApp
        {
            get
            {
                if ( _wpfApplicationHelper != null )
                {
                    return _wpfApplicationHelper.MyApp;
                }

                return null ;
            }
            set
            {
                if ( _wpfApplicationHelper != null )
                {
                    _wpfApplicationHelper.MyApp = value ;
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public WpfApplicationFixture ( )
        {
            var qq = AppDomain.CurrentDomain.GetAssemblies ( )
                              .Where (
                                      a => Attribute.IsDefined (
                                                                ( Assembly ) a
                                                              , typeof ( WpfTestApplicationAttribute
                                                                )
                                                               )
                                     ) ;
            if ( Application.Current != null ) {
                var assembly = Application.Current.GetType().Assembly ;
                Assert.True (
                             Attribute.IsDefined ( assembly , typeof ( WpfTestApplicationAttribute ) )
                            ) ;
                _wpfApplicationHelper = new WpfApplicationHelper ( assembly ) ;
            }
        }

        /// <summary>Called immediately after the class has been created, before it is used.</summary>
        /// <returns></returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InitializeAsync
        public Task InitializeAsync ( )
        {
            //Logger.Debug($"{nameof(InitializeAsync)}"  );
            if ( _wpfApplicationHelper != null )
            {
                return _wpfApplicationHelper.InitializeAsync ( ) ;
            } else
            {
                return Task.CompletedTask ;

            }
        }

        /// <summary>
        ///     Called when an object is no longer needed. Called just before
        ///     <see cref="M:System.IDisposable.Dispose" />
        ///     if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( )
        {
            //Logger.Debug($"{nameof(InitializeAsync)}");
            if ( _wpfApplicationHelper != null )
            {
                return _wpfApplicationHelper.DisposeAsync ( ) ;
            }

            return Task.CompletedTask ;
        }
    }
}