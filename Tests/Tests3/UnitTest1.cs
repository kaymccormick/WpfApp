﻿using System.Collections ;
using Autofac.Core ;
using Autofac.Core.Registration ;
using Common ;
using TestLib.Attributes ;
using WpfApp1.Windows ;
using Xunit ;
using ComponentRegistration = Common.Model.ComponentRegistration ;
using ComponentRegistry = Autofac.Core.Registration.ComponentRegistry ;

namespace WpfApp1Tests3
{
	/// <summary>
	///     Summary description for UnitTest1
	/// </summary>
    [LogTestMethod, BeforeAfterLogger]
	public class UnitTest1
	{
		[ Fact ]
		public void Tesst1 ( )
		{
			var componentRegistry = new ComponentRegistry ( ) ;

			var componentRegistration = new ComponentRegistration ( ) ;
			var c = componentRegistration ;
			var typedService = new TypedService ( typeof ( IEnumerable ) ) ;
			//v//ar x = new LifetimeScope ( componentRegistry ) ;
		}
	}
}