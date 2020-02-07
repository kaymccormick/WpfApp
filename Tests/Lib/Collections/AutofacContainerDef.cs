using Tests.Lib.Fixtures ;
using Xunit ;

namespace Tests.Lib.Collections
{
    [ CollectionDefinition ( "AutofacContainer" ) ]
    internal class AutofacContainerDef : ICollectionFixture < AppContainerFixture >
    {
    }
}