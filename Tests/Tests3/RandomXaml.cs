﻿using AppShared ;
using Common.Logging ;
using Logging ;
using NLog ;
using TestLib.Attributes ;
using Tests.Lib.Attributes ;
using WpfApp.Core.Logging ;
using Xunit ;

namespace WpfApp1Tests3
{
    [ WpfTestApplication ]
    [ Collection ( "WpfApp" ) ]
    [LogTestMethod, BeforeAfterLogger]
    public class RandomXaml // : IClassFixture <LoggingFixture>
    {
        private static readonly Logger Logger = LoggerProxyHelper.GetCurrentClassLogger ( ) ;

        public LogDelegates.LogMethod UseLogMethod { get ; set ; }

        [ Fact ]
        public void Test1 ( ) { }
    }
}