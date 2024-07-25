using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Prac.View;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MVVM_Prac.Modules
{
    public class WeatherModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public WeatherModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _regionManager.RegisterViewWithRegion("AdditionalOptionsRegion", typeof(AdditionalOptionsView));
        }
    }
}
