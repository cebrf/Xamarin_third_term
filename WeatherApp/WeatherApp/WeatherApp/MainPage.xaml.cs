using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public List<string> chosenCities = new List<string>() { "Paris", "Moscow", "London", "Vladivostok" };
        RestService restService;

        public MainPage()
        {
            InitializeComponent();

            //TODO load chosen city from json
            restService = new RestService();
            getDayWeather(chosenCities[0]);
        }

        private void ChooseCity_Clicked(object sender, EventArgs e)
        {
            ChooseCityPage cityPage = new ChooseCityPage(chosenCities);
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
