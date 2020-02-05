﻿using JetBrains.Annotations ;
using TestLib.Fixtures ;
using Xunit ;

namespace TestLib.Collections
{
    /// <summary></summary>
    /// <seealso cref="WpfApplicationFixture" />
    /// <seealso cref="UtilsContainerFixture" />
    /// <seealso cref="LoggingFixture" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for WpfApplication
    [ CollectionDefinition ( "WpfApp" ) , UsedImplicitly ]
    public class WpfApplication : ICollectionFixture < WpfApplicationFixture >
      , ICollectionFixture < UtilsContainerFixture >
      , ICollectionFixture < LoggingFixture >
      , ICollectionFixture < AppContainerFixture >

    {
    }
}