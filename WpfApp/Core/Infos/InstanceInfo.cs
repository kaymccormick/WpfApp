﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// InstanceInfo.cs
// 
// 2020-01-25-7:05 PM
// 
// ---
#endregion
using System.Collections.Generic ;
using Autofac.Core ;

namespace WpfApp.Core.Infos
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for InstanceInfo
    public class InstanceInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public InstanceInfo ( ) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public InstanceInfo ( object instance , IEnumerable < Parameter > parameters )
        {
            Instance   = instance ;
            Parameters = parameters ;
        }

        /// <summary>Gets or sets the instance.</summary>
        /// <value>The instance.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Instance
        public object Instance { get ; set ; }

        /// <summary>Gets or sets the parameters.</summary>
        /// <value>The parameters.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Parameters
        public IEnumerable < Parameter > Parameters { get ; set ; }
    }
}