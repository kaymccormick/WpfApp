﻿using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Linq ;
using Autofac ;
using Autofac.Core ;
using Tests.Attributes ;
using Tests.Lib.Fixtures ;
using Tests.Lib.Misc ;
using WpfApp.Core.Converters ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Model ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main.Converters
{
    /// <summary>
    ///     Test class for <see cref="InstanceRegistrationConverter" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{T}" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for InstanceRegistrationConverterTests
    [ BeforeAfterLogger ] [ LogTestMethod ] [Collection("GeneralPurpose")]
    public class InstanceRegistrationConverterTests : IClassFixture < TestContainerFixture >, IClassFixture <LoggingFixture>, IDisposable
    {
        /// <summary>The fixture</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _fixture
        private readonly TestContainerFixture _fixture ;

        /// <summary>The output</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _output
        private readonly ITestOutputHelper _output ;

        private readonly LoggingFixture _loggingFixture ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public InstanceRegistrationConverterTests (
            TestContainerFixture fixture
          , ITestOutputHelper    output
            , LoggingFixture loggingFixture
        )
        {
            _fixture = fixture ;
            loggingFixture.SetOutputHelper(output);

            _output = output ;
            _loggingFixture = loggingFixture ;
        }

        /// <summary>Tests the conversion1.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestConversion1
        [ WpfFact ]
        public void LazyLoadButton ( )
        {
            // takes IComponentLifetime

            var lazy = _fixture.Container.Resolve < Lazy < IRandom > > ( ) ;
            var objIdProv = _fixture.Container.Resolve < IObjectIdProvider > ( ) ;

            var instConv =
                new RegistrationConverter (
                                           _fixture.Container
                                         , objIdProv
                                          ) ; // TypeDescriptor.GetConverter ( typeof (IComponentRegistration) ) ;
            Assert.NotNull ( instConv ) ;
            _output.WriteLine ( $"{instConv}" ) ;

            var converter = new InstanceRegistrationConverter ( ) ;

            var regs = _fixture.Container.ComponentRegistry.Registrations ;
            var regsAry = regs as IComponentRegistration[] ?? regs.ToArray ( ) ;
            DumpRegistrations ( regsAry ) ;

            var serviceType = lazy.GetType ( ) ;
            _output.WriteLine ( $"serviceType is {serviceType}" ) ;
            var value = FindTypedService ( regsAry , serviceType ) ;
            if ( value == null )
            {
                throw new ArgumentNullException ( ) ;
            }

            _output.WriteLine ( $"typed service is ${value}" ) ;
            var myVal = instConv.Convert (
                                          value
                                        , null
                                        , _fixture.Container
                                        , CultureInfo.CurrentCulture
                                         ) ;

            Assert.NotNull ( myVal ) ;
            Assert.IsAssignableFrom < IList < InstanceRegistration > > ( myVal ) ;
            var list = ( IList < InstanceRegistration > ) myVal ;
            Assert.Single ( list ) ;
            Assert.Collection (
                               list
                             , info => {
                                   Assert.NotNull ( info.Instance ) ;
                               }
                              ) ;

            var instReg = list[ 0 ] ;
            _output.WriteLine ( $"Passing {instReg} into {converter}" ) ;
            var result = converter.Convert (
                                            instReg
                                          , typeof ( IEnumerable )
                                          , null
                                          , CultureInfo.CurrentCulture
                                           ) ;
            Assert.NotNull ( result ) ;
            var enumerable = ( IEnumerable ) result ;
            var collection = enumerable.Cast < object > ( ).ToList ( ) ;
            Assert.NotEmpty ( collection ) ;
            foreach ( var o in collection )
            {
                _output.WriteLine ( $"{o}" ) ;
            }
        }

        private static IComponentRegistration FindTypedService (
            IComponentRegistration[] regsAry
          , Type                     serviceType
        )
        {
            var regs = regsAry.Where (
                                      ( registration , i )
                                          => registration.Services.Any (
                                                                        service
                                                                            => service is
                                                                                   TypedService t
                                                                               && t.ServiceType
                                                                               == serviceType
                                                                       )
                                     ) ;
            var componentRegistrations = regs.ToList ( ) ;
            if ( ! componentRegistrations.Any ( ) )
            {
                return null ;
            }

            var value = componentRegistrations.First ( ) ;
            return value ;
        }

        /// <summary>Dumps the registrations.</summary>
        /// <param name="regs">The regs.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DumpRegistrations
        private void DumpRegistrations ( IEnumerable < IComponentRegistration > regs )
        {
            _output.WriteLine ( "--" ) ;
            try
            {
                foreach ( var reg in regs )
                {
                    _output.WriteLine ( $"{reg.Id}" ) ;
                    _output.WriteLine ( $"{reg.Activator.LimitType.FullName}" ) ;
                    foreach ( var regService in reg.Services )
                    {
                        if ( regService is TypedService t )
                        {
                            var tServiceType = t.ServiceType ;
                            if ( tServiceType.IsGenericType
                                 && tServiceType.GetGenericTypeDefinition ( )
                                 == typeof ( Lazy < object > ).GetGenericTypeDefinition ( ) )
                            {
                                _output.WriteLine (
                                                   $"  Lazy {tServiceType.GenericTypeArguments[ 0 ]}"
                                                  ) ;
                            }

                            _output.WriteLine ( $"  {t.Description}:\t{tServiceType.FullName}" ) ;
                        }
                        else
                        {
                            _output.WriteLine ( $"  {regService.Description}:\t{regService}" ) ;
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                _output.WriteLine ( $"{nameof ( DumpRegistrations )} exception {ex.Message}" ) ;
            }
            finally
            {
                _output.WriteLine ( "--" ) ;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( )
        {
            // _xunitTarget?.Dispose ( ) ;
            _loggingFixture.SetOutputHelper(null);
        }
    }
}