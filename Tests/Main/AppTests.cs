﻿using WpfApp.Application ;
using WpfApp.Core.Utils ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main
{
    /// <summary></summary>
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for AppTests
    public class AppTests
    {
        private readonly ITestOutputHelper _output ;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public AppTests ( ITestOutputHelper output ) { _output = output ; }

        /// <summary>Tests the apply configuration.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for TestApplyConfiguration
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