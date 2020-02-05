using System ;

namespace Tests.Lib.Attributes
{
	[ AttributeUsage (
		                 AttributeTargets.Assembly
		                 | AttributeTargets.Class
		                 | AttributeTargets.Module
	                 ) ]
	public class WpfTestApplicationAttribute : Attribute
	{
	}
}