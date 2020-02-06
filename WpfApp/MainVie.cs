using System.Collections.Generic ;

namespace WpfApp
{
    public class MainVie
    {
        public MainVie ( IEnumerable < ITabGuest > guests ) { Guests = guests ; }

        public IEnumerable < ITabGuest > Guests { get ; }
    }

    public interface ITabGuest
    {
    }
}