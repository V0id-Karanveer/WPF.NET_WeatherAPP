using System.Windows;
using Prism.Ioc;
using Prism.Unity;
using MVVM_Prac.View;

namespace MVVM_Prac
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DayView>();
            containerRegistry.RegisterForNavigation<RainView>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<WeatherWindow>();
        }
    }
}
