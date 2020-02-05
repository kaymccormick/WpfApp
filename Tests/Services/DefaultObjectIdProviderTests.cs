using System.Runtime.Serialization ;
using AppShared.Services ;
using Autofac.Core ;
using Moq ;
using Xunit ;

namespace Tests.Services
{
    public class DefaultObjectIdProviderTests
    {
        [ Fact ]
        public void GetInstanceByComponentRegistrationTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var list = x.GetInstanceByComponentRegistration ( reg.Object ) ;
            Assert.NotNull ( list ) ;
        }

        [ Fact ]
        public void GetInstanceCountTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var instanceCount = x.GetInstanceCount ( reg.Object ) ;
            Assert.Equal ( 0 , instanceCount ) ;
        }

        [ Fact ]
        public void GetObjectInstancesTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var instances = x.GetObjectInstances ( ) ;
            Assert.NotNull ( instances ) ;
        }

        [ Fact ]
        public void GetRootNodesTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var rootNodes = x.GetRootNodes ( ) ;
            Assert.Empty ( rootNodes ) ;
        }

        [ Fact ]
        public void ProvideObjectInstanceIdentifierTest ( )
        {
            var x = new DefaultObjectIdProvider ( new ObjectIDGenerator ( ) ) ;
            var reg = new Mock < IComponentRegistration > ( ) ;
            var instance = new object ( ) ;
            var id = x.ProvideObjectInstanceIdentifier ( instance , reg.Object , null ) ;
            Assert.NotNull ( id ) ;
        }
    }
}