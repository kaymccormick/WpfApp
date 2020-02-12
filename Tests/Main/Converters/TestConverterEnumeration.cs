﻿using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Linq ;
using System.Reflection ;
using System.Windows.Data ;
using NLog ;
using Tests.Attributes ;
using Tests.Lib.Fixtures ;
using Tests.Lib.Utils ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main.Converters
{
    /// <summary>Test class for discovering converters and evaluating them</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestConverterEnumeration
    [ LogTestMethod ] [ BeforeAfterLogger ][Collection("GeneralPurpose")]
    public class TestConverterEnumeration : IClassFixture < LoggingFixture> , IDisposable
    {
        private readonly LoggingFixture _loggingFixture ;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public TestConverterEnumeration ( ITestOutputHelper output, LoggingFixture loggingFixture)
        {
            _loggingFixture = loggingFixture ;
            loggingFixture.SetOutputHelper ( output ) ;
        }

        private static string GetCompanyForAssembly ( Assembly refAssembly )
        {
            var attributes =
                refAssembly.GetCustomAttributes ( typeof ( AssemblyCompanyAttribute ) , true ) ;
            var theCompany = "" ;
            if ( attributes.Length > 0 )
            {
                theCompany = ( ( AssemblyCompanyAttribute ) attributes[ 0 ] ).Company ;
            }

            return theCompany ;
        }

        // ReSharper disable once UnusedMember.Local
        private void DumpEnumerable < T > ( IEnumerable < T > en , string header = null )
        {
            Logger.Debug ( $"Enumerable: {header}" ) ;
            Logger.Debug ( string.Join ( ", " , en ) ) ;
        }

        private static IEnumerable < ConverterInfo > GetConverts ( Type converter )
        {
            if ( typeof ( TypeConverter ).IsAssignableFrom ( converter ) )
            {
            }

            return Attribute.GetCustomAttributes ( converter , typeof ( ValueConversionAttribute ) )
                            .Select (
                                     ( attribute , i )
                                         => new ConverterInfo
                                            {
                                                Attr =
                                                    ( ValueConversionAttribute ) attribute
                                              , ConverterType = converter
                                            }
                                    ) ;
        }

        private IEnumerable < Type > GetConverterTypes ( out IEnumerable < Type > types )
        {
            var company = GetCompanyForAssembly ( Assembly.GetExecutingAssembly ( ) ) ;
            var refs = LoadReferenced ( ) ;

            //Logger.Debug ( String.Join ( ", " , refs.Select ( Selector ) ) ) ;
            //OutputAssemblyNames ( refs ) ;
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies ( ) ;
            //OutputAssemblies ( assemblies ) ;
            types = refs.Where (
                                ( assembly , i )
                                    => GetCompanyForAssembly ( assembly ) == company
                               )
                        .SelectMany ( ( assembly , i ) => assembly.GetExportedTypes ( ) ) ;
            var converters = types.Where ( IsConverter ) ;
            return converters ;
        }

        private static IEnumerable < Assembly > LoadReferenced ( )
        {
            return Assembly.GetExecutingAssembly ( )
                           .GetReferencedAssemblies ( )
                           .Select ( ( name , i ) => Assembly.Load ( name ) ) ;
        }

        private bool IsConverter ( Type arg )
        {
            return typeof ( IValueConverter ).IsAssignableFrom ( arg )
                   || typeof ( TypeConverter ).IsAssignableFrom ( arg ) ;
        }

        // ReSharper disable once UnusedMember.Local
        private string Selector ( AssemblyName name )
        {
            var loaded = Assembly.Load ( name ) ;
            return GetCompanyForAssembly ( loaded ) ;
        }

        // ReSharper disable once UnusedMember.Local
        private void OutputAssemblyNames ( AssemblyName[] getReferencedAssemblies )
        {
            Logger.Debug (
                               "Assemblies: "
                               + string.Join (
                                              ", "
                                            , getReferencedAssemblies.Select (
                                                                              ( assemblyName , i )
                                                                                  => assemblyName
                                                                                     .Name
                                                                             )
                                             )
                              ) ;
            Logger.Debug ( "" ) ;
        }

        // ReSharper disable once UnusedMember.Local
        private void OutputAssemblies ( Assembly[] assemblies )
        {
            Logger.Debug (
                               "Assemblies: "
                               + string.Join (
                                              ", "
                                            , assemblies.Select (
                                                                 ( assembly , i )
                                                                     => assembly.GetName ( ).Name
                                                                )
                                             )
                              ) ;
            Logger.Debug ( "" ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        [ Fact ]
        public void TestConverterEnumeration1 ( )
        {
            var refs = LoadReferenced ( ) ;
            foreach ( var (item1 , item2) in refs
                                            .SelectMany ( assembly => assembly.GetTypes ( ) )
                                            .SelectMany (
                                                         ( type , i )
                                                             => type
                                                               .GetCustomAttributes (
                                                                                     typeof (
                                                                                         TypeConverterAttribute
                                                                                     )
                                                                                    )
                                                               .Select (
                                                                        ( attribute , i1 )
                                                                            => new Tuple < Type ,
                                                                                Attribute > (
                                                                                             type
                                                                                           , attribute
                                                                                            )
                                                                       )
                                                        ) )
            {
                var type = Type.GetType ( ( ( TypeConverterAttribute ) item2 ).ConverterTypeName ) ;

                Logger.Debug ( $"{item1}" ) ;
                if ( type != null )
                {
                    Logger.Debug ( "\t" + type.FullName ) ;
                }

                Logger.Debug ( "" ) ;
            }

            var converters = GetConverterTypes ( out _ ) ;
            //DumpEnumerable ( converters ) ;
            foreach ( var converter in converters.SelectMany ( GetConverts ) )
            {
                var x = converter.Attr ;
                Logger.Debug (
                                   $"\t{converter.ConverterType} converts {x.SourceType} => {x.TargetType}"
                                  ) ;
            }

            // Logger.Debug (
            // String.Join (
            // ". "
            // , types.Select ( ( type , i ) => type.FullName )
            // )
            // ) ;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( ) { _loggingFixture.SetOutputHelper ( null ) ; }
    }
}