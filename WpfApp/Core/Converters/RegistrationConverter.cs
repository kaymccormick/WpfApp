﻿using System ;
using System.Globalization ;
using System.Linq ;
using System.Windows.Data ;
using Autofac ;
using Autofac.Core ;
using DynamicData.Kernel ;
using WpfApp.Core.Interfaces ;
using WpfApp.Core.Model ;

namespace WpfApp.Core.Converters
{
    /// <summary></summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for RegistrationConverter
    public class RegistrationConverter : IValueConverter
    {
        // ReSharper disable once NotAccessedField.Local

        /// <summary>The provider</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _provider
        private readonly IObjectIdProvider _provider ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public RegistrationConverter ( ) { }

        /// <summary>Initializes a new instance of the <see cref="RegistrationConverter"/> class.</summary>
        /// <param name="appContainer">The application container.</param>
        /// <param name="provider">The provider.</param>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for #ctor
        // ReSharper disable once UnusedParameter.Local
        public RegistrationConverter ( ILifetimeScope appContainer , IObjectIdProvider provider )
        {
            _provider     = provider ;
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     A converted value. If the method returns <see langword="null" />,
        ///     the valid null value is used.
        /// </returns>
        public object Convert (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            if ( value == null )
            {
                throw new ArgumentNullException ( nameof ( value ) ) ;
            }

            var componentRegistration = ( IComponentRegistration ) value ;
            var instanceInfo =
                _provider.GetInstanceByComponentRegistration ( componentRegistration ) ;
            var x = instanceInfo.Select (
                                         ( o , i ) => {
                                             var objId =
                                                 _provider.ProvideObjectInstanceIdentifier (
                                                                                            o.Instance
                                                                                          , componentRegistration
                                                                                          , o
                                                                                               .Parameters
                                                                                           ) ;
                                             var instanceRegistration =
                                                 new InstanceRegistration (
                                                                           o.Instance
                                                                         , objId
                                                                         , o
                                                                          ) ;
                                             return instanceRegistration ;
                                         }
                                        ) ;

            if ( parameter is string xx )
            {
                if ( string.CompareOrdinal ( xx , "Count" ) == 0 )
                {
                    return _provider.GetInstanceCount ( componentRegistration ) ;
                }
            }

            return x.AsList ( ) ;
        }


        /// <summary>Converts a value.</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <span class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span class="nu">a null reference (<span class="keyword">Nothing</span> in Visual Basic)</span>, the valid null value is used.
        /// </returns>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ConvertBack
        public object ConvertBack (
            object      value
          , Type        targetType
          , object      parameter
          , CultureInfo culture
        )
        {
            return "" ;
        }
    }
}