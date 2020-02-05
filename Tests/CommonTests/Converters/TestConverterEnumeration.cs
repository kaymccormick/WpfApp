﻿using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Linq ;
using System.Net.Sockets ;
using System.Reflection ;
using System.Text ;
using System.Threading.Tasks ;
using System.Windows.Data ;
using TestLib.Attributes ;
using Xunit ;
using Xunit.Abstractions ;
using TypeConverter = System.ComponentModel.TypeConverter ;

namespace CommonTests.Converters
{
    /// <summary>Test class for discovering converters and evluating them</summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TestConverterEnumeration
    [LogTestMethod, BeforeAfterLogger]
    public class TestConverterEnumeration
    {
        private readonly ITestOutputHelper _output ;

        private static string GetCompanyForAssembly ( Assembly refAssembly )
        {
            var attribs =
                refAssembly.GetCustomAttributes ( typeof ( AssemblyCompanyAttribute ) , true ) ;
            var theCompany = "" ;
            if ( attribs.Length > 0 )
            {
                theCompany = ( ( AssemblyCompanyAttribute ) attribs[ 0 ] ).Company ;
            }

            return theCompany ;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestConverterEnumeration ( ITestOutputHelper output ) { _output = output ; }

        [ Fact ]
        public void TestConverterEnumeration1 ( )
        {
            var refs = LoadReferenced ( ) ;
            foreach ( var (item1 , item2) in refs
                                            .SelectMany ( assembly => assembly.GetTypes ( ) )
                                            .SelectMany (
                                                         ( type , i )
                                                             => type.GetCustomAttributes ( typeof ( TypeConverterAttribute ))
                                                                    .Select (
                                                                             ( attribute , i1 )
                                                                                 => new Tuple < Type
                                                                                   , Attribute > (
                                                                                                  type
                                                                                                , attribute
                                                                                                 )
                                                                            )
                                                        ) )
            {
                var type = Type.GetType ( ( item2 as TypeConverterAttribute ).ConverterTypeName ) ;

                _output.WriteLine ( $"{item1}" ) ;
                _output.WriteLine ( "\t" + type.FullName ) ;
                _output.WriteLine ( "" ) ;
            }
            var converters = GetConverterTypes ( out var types ) ;
            //DumpEnumerable ( converters ) ;
            foreach ( var converter in converters.SelectMany ( GetConverts ) )
            {
                ValueConversionAttribute x = converter.Attr ;
                _output.WriteLine ( $"\t{converter.ConverterType} converts {x.SourceType} => {x.TargetType}" ) ;
            }
            
            // _output.WriteLine (
                               // String.Join (
                                            // ". "
                                          // , types.Select ( ( type , i ) => type.FullName )
                                           // )
                              // ) ;
        }

        private void DumpEnumerable<T> ( IEnumerable < T > en, string header = null )
        {
            _output.WriteLine ( $"Enumerable: {header}" ) ;
            _output.WriteLine ( String.Join ( ", " , en ) ) ;
        }

        private static IEnumerable<ConverterInfo> GetConverts ( Type converter )
        {
            if(typeof(TypeConverter).IsAssignableFrom(converter))
            {

            }
            return Attribute.GetCustomAttributes ( converter , typeof ( ValueConversionAttribute ) )
                            .Select (
                                     ( attribute , i )
                                         => new ConverterInfo
                                            {
                                                Attr          = ( ValueConversionAttribute ) attribute
                                              , ConverterType = converter
                                            }
                                    ) ;

        }

        private IEnumerable < Type > GetConverterTypes ( out IEnumerable < Type > types )
        {
            var company = GetCompanyForAssembly ( Assembly.GetExecutingAssembly ( ) ) ;
            //utput.WriteLine ( $"Company is {company}" ) ;
            var refs = LoadReferenced ( ) ;

            //_output.WriteLine ( String.Join ( ", " , refs.Select ( Selector ) ) ) ;
            //OutputAssemblyNames ( refs ) ;
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies ( ) ;
            //OutputAssemblies ( assemblies ) ;
            types = refs
                   .Where ( ( assembly ,      i ) => GetCompanyForAssembly ( assembly ) == company )
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
            return typeof ( IValueConverter ).IsAssignableFrom ( arg ) || typeof(TypeConverter).IsAssignableFrom(arg);
        }

        private string Selector ( AssemblyName name , int i )
        {
            var loaded = Assembly.Load ( name ) ;
            return GetCompanyForAssembly ( loaded ) ;
        }

        private void OutputAssemblyNames ( AssemblyName[] getReferencedAssemblies )
        {
            _output.WriteLine (
                               "Assemblies: "
                               + String.Join (
                                              ", "
                                            , getReferencedAssemblies.Select (
                                                                 ( assemblyName , i )
                                                                     => assemblyName.Name
                                                                )
                                             )
                              ) ;
            _output.WriteLine ( "" ) ;
        }

        private void OutputAssemblies ( Assembly[] assemblies )
        {
            _output.WriteLine (
                               "Assemblies: "
                               + String.Join (
                                              ", "
                                            , assemblies.Select (
                                                                 ( assembly , i )
                                                                     => assembly.GetName ( ).Name
                                                                )
                                             )
                              ) ;
            _output.WriteLine ( "" ) ;
        }
    }

    internal class ConverterInfo
    {
        public ValueConversionAttribute Attr { get ; set ; }

        public Type ConverterType
            { get ; set ; }
    }
}