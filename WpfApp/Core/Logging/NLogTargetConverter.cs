using System ;
using System.Collections ;
using System.ComponentModel ;
using System.Globalization ;
using System.Windows.Data ;
using NLog.Targets ;

namespace WpfApp.Core.Logging
{
    /// <summary></summary>
    /// <seealso cref="System.ComponentModel.TypeConverter" />
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for TargetConverter
    public class NLogTargetConverter : TypeConverter , IValueConverter
    {

        /// <summary>Converts a value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>, the valid null value is used.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Convert
        object IValueConverter.Convert (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            return Convert ( value , targetType , parameter , culture ) ;
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the
        ///     valid null value is used.
        /// </returns>
        object IValueConverter.ConvertBack (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            return ConvertBack ( value , targetType , parameter , culture ) ;
        }


        /// <summary>
        /// Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.
        /// </summary>
        /// <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
        /// <returns>
        /// An <see cref="System.Object"/> representing the given <see cref="System.Collections.IDictionary"/>, or <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> if the object cannot be created. This method always returns <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for CreateInstance
        public override object CreateInstance (
            ITypeDescriptorContext context
          , IDictionary            propertyValues
        )
        {
            return base.CreateInstance ( context , propertyValues ) ;
        }

        
        public override bool GetCreateInstanceSupported ( ITypeDescriptorContext context )
        {
            return base.GetCreateInstanceSupported ( context ) ;
        }

        /// <summary></summary>
  
        /// <returns>
        ///     A <see cref="System.ComponentModel.PropertyDescriptorCollection" />
        ///     with the properties that are exposed for this data type, or
        ///     <see langword="null" /> if there are no properties.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties (
            ITypeDescriptorContext context
          , object                 value
          , Attribute[]            attributes
        )
        {
            return base.GetProperties ( context , value , attributes ) ;
        }

        /// <summary>Returns whether this object supports properties, using the specified context.</summary>
        /// <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns>
        ///   <span class="keyword">
        ///     <span class="languageSpecificText">
        ///       <span class="cs">true</span>
        ///       <span class="vb">True</span>
        ///       <span class="cpp">true</span>
        ///     </span>
        ///   </span>
        ///   <span class="nu">
        ///     <span class="keyword">true</span> (<span class="keyword">True</span> in Visual Basic)</span> if <see cref="System.ComponentModel.TypeConverter.GetProperties(System.Object)"/> should be called to find the properties of this object; otherwise, <span class="keyword"><span class="languageSpecificText"><span class="cs">false</span><span class="vb">False</span><span class="cpp">false</span></span></span><span class="nu"><span class="keyword">false</span> (<span class="keyword">False</span> in Visual Basic)</span>.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetPropertiesSupported
        public override bool GetPropertiesSupported ( ITypeDescriptorContext context )
        {
            return base.GetPropertiesSupported ( context ) ;
        }
        /// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
        /// <param name="context">
        /// An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>.
        /// </param>
        /// <returns>
        /// A <see cref="System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values, or <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span> if the data type does not support a standard set of values.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetStandardValues
        public override StandardValuesCollection GetStandardValues (
            ITypeDescriptorContext context
        )
        {
            return base.GetStandardValues ( context ) ;
        }

        
        public override bool GetStandardValuesExclusive ( ITypeDescriptorContext context )
        {
            return base.GetStandardValuesExclusive ( context ) ;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool GetStandardValuesSupported ( ITypeDescriptorContext context )
        {
            return base.GetStandardValuesSupported ( context ) ;
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom ( ITypeDescriptorContext context , Type sourceType )
        {
            return base.CanConvertFrom ( context , sourceType ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo ( ITypeDescriptorContext context , Type destinationType )
        {
            if ( destinationType == typeof ( string ) )
            {
                return true ;
            }

            return base.CanConvertTo ( context , destinationType ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom (
            ITypeDescriptorContext context
          , CultureInfo            culture
          , object                 value
        )
        {
            return base.ConvertFrom ( context , culture , value ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo (
            ITypeDescriptorContext context
          , CultureInfo            culture
          , object                 value
          , Type                   destinationType
        )
        {
            try
            {
                var target = value as Target ;
                if ( target is NetworkTarget n )
                {
                    return n.Address ;
                }
            }
            catch ( Exception )
            {
            }

            return base.ConvertTo ( context , culture , value , destinationType ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid ( ITypeDescriptorContext context , object value )
        {
            return base.IsValid ( context , value ) ;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString ( ) { return base.ToString ( ) ; }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the
        ///     valid null value is used.
        /// </returns>
        public object Convert (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            // var typeDescriptionProvider = new TypeDescriptionProvider();
            // var customTypeDescriptor = typeDescriptionProvider.GetTypeDescriptor( value );
            if ( ( string ) parameter == "GetType" )
            {
                return ConvertTo ( value.GetType ( ) , targetType ) ;
            }

            return ConvertTo ( value , targetType ) ;
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />, the
        ///     valid null value is used.
        /// </returns>
        public object ConvertBack (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            throw new NotImplementedException ( ) ;
        }
    }
}