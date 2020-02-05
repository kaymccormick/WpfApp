﻿using System ;
using AppShared.Interfaces ;
using NLog ;
using TestLib ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using WpfApp1.Menus ;
using WpfApp1Tests3 ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Tests3
{
    /// <summary></summary>
    [Collection ( "WpfApp" ) ]
    [LogTestMethod, BeforeAfterLogger]
    public class MenuHelperTests : WpfTestsBase
    {
        // ReSharper disable once UnusedMember.Local
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

#pragma warning disable 649
        private readonly Func < IMenuItem > _xMenuItemCreator ;
#pragma warning restore 649


        protected MenuHelperTests ( WpfApplicationFixture wpfAppFixture , AppContainerFixture containerFixture , UtilsContainerFixture utilsContainerFixture , ITestOutputHelper outputHelper ) : base ( wpfAppFixture , containerFixture , utilsContainerFixture , outputHelper )
        {
        }

        /// <summary>Makes the menu item test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for MakeMenuItemTest
        [WpfFact ]
        public void MakeMenuItemTest ( )
        {
            var header = "test" ;
            var arg = _xMenuItemCreator ( ) ;
            arg.Header = header ;
            Assert.NotNull ( arg ) ;
            var item = MenuHelper.MakeMenuItem ( arg ) ;
            Assert.NotNull ( item ) ;
            Assert.Equal ( header , item.Header ) ;
        }
    }
}