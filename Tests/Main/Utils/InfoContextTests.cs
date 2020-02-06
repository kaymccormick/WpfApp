﻿using System.Linq ;
using Tests.Lib.Attributes ;
using WpfApp.Core.Infos ;
using Xunit ;

namespace Tests.Main.Utils
{
    [ LogTestMethod ] [ BeforeAfterLogger ]
    public class InfoContextTests
    {
        [ Fact ]
        public void EnumeratorTest ( )
        {
            var name = "test" ;
            var objectContext = "hello" ;
            var x = new InfoContext ( name , objectContext ) ;
            var objects = x.ToList ( ) ;
            Assert.Equal ( 2 ,             objects.Count ) ;
            Assert.Equal ( name ,          objects[ 0 ] ) ;
            Assert.Equal ( objectContext , objects[ 1 ] ) ;
        }
    }
}