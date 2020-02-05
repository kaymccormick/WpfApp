using Autofac ;

namespace WpfApp.Core.Interfaces.Interfaces
{
    public interface IHaveLifetimeScope

    {
        ILifetimeScope LifetimeScope { get ; set ; }
    }
}