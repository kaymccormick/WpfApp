using System.Runtime.Serialization ;
using AppShared.Services ;
using Autofac.Core ;
using NLog ;
using Xunit ;

namespace Tests.Services
{
    public class DefaultObjectIdProviderTests
    {
        [Fact ( )]
        public void GetRootNodesTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var rootNodes = x.GetRootNodes ( ) ;
            Assert.Empty(rootNodes);
        }


        [Fact ( )]
        public void GetInstanceByComponentRegistrationTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var list = x.GetInstanceByComponentRegistration ( reg.Object) ;
            Assert.NotNull(list);

        }

        [Fact ( )]
        public void GetInstanceCountTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var instanceCount = x.GetInstanceCount ( reg.Object ) ;
            Assert.Equal(0, instanceCount);
            
            
        }

        [Fact ( )]
        public void GetObjectInstancesTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var instances = x.GetObjectInstances() ;
            Assert.NotNull(instances);

        }

        [Fact ( )]
        public void ProvideObjectInstanceIdentifierTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var instance = new object ( ) ;
            var id = x.ProvideObjectInstanceIdentifier ( instance, reg.Object , null ) ;
            Assert.NotNull ( id ) ;

        }
    }
}