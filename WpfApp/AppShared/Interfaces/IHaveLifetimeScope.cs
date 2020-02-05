using Autofac ;

namespace AppShared.Interfaces
{
    public interface IHaveLifetimeScope

    {
        ILifetimeScope LifetimeScope { get ; set ; }
    }
}