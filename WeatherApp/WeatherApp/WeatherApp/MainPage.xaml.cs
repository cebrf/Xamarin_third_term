using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        RestService restService;

        public MainPage()
        {
            InitializeComponent();

            App.chosenCities = new List<string>() { "Paris", "Moscow", "London", "Vladivostok, RU" };
            //TODO load chosen city from json
            restService = new RestService();
            getDayWeather(App.chosenCities[0]);
        }

        private void ChooseCity_Clicked(object sender, EventArgs e)
        {
            ChooseCityPage cityPage = new ChooseCityPage();
            cityPage.Disappearing += (object cityPageSender, EventArgs cityPageargs) =>
            {
                var chosenCity = cityPage.chosenCity;
                if (chosenCity != null)
                {
                    getDayWeather(chosenCity);

                    // TODO get info about city

                    //TODO save chosen city to json
                }

            };
            Navigation.PushAsync(cityPage);
        }
        async void getDayWeather(string cityName)
        {
            WeatherData weatherData = await restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint, cityName));
            BindingContext = weatherData;
        }
        string GenerateRequestUri(string endpoint, string cityName)
        {
            string requestUri = endpoint;
            requestUri += $"?q={cityName}";
            requestUri += "&units=metric"; // or units=imperials
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}
