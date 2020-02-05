using Autofac ;

namespace WpfApp.Core.Interfaces
{
    public interface IHaveLifetimeScope

    {
        ILifetimeScope LifetimeScope { get ; set ; }
    }
}