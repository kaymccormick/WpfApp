#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// ComponentRegistry.cs
// 
// 2020-01-26-9:09 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Windows.Markup ;
using Autofac.Core ;

namespace WpfApp.Controls.Windows
{
    [ ContentProperty ( "RegistrationsList" ) ]
    public class ComponentRegistry : IComponentRegistry , IAddChild
    {
        private IEnumerable < IComponentRegistration > _registrations ;


        public ComponentRegistry ( )
        {
            RegistrationsList = new List < IComponentRegistration > ( ) ;
        }


        public ComponentRegistry (
            IEnumerable < IComponentRegistration > registrations
          , IDictionary < string , object >        properties
          , IEnumerable < IRegistrationSource >    sources
          , bool                                   hasLocalComponents
        )
        {
            Properties         = properties ;
            Registrations      = registrations ;
            Sources            = sources ;
            HasLocalComponents = hasLocalComponents ;
        }


        public List < IComponentRegistration > RegistrationsList { get ; }


        public void AddChild ( object value )
        {
            RegistrationsList.Add ( value as IComponentRegistration ) ;
        }


        public void AddText ( string text ) { }


        public void Dispose ( ) { throw new NotImplementedException ( ) ; }


        public bool TryGetRegistration ( Service service , out IComponentRegistration registration )
        {
            throw new NotImplementedException ( ) ;
        }


        public bool IsRegistered ( Service service ) { throw new NotImplementedException ( ) ; }


        public void Register ( IComponentRegistration registration )
        {
            throw new NotImplementedException ( ) ;
        }


        public void Register ( IComponentRegistration registration , bool preserveDefaults )
        {
            throw new NotImplementedException ( ) ;
        }


        public IEnumerable < IComponentRegistration > RegistrationsFor ( Service service )
        {
            throw new NotImplementedException ( ) ;
        }


        public IEnumerable < IComponentRegistration > DecoratorsFor (
            IComponentRegistration registration
        )
        {
            throw new NotImplementedException ( ) ;
        }


        public void AddRegistrationSource ( IRegistrationSource source )
        {
            throw new NotImplementedException ( ) ;
        }


        public IDictionary < string , object > Properties { get ; }


        public IEnumerable < IComponentRegistration > Registrations
        {
            get
            {
                if ( RegistrationsList != null )
                {
                    return RegistrationsList ;
                }

                return _registrations ;
            }
            private set => _registrations = value ;
        }


        public IEnumerable < IRegistrationSource > Sources { get ; }


        public bool HasLocalComponents { get ; }


        public event EventHandler < ComponentRegisteredEventArgs > Registered ;


        public event EventHandler < RegistrationSourceAddedEventArgs > RegistrationSourceAdded ;


        public IReadOnlyList < IComponentRegistration > DecoratorsFor ( IServiceWithType service )
        {
            throw new NotImplementedException ( ) ;
        }
    }
}