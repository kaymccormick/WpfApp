#region header
// Kay McCormick (mccor)
// 
// PsProject
// WpfTestApp
// ProxyUtils.cs
// 
// 2020-02-01-6:58 AM
// 
// ---
#endregion
using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.ComponentModel ;
using System.IO ;
using System.Linq ;
using System.Reflection ;
using System.Threading ;
using System.Windows.Forms ;
using System.Windows.Markup ;
using System.Xaml ;
using System.Xaml.Schema ;
using System.Xml ;
using Castle.DynamicProxy ;
using XamlReader = System.Xaml.XamlReader ;

namespace WpfTestApp
{
    public class ProxyUtilsBase
    {
        private Action < string > _writeOut ;
        private IInterceptor _interceptor ;
        protected static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProxyUtilsBase ( Action < string > writeOut , IInterceptor interceptor )
        {
            _writeOut    = writeOut ;
            _interceptor = interceptor ;
            //_interceptor = new BaseInterceptorImpl ( _writeOut , _proxyGenerator ) ;
        }

        public static ProxyGenerator proxyGenerator { get ; } = _proxyGenerator ;

        public ProxyGenerator Generator => _proxyGenerator ;

        public static IInterceptor CreateInterceptor ( Action < string > @out )
        {
            return new BaseInterceptorImpl ( @out ) ;
        }

        public string DumpIt ( object instance )
        {
            var stringWriter = CreateStringWriter ( ) ;

            var xmlWriterProxy = CreateXmlWriter ( stringWriter ) ;
            var context = CreateXamlSchemaContext ( ) ;
            var settings = CreateXamlObjectReaderSettings ( ) ;
            var reader = CreateXamlObjectReader ( instance , context , settings ) ;

            var xamlWriterProxy = CreateXamlXmlWriter ( xmlWriterProxy , context ) ;


            try
            {
                XamlServices.Transform ( ( XamlReader ) reader , xamlWriterProxy , true ) ;
                //XamlServices.Save (xamlWriterProxy , this ) ;
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( ex ) ;
            }

            return stringWriter.ToString ( ) ;
        }

        public virtual XamlXmlWriter CreateXamlXmlWriter (
            XmlWriter         xmlWriterProxy
          , XamlSchemaContext context
        )
        {
            var xamlXmlWriter = new XamlXmlWriter ( xmlWriterProxy , context ) ;
            var xamlWriterProxy = ( XamlXmlWriter ) _proxyGenerator.CreateClassProxy (
                                                                                      typeof (
                                                                                          XamlXmlWriter
                                                                                      )
                                                                                    , new object[]
                                                                                      {
                                                                                          xmlWriterProxy
                                                                                        , context
                                                                                      }
                                                                                    , _interceptor
                                                                                     ) ;
            return xamlWriterProxy ;
        }

        public virtual object CreateXamlObjectReader (
            object                   instance
          , XamlSchemaContext        context
          , XamlObjectReaderSettings settings
        )
        {
            return _proxyGenerator.CreateClassProxy (
                                                     typeof ( XamlObjectReader )
                                                   , new object[] { instance , context , settings }
                                                   , _interceptor
                                                    ) ;
        }

        public virtual XamlObjectReaderSettings CreateXamlObjectReaderSettings ( )
        {
            return new XamlObjectReaderSettings ( )
                   {
                       AllowProtectedMembersOnRoot      = true
                     , RequireExplicitContentVisibility = false
                     , ValuesMustBeString               = false
                   } ;
        }

        public virtual XmlWriter CreateXmlWriter ( StringWriter stringWriter )
        {
            var x = XmlWriter.Create ( stringWriter ) ;
            var xmlWriterProxy = _proxyGenerator.CreateClassProxyWithTarget ( x , _interceptor ) ;
            return xmlWriterProxy ;
        }

        public virtual StringWriter CreateStringWriter ( )
        {
            return _proxyGenerator.CreateClassProxy < StringWriter > ( _interceptor ) ;
        }

        public virtual XamlSchemaContext CreateXamlSchemaContext ( )
        {
            return _proxyGenerator.CreateClassProxy < XamlSchemaContext > ( _interceptor ) ;
        }

