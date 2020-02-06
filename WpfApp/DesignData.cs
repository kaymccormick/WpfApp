﻿using System.Collections.Generic ;
using Autofac ;
using WpfApp.Core.Infos ;
using WpfApp.Core.Menus ;

namespace WpfApp
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for DesignData
    public class DesignData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public DesignData ( )
        {
            var containerBuilder = new ContainerBuilder ( ) ;
            containerBuilder.RegisterModule < MenuModule > ( ) ;
            LifetimeScope = containerBuilder.Build ( ).BeginLifetimeScope ( "Design scope" ) ;
        }

        /// <summary>Gets the lifetime scope.</summary>
        /// <value>The lifetime scope.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LifetimeScope
        public ILifetimeScope LifetimeScope { get ; }

        /// <summary>Gets the instance list.</summary>
        /// <value>The instance list.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InstanceList
        // ReSharper disable once CollectionNeverUpdated.Global
        public static List < InstanceInfo > InstanceList { get ; } = new List < InstanceInfo > ( ) ;

        /// <summary>Gets the instance information.</summary>
        /// <value>The instance information.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for InstanceInfo
        public static InstanceInfo InstanceInfo { get ; } = new InstanceInfo ( ) ;
    }
}