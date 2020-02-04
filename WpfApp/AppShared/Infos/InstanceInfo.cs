#region header
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

namespace AppShared.Infos
{
	public class InstanceInfo
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public InstanceInfo ( ) { }

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public InstanceInfo ( object instance , IEnumerable < Parameter > parameters )
		{
			Instance   = instance ;
			Parameters = parameters ;
		}

		public object Instance { get ; set ; }

		public IEnumerable < Parameter > Parameters { get ; set ; }
	}
}