using Xunit;
using System.Runtime.Serialization ;
using Autofac.Core ;

namespace AppShared.Services.ests
{
    public class DefaultObjectIdProviderTests
    {
        public DefaultObjectIdProviderTests ()
        {

        }

        [Fact ( )]
        public void getRootNodesTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var rootNodes = x.GetRootNodes ( ) ;
            Assert.NotEmpty(rootNodes);
        }


        [Fact ( )]
        public void GetInstanceByComponentRegistrationTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var list = x.GetInstanceByComponentRegistration ( reg.Object) ;

        }

        [Fact ( )]
        public void GetInstanceCountTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var instanceCount = x.GetInstanceCount ( reg.Object ) ;
            
            
        }

        [Fact ( )]
        public void GetObjectInstancesTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var instances = x.GetObjectInstances() ;

        }

        [Fact ( )]
        public void ProvideObjectInstanceIdentifierTest ()
        {
            DefaultObjectIdProvider x = new DefaultObjectIdProvider(new ObjectIDGenerator());
            var reg = new Moq.Mock < IComponentRegistration > ( ) ;
            var id = x.ProvideObjectInstanceIdentifier ( null , reg.Object , null ) ;
            
        }
    }
}