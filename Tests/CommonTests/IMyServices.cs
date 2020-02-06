#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// IMyServices.cs
// 
// 2020-02-02-5:22 PM
// 
// ---
#endregion
using WpfApp.Core.Infos ;

namespace Tests.CommonTests
{
    public interface IMyServices

    {
        InfoContext.Factory InfoContextFactory { get ; }
    }
}