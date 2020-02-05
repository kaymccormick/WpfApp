using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Fixtures ;
using Xunit ;

namespace Tests.Tests3
{
    [Collection("WpfApp")]
    public class TestWindowViews
    {
        public WpfApplicationFixture WpfApplicationFixture { get ; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TestWindowViews (WpfApplicationFixture wpfApplicationFixture )
        {
            WpfApplicationFixture = wpfApplicationFixture ;
        }
    }
}
