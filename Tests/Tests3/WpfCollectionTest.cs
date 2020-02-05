using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Attributes ;
using Xunit ;

namespace Tests.Tests3
{
    [ Collection ( "WpfApp" ) ]
    [ LogTestMethod ] [ BeforeAfterLogger ]
    class WpfCollectionTest
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfCollectionTest ( ) {
        }
    }
}
