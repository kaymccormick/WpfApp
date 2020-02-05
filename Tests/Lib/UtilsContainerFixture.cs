﻿using System ;
using AppShared ;
using AppShared.Infos ;
using Autofac ;
using TestLib.Fixture ;
using TestLib.Fixtures ;

namespace TestLib
{
/// <summary></summary>
/// <seealso cref="System.IDisposable" />
/// <autogeneratedoc />
/// TODO Edit XML Comment Template for UtilsContainerFixture
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class UtilsContainerFixture : IDisposable
    {
        private readonly IContainer _container;
        private ILifetimeScope _scope;
        public IComponentContext Container { get => _scope;  }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public UtilsContainerFixture()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType< InfoContext >().AsSelf();
            containerBuilder.RegisterType < MyServices >().As < IMyServices >();
            containerBuilder.RegisterType < MyServicesFixture >().AsSelf ();
            _container = containerBuilder.Build();
            _scope = _container.BeginLifetimeScope();

        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _scope?.Dispose();
            _container.Dispose();
        }
    }
}