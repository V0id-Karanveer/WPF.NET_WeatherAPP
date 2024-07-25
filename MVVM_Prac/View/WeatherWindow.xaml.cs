using System.Windows;
using Prism.Regions;
using MVVM_Prac.ViewModel;

namespace MVVM_Prac.View
{
    public partial class WeatherWindow : Window
    {
        public WeatherWindow(IRegionManager regionManager)
        {
            DataContext = new WeatherVm(regionManager);
            InitializeComponent();
            RegionManager.SetRegionManager(this, regionManager);
            

        }
    }
}
