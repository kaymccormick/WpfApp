using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics.CodeAnalysis ;
using System.Threading.Tasks ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using Autofac.Core.Resolving ;

namespace WpfApp.Controls.Windows
{
    [ SuppressMessage (
                          "Design"
                        , "CA1063:Implement IDisposable Correctly"
                        , Justification = "<Pending>"
                      ) ]
    [ EditorBrowsable ( EditorBrowsableState.Never ) ]
    public class LifetimeScope : ILifetimeScope
    {
        public LifetimeScope (
            object             tag
          , IDisposer          disposer
          , IComponentRegistry componentRegistry = null
        )
        {
            ComponentRegistry = componentRegistry ?? new ComponentRegistry ( ) ;

            Tag      = tag ;
            Disposer = disposer ;
        }


        public object ResolveComponent (
            IComponentRegistration    registration
          , IEnumerable < Parameter > parameters
        )
        {
            throw new NotImplementedException ( ) ;
        }


        public IComponentRegistry ComponentRegistry { get ; }


        public void Dispose ( ) { throw new NotImplementedException ( ) ; }


        public ILifetimeScope BeginLifetimeScope ( ) { throw new NotImplementedException ( ) ; }


        public ILifetimeScope BeginLifetimeScope ( object tag )
        {
            throw new NotImplementedException ( ) ;
        }


        public ILifetimeScope BeginLifetimeScope ( Action < ContainerBuilder > configurationAction )
        {
            throw new NotImplementedException ( ) ;
        }


        public ILifetimeScope BeginLifetimeScope (
            object                      tag
          , Action < ContainerBuilder > configurationAction
        )
        {
            throw new NotImplementedException ( ) ;
        }


        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public IDisposer Disposer { get ; set ; }


        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public object Tag { get ; set ; }


        public event EventHandler < LifetimeScopeBeginningEventArgs > ChildLifetimeScopeBeginning ;


        public event EventHandler < LifetimeScopeEndingEventArgs > CurrentScopeEnding ;


        public event EventHandler < ResolveOperationBeginningEventArgs > ResolveOperationBeginning ;


        public ValueTask DisposeAsync ( ) { throw new NotImplementedException ( ) ; }
    }
}