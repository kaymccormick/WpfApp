using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Application ;
using WpfApp.Common.Utils ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests
{
    public class AppTests
    {
        private readonly ITestOutputHelper _output ;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AppTests ( ITestOutputHelper output ) { _output = output ; }

        [WpfFact]
        public void TestApplyConfiguration()
        {
            var app = new App ( DebugEventHandler ) ;
            Assert.NotNull ( app ) ;
            Assert.Collection( app.ConfigSettings , o => Assert.IsType<ContainerHelperSettings>(o));
                
        }

        private void DebugEventHandler ( object sender , DebugEventArgs e )
        {
            _output.WriteLine ( e.Message ) ;
        }
    }
}
