using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using MVVM_Prac.Model;
using Newtonsoft.Json;

namespace MVVM_Prac.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        private string ConfigFile;
        private dynamic ConfigJson;
        private string BaseUrl;
        private string SearchCitiesApi;
        private string CurrentConditions;
        private string API_KEY;

        public AccuWeatherHelper()
        {

            try
            {
                ConfigFile = "config.json";
                ConfigJson = JsonConvert.DeserializeObject(File.ReadAllText(ConfigFile));
                BaseUrl = ConfigJson.BaseUrl;
                SearchCitiesApi = BaseUrl + ConfigJson.SearchCitiesApi;
                CurrentConditions = BaseUrl + ConfigJson.CurrentConditions;
                API_KEY = "rbOviplrQcPTcnZXpZq4EVGPz25rGO8y";
                //InitializeAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error Occured please check your Config File");
                Environment.Exit(0);
            }
        }

        public async Task InitializeAsync()
        {
            try
            {
                var c = await GetCities("Delhi");
                await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("API not responding, please check your API KEY");
                Environment.Exit(0);
            }

        }

        public async Task<List<City>> GetCities(string req)
        {

            var cities = new List<City>();

            var url = string.Format(SearchCitiesApi, API_KEY, req);

            using (var client = new HttpClient())
            {
                var res = await client.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);

            }

            return cities;
        }

        public async Task<CurrentConditions> GetCurrentConditions(string id)
        {
            var c = new CurrentConditions();

            var url = string.Format(CurrentConditions, id, API_KEY);

            using (var cli = new HttpClient())
            {
                var res = await cli.GetAsync(url);
                var json = await res.Content.ReadAsStringAsync();
                c = JsonConvert.DeserializeObject<List<CurrentConditions>>(json)
                    .FirstOrDefault();
            }

            return c;
        }
    }
}
