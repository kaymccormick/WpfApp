﻿using System ;
using System.Runtime.Serialization ;
using Autofac.Core ;
using JetBrains.Annotations ;
using Moq ;
using Tests.Attributes ;
using Tests.Lib.Fixtures ;
using WpfApp.Core ;
using WpfApp.Core.Interfaces ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Main
{
    /// <summary>Test for the default configured service for the <seealso cref="IObjectIdProvider"/> service.</summary>
    /// <seealso cref="LoggingFixture" />
    [BeforeAfterLogger, LogTestMethod] [UsedImplicitly] [Collection("GeneralPurpose")]
    public class DefaultObjectIdProviderTests : IClassFixture <LoggingFixture>, IDisposable
    {
        private readonly LoggingFixture _loggingFixture ;

        /// <summary>Initializes a new instance of the <see cref="System.Object" /> class.</summary>
        public DefaultObjectIdProviderTests (LoggingFixture loggingFixture, ITestOutputHelper helper )
        {
            _loggingFixture = loggingFixture ;
            loggingFixture.SetOutputHelper(helper);

        }

        /// <summary>Gets the instance by component registration test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetInstanceByComponentRegistrationTest
        [Fact ]
        public void GetInstanceByComponentRegistrationTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var list = x.GetInstanceByComponentRegistration ( reg.Object ) ;
            Assert.NotNull ( list ) ;
        }

        /// <summary>Gets the instance count test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetInstanceCountTest
        [Fact ]
        public void GetInstanceCountTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var instanceCount = x.GetInstanceCount ( reg.Object ) ;
            Assert.Equal ( 0 , instanceCount ) ;
        }

        /// <summary>Gets the object instances test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetObjectInstancesTest
        [Fact ]
        public void GetObjectInstancesTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var instances = x.GetObjectInstances ( ) ;
            Assert.NotNull ( instances ) ;
        }

        /// <summary>Gets the root nodes test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for GetRootNodesTest
        [Fact ]
        public void GetRootNodesTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var rootNodes = x.GetRootNodes ( ) ;
            Assert.Empty ( rootNodes ) ;
        }

        /// <summary>Provides the object instance identifier test.</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for ProvideObjectInstanceIdentifierTest
        [Fact ]
        public void ProvideObjectInstanceIdentifierTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var instance = new object ( ) ;
            var id = x.ProvideObjectInstanceIdentifier ( instance , reg.Object , null ) ;
            Assert.NotNull ( id ) ;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( )
        {
            _loggingFixture.SetOutputHelper ( null ) ;
            // _loggingFixture?.Dispose ( ) ;
        }
    }
}