        public static ProxyUtils CreateProxy (
            Action < string > writeLine
          , IInterceptor      interceptor
        )
        {
            return ( ProxyUtils ) _proxyGenerator.CreateClassProxy (
                                                                    typeof ( ProxyUtils )
                                                                  , new object[]
                                                                    {
                                                                        writeLine , interceptor
                                                                    }
                                                                  , interceptor
                                                                   ) ;
        }
    }

    public class ProxyUtils : ProxyUtilsBase
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProxyUtils ( Action < string > writeOut , IInterceptor interceptor ) : base ( writeOut , interceptor )
        {
        }
    }

    public abstract class BaseInterceptor : IInterceptor
    {
        protected Action < string > _write     = null ;
        protected Action < string > _writeLine = null ;
        public BaseInterceptor ( ) { }

        public void DumpInvocation ( IInvocation invocation , int callDepth )
        {
            var c = Depth ( callDepth ) ;

            var f = FormatInvocation ( invocation , c ) ;
            if ( _write == null )
            {
                _writeLine ( f ) ;
            }
            else
            {
                _write ( f ) ;
            }
        }


        private string FormatInvocation ( IInvocation invocation , string c )
        {
            var args = invocation.Method.IsSpecialName && invocation.Arguments.Length == 0
                           ? ""
                           :
                           invocation.Arguments.Length == 0
                               ?
                               "( )"
                               : "("
                                 + string.Join (
                                                ", "
                                              , invocation
                                               .Arguments.Select < object , string > (
                                                                                      ( o , i )
                                                                                          => FormatArgument (
                                                                                                             invocation
                                                                                                           , o
                                                                                                           , i
                                                                                                            )
                                                                                     )
                                               .ToArray ( )
                                               )
                                         .PadRight ( 40 )
                                 + " )" ;
            return c + "\t" + MethodSpec ( invocation ) + args ;
        }

        private string FormatArgument ( IInvocation invocation , object arg1 , int arg2 )
        {
            var q = FormatTypeAndValue ( arg1 ) ;
            var name = invocation.Method.GetParameters ( )[ arg2 ].Name ;
            return $"{name}: {q}" ;
        }

        private string FormatTypeAndValue ( object o )
        {
            if ( o == null )
            {
                return "null" ;
            }

            var type = FormatTyp ( o.GetType ( ) ) + " " ;

            if ( o is Type t )
            {
                type = "" ;
            }

            var formatValue = FormatValue ( o , out var wantType ) ;
            return wantType ? $"{type}{formatValue}" : formatValue ;
        }

        private static string Depth ( int callDepth )
        {
            var c = '⟳' ;
            var c1 = '⯈' ;
            return $"{new string ( c , callDepth ),- 8}" ;
            //return char.ConvertFromUtf32 ( 0x2460 + callDepth - 1 ) ;
        }

        public void DumpReturnValue ( IInvocation invocation , int callDepth , bool continuation )
        {
            var c = Depth ( callDepth ) ;
            if ( invocation.ReturnValue != null )
            {
                var type = invocation.ReturnValue.GetType ( ) ;
                var f = ( continuation
                              ? ""
                              : c
                                + "\t"
                                + MethodSpec ( invocation )
                                + "\t"
                                + //invocation.TargetType + "."+ invocation.Method.Name +
                                "\t\t" )
                        + " ➦ "
                        + FormatReturnValue ( invocation , invocation.ReturnValue ) ;
                _writeLine ( f ) ;
            }
        }

        private string FormatReturnValue ( IInvocation invocation , object r )
        {
            return FormatValue ( r , out var b1 ) ;
        }

        private static string FormatValue ( object r , out bool b1 )
        {
            string val ;
            b1 = false ;
            if ( r is bool b )
            {
                val = b ? "⊨" : "⊭" ;
                return val ;
            }
            else if ( r is Type t )
            {
                var rr = t.UnderlyingSystemType == t ? "" : "𝓡" ;
                return $"{rr}𝒯 {FormatTyp ( t.UnderlyingSystemType )}" ;
            }
            else if ( r is XamlType xt )
            {
                return "𝓧" + FormatValue ( xt.UnderlyingType , out b1 ) ;
            }
            else if ( r is string ss )
            {
                return ss ;
            } else
            {
                var col = r as ICollection <object> ;
                if ( col != null)
                {
                    var q = from o in col select FormatValue ( o , out b ) ;
                    b1 = true ;
                    return String.Join ( ", " , q ) ;
                }
            }

            b1 = true ;
            return r.ToString ( ) ;
        }

        private static string FormatValue < T > ( ICollection < T > r , out bool b1 )
        {
            b1 = true ;
            return string.Join ( ", " , r.Select ( v => FormatValue ( v , out bool b2 ) ) ) ;
        }
             
        protected static string FormatTyp ( Type type )
        {
            if ( type.IsGenericType )
            {
                return FormatConstructedGenericType ( type ) ;
            }

            var typeName = type.Name ;
            return Alter ( typeName , true ) ;
        }

        private static string FormatConstructedGenericType ( Type type )
        {
            var type2 = type.GetGenericTypeDefinition ( ) ;
            return Alter (
                          $"{type2.Name}<{string.Join ( "," , type.GenericTypeArguments.Select ( ( type1 , i ) => FormatTyp ( type1 ) ) )}>"
                        , true
                         ) ;
        }

        private static string Alter ( string s , bool b )
        {
            return s.Replace ( "<" , " 〈 " )
                    .Replace ( ">" , " 〉 " )
                    .Replace ( "[" , " 【 " )
                    .Replace ( "]" , " 】 " )
                    .Replace ( "`" , "❛" ) ;
        }

        private string MethodSpec ( IInvocation invocation )
        {
            var declType = invocation.Method.DeclaringType ;
            var formatTyp = FormatTyp ( declType ) ;
            var ttype = invocation.TargetType ;
            var typ = FormatTyp ( ttype ) ;

            var m = " " + invocation.Method.Name ;
            if ( invocation.Method.IsSpecialName
                 && invocation.Method.Name.StartsWith ( "get_" ) )
            {
                m = $"𝜙 {invocation.Method.Name.Substring ( 4 )}" ;
            }

            return ( declType == ttype ? $"{formatTyp,33}" : $"{formatTyp,16} {typ,16}" )
                   + " "
                   + m ;
        }

        public abstract void Intercept ( IInvocation invocation ) ;
    }

    public class BaseInterceptorImpl : BaseInterceptor
    {
        private            int            callDepth  = 0 ;
        protected readonly ProxyGenerator _generator = null ;
        private            Stack < X >    stack      = new Stack < X > ( ) ;

        public BaseInterceptorImpl ( Action < string > @out , ProxyGenerator generator = null )
        {
            _writeLine = @out ;
            _generator = generator ?? new ProxyGenerator ( ) ;
        }

        public override void Intercept ( IInvocation invocation )
        {
            Interlocked.Increment ( ref callDepth ) ;
            var x = stack.Any ( ) ? stack.Peek ( ) : null ;
            var myX = new X ( ) ;
            stack.Push ( myX ) ;
            var writeLine = _writeLine ;
            if ( x != null )
            {
                x.written = true ;
                writeLine ( "" ) ;
            }

            DumpInvocation ( invocation , callDepth ) ;
            invocation.Proceed ( ) ;
            try
            {
                DumpReturnValue ( invocation , callDepth , ! myX.written ) ;
                if ( _generator                != null
                     && invocation.ReturnValue != null
                     && ! invocation.ReturnValue.GetType ( ).IsSealed
                     && ! ( invocation.ReturnValue is IProxyTargetAccessor ) )
                {
                    var r = invocation.ReturnValue ;
                    if ( r is XamlType typ )
                    {
                        var invoker =
                            ( XamlTypeInvoker ) _generator.CreateClassProxyWithTarget (
                                                                                       typeof (
                                                                                           XamlTypeInvoker
                                                                                       )
                                                                                     , typ.Invoker
                                                                                     , new[] { r }
                                                                                     , this
                                                                                      ) ;
                        object[] args = { typ.UnderlyingType , typ.SchemaContext , invoker } ;


                        invocation.ReturnValue =
                            _generator.CreateClassProxyWithTarget (
                                                                   r.GetType ( )
                                                                 , r
                                                                 , args
                                                                 , this
                                                                  ) ;
                    }
                    else if ( r.GetType ( ).IsGenericType
                              && r.GetType ( ).GetGenericTypeDefinition ( )
                              == typeof ( XamlValueConverter < object > )
                                 .GetGenericTypeDefinition ( ) )
                    {
                        object[] args = null ;
                        if ( r is XamlValueConverter < ValueSerializer > q )
                        {
                            args = new object[] { q.ConverterType , q.TargetType , q.Name } ;
                        }
                        else if ( r is XamlValueConverter < TypeConverter > z )
                        {
                            args = new object[] { z.ConverterType , z.TargetType , z.Name } ;
                        }

                        if ( args != null )
                        {
                            invocation.ReturnValue =
                                _generator.CreateClassProxyWithTarget (
                                                                       r.GetType ( )
                                                                     , r
                                                                     , args
                                                                     , this
                                                                      ) ;
                        }
                    }
                    else if ( r.GetType ( ).IsGenericType
                              && r.GetType ( ).GetGenericTypeDefinition ( )
                              == typeof ( ReadOnlyCollection < object > )
                                 .GetGenericTypeDefinition ( ) )
                    {
                        var propinfo = r.GetType ( )
                                        .GetField (
                                                   "list"
                                                 , BindingFlags.NonPublic | BindingFlags.Instance
                                                  ) ;
                        var args = new[] { propinfo.GetValue ( r ) } ;
                        invocation.ReturnValue =
                            _generator.CreateClassProxyWithTarget (
                                                                   r.GetType ( )
                                                                 , r
                                                                 , args
                                                                 , this
                                                                  ) ;
                    }
                    else if ( r is XamlDirective d )
                    {
                        object[] args =
                        {
                            d.GetXamlNamespaces ( ) , d.Name , d.Type , d.TypeConverter
                          , d.AllowedLocation
                        } ;

                        invocation.ReturnValue =
                            _generator.CreateClassProxyWithTarget (
                                                                   r.GetType ( )
                                                                 , r
                                                                 , args
                                                                 , this
                                                                  ) ;
                    } else if (r is NamespaceDeclaration ns) {
                        invocation.ReturnValue =
                            _generator.CreateClassProxyWithTarget (
                                                                   r.GetType ( )
                                                                 , r
                                                                 , new object[] { ns.Namespace, ns.Prefix }
                                                                 , this
                                                                  ) ;

                }
                    else

                    {
                        try
                        {
                            invocation.ReturnValue =
                                _generator.CreateClassProxyWithTarget ( r.GetType ( ) , r , this ) ;
                        }
                        catch ( InvalidProxyConstructorArgumentsException ex )
                        {
                            writeLine ( "Constructors for ⮜" + FormatTyp ( r.GetType ( ) ) + "⮞" ) ;
                            foreach ( var constructorInfo in r.GetType ( ).GetConstructors ( ) )
                            {
                                writeLine (
                                           FormatTyp ( constructorInfo.DeclaringType )
                                           + " ( "
                                           + string.Join (
                                                          " , "
                                                        , constructorInfo
                                                         .GetParameters ( )
                                                         .Select (
                                                                  ( info , i )
                                                                      => FormatTyp (
                                                                                    info
                                                                                       .ParameterType
                                                                                   )
                                                                         + " "
                                                                         + info.Name
                                                                         + ( info.HasDefaultValue
                                                                                 ? " = "
                                                                                   + info
                                                                                      .DefaultValue
                                                                                   ?? "null"
                                                                                 : "" )
                                                                 )
                                                         )
                                          ) ;
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                writeLine ( "--Exception--" ) ;
                writeLine ( ex.ToString ( ) ) ; //.GetType ( ) ) ;
                //Console.WriteLine ( ex.Message ) ;
                writeLine ( "--End Exception--" ) ;
            }
            finally
            {
                stack.Pop ( ) ;
                Interlocked.Decrement ( ref callDepth ) ;
            }
        }
    }

    internal class X
    {
        public bool written ;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public X ( bool written = false ) { this.written = written ; }
    }
}
