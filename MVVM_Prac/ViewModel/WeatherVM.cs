using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using MVVM_Prac.Model;
using MVVM_Prac.ViewModel.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MVVM_Prac.ViewModel
{
    public class WeatherVm : BindableBase
    {
        public IRegionManager RegionManager;

        private string _query;

        private AccuWeatherHelper _api = new AccuWeatherHelper();

        public string Query
        {
            get => _query;
            set => SetProperty(ref _query, value);
        }

        private ObservableCollection<City> _cities;

        public ObservableCollection<City> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        private CurrentConditions _currentConditions;

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set => SetProperty(ref _currentConditions, value);
        }

        private City _selectedCity;

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                SetProperty(ref _selectedCity, value);

                if (SelectedCity != null)
                {
                    GetCurrentConditions();
                }
            }
        }

        private ObservableCollection<MenuItem> _menuItems;

        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private MenuItem _selectedMenuItem;

        public MenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                SetProperty(ref _selectedMenuItem, value);
                ShowExternalConditionView();
            }
        }

        public DelegateCommand SearchCommand { get; private set; }

        public WeatherVm(IRegionManager regionManager)
        {
            SearchCommand = new DelegateCommand(ExecuteSearchCommand);
            Cities = new ObservableCollection<City>();
            RegionManager = regionManager;
        }

        public async void ExecuteSearchCommand()
        {
            var cities = await _api.GetCities(Query);
            Cities = new ObservableCollection<City>(cities);

            SelectedCity = null;
        }

        public async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();

            CurrentConditions = await _api.GetCurrentConditions(SelectedCity.Key);
            var jsonFilePath = "MenuItems.json";
            var jsonText = File.ReadAllText(jsonFilePath);
            MenuItems = JsonConvert.DeserializeObject<ObservableCollection<MenuItem>>(jsonText);
        }

        public void ShowExternalConditionView()
        {
            var existingView = RegionManager.Regions["AdditionalOptionsRegion"].ActiveViews.FirstOrDefault();
            if (existingView != null)
            {
                RegionManager.Regions["AdditionalOptionsRegion"].Remove(existingView);
            }

            if (SelectedMenuItem is null)
            {
                return;
            }

            RegionManager.RegisterViewWithRegion("AdditionalOptionsRegion", SelectedMenuItem.ViewName);
        }
    }
}
