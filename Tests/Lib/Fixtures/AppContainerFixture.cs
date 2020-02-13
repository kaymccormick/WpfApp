﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ContainerFixture.cs
// 
// 2020-01-19-5:59 PM
// 
// ---
#endregion

using System ;
using System.Collections.Generic ;
using System.Threading.Tasks ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using Autofac.Core.Resolving ;
using JetBrains.Annotations ;
using KayMcCormick.Test.Common ;
using NLog ;
using WpfApp.Core.Container ;
using Xunit ;
using Xunit.Abstractions ;

namespace Tests.Lib.Fixtures
{
    /// <summary>Test fixture configured to supply the primary application container from Autofac.</summary>
    /// <seealso cref="Xunit.IAsyncLifetime" />
    /// <seealso cref="ContainerHelper"/>
    [UsedImplicitly]
    public class AppContainerFixture : IAsyncLifetime, ILifetimeScope
    {
        private readonly IMessageSink _sink ;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        /// <summary>The container</summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for _container
        private readonly ILifetimeScope _container = null ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="System.Object" />
        ///     class.
        /// </summary>
        public AppContainerFixture ( IMessageSink sink )
        {
            _sink = sink ;
            FixtureLogger.LogFixtureCreatedLifecycleEvent(GetType());

        }

        /// <summary>Gets the lifetime scope.</summary>
        /// <value>The lifetime scope.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for LifetimeScope
        protected ILifetimeScope LifetimeScope { get ; set ; }


        /// <summary>
        ///     Called immediately after the class has been created, before it is used.
        /// </summary>
        public Task InitializeAsync ( )
        {
            _sink.OnMessage ( new DiagnosticMessage ( "Initializing container." ) ) ;
            LifetimeScope = ContainerHelper.SetupContainer(out _, null, null);
            return Task.CompletedTask ;
        }


        /// <summary>
        ///     Called when an object is no longer needed. Called just before
        ///     <see cref="System.IDisposable.Dispose"/>
        ///     if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( )
        {
            LifetimeScope?.Dispose ( ) ;
            _container?.Dispose ( ) ;
            return Task.CompletedTask ;
        }

        /// <summary>
        /// Resolve an instance of the provided registration within the context.
        /// </summary>
        /// <param name="registration">The registration.</param>
        /// <param name="parameters">Parameters for the instance.</param>
        /// <returns>The component instance.</returns>
        /// <exception cref="Autofac.Core.Registration.ComponentNotRegisteredException" />
        /// <exception cref="Autofac.Core.DependencyResolutionException" />
        public object ResolveComponent ( IComponentRegistration registration , IEnumerable < Parameter > parameters ) { return LifetimeScope.ResolveComponent ( registration , parameters ) ; }

        /// <summary>
        /// Gets the associated services with the components that provide them.
        /// </summary>
        public IComponentRegistry ComponentRegistry => LifetimeScope.ComponentRegistry ;

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose ( )
        {
            FixtureLogger.LogFixtureFinalizedLifecycleEvent(GetType());
            LifetimeScope.Dispose ( ) ;
        }

        /// <summary>
        /// Begin a new nested scope. Component instances created via the new scope
        /// will be disposed along with it.
        /// </summary>
        /// <returns>A new lifetime scope.</returns>
        public ILifetimeScope BeginLifetimeScope ( ) { return LifetimeScope.BeginLifetimeScope ( ) ; }

        /// <summary>
        /// Begin a new nested scope. Component instances created via the new scope
        /// will be disposed along with it.
        /// </summary>
        /// <param name="tag">The tag applied to the <see cref="Autofac.ILifetimeScope" />.</param>
        /// <returns>A new lifetime scope.</returns>
        public ILifetimeScope BeginLifetimeScope ( object tag ) { return LifetimeScope.BeginLifetimeScope ( tag ) ; }


        /// <summary />
        /// <param name="configurationAction"></param>
        /// <returns></returns>
        public ILifetimeScope BeginLifetimeScope ( Action < ContainerBuilder > configurationAction ) { return LifetimeScope.BeginLifetimeScope ( configurationAction ) ; }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="configurationAction"></param>
        /// <returns></returns>
        public ILifetimeScope BeginLifetimeScope ( object tag , Action < ContainerBuilder > configurationAction ) { return LifetimeScope.BeginLifetimeScope ( tag , configurationAction ) ; }

       
        /// <summary>
        /// 
        /// </summary>
        public IDisposer Disposer => LifetimeScope.Disposer ;

      
        /// <summary>
        /// 
        /// </summary>
        public object Tag => LifetimeScope.Tag ;

        /// <summary>
        /// Fired when a new scope based on the current scope is beginning.
        /// </summary>
        public event EventHandler < LifetimeScopeBeginningEventArgs > ChildLifetimeScopeBeginning
        {
            add => LifetimeScope.ChildLifetimeScopeBeginning += value ;
            remove => LifetimeScope.ChildLifetimeScopeBeginning -= value ;
        }

        /// <summary>Fired when this scope is ending.</summary>
        public event EventHandler < LifetimeScopeEndingEventArgs > CurrentScopeEnding
        {
            add => LifetimeScope.CurrentScopeEnding += value ;
            remove => LifetimeScope.CurrentScopeEnding -= value ;
        }

        /// <summary>
        /// Fired when a resolve operation is beginning in this scope.
        /// </summary>
        public event EventHandler < ResolveOperationBeginningEventArgs > ResolveOperationBeginning
        {
            add => LifetimeScope.ResolveOperationBeginning += value ;
            remove => LifetimeScope.ResolveOperationBeginning -= value ;
        }
    }
}
