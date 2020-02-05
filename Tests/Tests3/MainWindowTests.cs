using Autofac ;
using TestLib ;
using TestLib.Fixtures ;
using WpfApp ;
using WpfApp1Tests3;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfTApp1Tests3
{
public class MainWindowTests  : WpfTestsBase
{
[WpfFact]
public void MainWindowTestsShow()
{
var mainWindow = containerScope.Resolve<MainWindow>();
mainWindow.Show();
}

protected MainWindowTests ( WpfApplicationFixture fixture , AppContainerFixture containerFixture , UtilsContainerFixture utilsContainerFixture , ITestOutputHelper outputHelper ) : base ( fixture , containerFixture , utilsContainerFixture , outputHelper )
{
}
}
}
