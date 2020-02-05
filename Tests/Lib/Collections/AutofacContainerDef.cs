using TestLib.Fixtures ;
using Xunit ;

namespace Tests.Lib.Collections
{
	[ CollectionDefinition ( "AutofacContainer" ) ]
	internal class AutofacContainerDef : ICollectionFixture < AppContainerFixture >
	  , ICollectionFixture < LoggingFixture >
	{
	}
}