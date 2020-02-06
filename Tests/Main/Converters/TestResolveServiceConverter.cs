﻿#region header
// Kay McCormick (mccor)
// 
// Common
// CommonTests
// TestResolveServiceConverter.cs
// 
// 2020-01-30-7:10 AM
// 
// ---
#endregion
using System ;
using System.Globalization ;
using Autofac ;

using TestLib.Attributes ;
using Tests.Lib.Fixtures ;
using WpfApp.Core ;
using WpfApp.Core.Converters ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main.Converters
{
    /// <summary></summary>
    /// <seealso cref="Xunit.IClassFixture{T}" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestResolveServiceConverter
    [ LogTestMethod ] [ BeforeAfterLogger ]
    public class TestResolveServiceConverter : IClassFixture < TestContainerFixture >
    {
        private readonly TestContainerFixture _fixture ;
        private readonly ITestOutputHelper    _output ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public TestResolveServiceConverter (
            TestContainerFixture fixture
          , ITestOutputHelper    output
        )
        {
            _fixture = fixture ;
            _output  = output ;
        }

        /// <summary>Tests the conversion1.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestConversion1
        [ WpfFact ]
        public void TestConversion1 ( )
        {
            // takes IComponentLifetime
            var svc = new ResolveService { ServiceType = typeof ( Lazy < IRandom > ) } ;

            _fixture.Container.Resolve < Lazy < IRandom > > ( ) ;

            var resolveConv = new ResolveServiceConverter ( ) ;
            Assert.NotNull ( resolveConv ) ;

            var value = resolveConv.Convert (
                                             svc
                                           , null
                                           , _fixture.Container
                                           , CultureInfo.CurrentCulture
                                            ) ;
            Assert.NotNull ( value ) ;
            Assert.IsAssignableFrom < Lazy < IRandom > > ( value ) ;
            var lazy = ( Lazy < IRandom > ) value ;
            _output.WriteLine ( $"IsValueCreated = {lazy.IsValueCreated}" ) ;
            _output.WriteLine ( $"Value = {lazy.Value}" ) ;
        }
    }
}