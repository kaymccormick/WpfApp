﻿using System.Configuration ;
using WpfApp.Core.Container ;

namespace WpfApp.Config
{
    /// <summary>Configuration section handler for container helper settings.</summary>
    /// <seealso cref="System.Configuration.ConfigurationSection" />
    [ConfigTarget(typeof ( ContainerHelperSettings ))]
    public class ContainerHelperSection : ConfigurationSection
    {
        private const string doTrace = "DoTraceConditionalRegistration";

        /// <summary>Gets or sets a value indicating whether [do interception].</summary>
        /// <value>
        ///   <c>true</c> if [do interception]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoInterception
        [ConfigurationProperty("DoInterception", DefaultValue = false, IsRequired = false, IsKey = false)]
        // ReSharper disable once UnusedMember.Global
        public bool DoInterception { get => ( bool ) this[ "DoInterception" ] ; set => this["DoInterception"] = value; }

        /// <summary>Gets or sets a value indicating whether [do trace conditional registration].</summary>
        /// <value>
        ///   <c>true</c> if [do trace conditional registration]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for DoTraceConditionalRegistration
        [ConfigurationProperty(doTrace, DefaultValue = false, IsRequired = false, IsKey = false)]
        // ReSharper disable once UnusedMember.Global
        public bool DoTraceConditionalRegistration
        {
            get => (bool)this[ doTrace ] ;
            set => this[doTrace] = value ;
        }
    }
}
