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
        MainPageWeather PageWeather;

        public MainPage()
        {
            InitializeComponent();

            PageWeather = new MainPageWeather();
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
            PageWeather.WeatherData = weatherData;
            MainPageWeather newWether = new MainPageWeather()
            {
                ForecastData = PageWeather.ForecastData,
                WeatherData = PageWeather.WeatherData
            };
            BindingContext = PageWeather;
        }

        async void getForecast(string cityName)
        {
            ForecastData forecastData = await restService.GetForecastData(GenerateRequestUri(Constants.OpenWeatherMapForecastEndpoint, cityName));
            PageWeather.ForecastData = forecastData;
            MainPageWeather newWether = new MainPageWeather()
            {
                ForecastData = PageWeather.ForecastData,
                WeatherData = PageWeather.WeatherData
            };
            BindingContext = newWether;
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


/*
 public MainPage()
        {
            InitializeComponent();

            PageWeather = new MainPageWeather();
            App.chosenCities = new List<string>() { "Paris", "Moscow", "London", "Vladivostok, RU" };
            //TODO load chosen city from json
            restService = new RestService();
            getDayWeather(App.chosenCities[0]);
            //getForecast(App.chosenCities[0]);
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
                    //getForecast(chosenCity);

                    // TODO get info about city

                    //TODO save chosen city to json
                }

            };
            Navigation.PushAsync(cityPage);
        }
        async void getDayWeather(string cityName)
        {
            WeatherData weatherData = await restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint, cityName));
            
            PageWeather.WeatherData = weatherData;
            MainPageWeather newWether = new MainPageWeather()
            {
                ForecastData = PageWeather.ForecastData,
                WeatherData = PageWeather.WeatherData
            };
            BindingContext = PageWeather;
        }

        async void getForecast(string cityName)
        {
            ForecastData forecastData = await restService.GetForecastData(GenerateRequestUri(Constants.OpenWeatherMapForecastEndpoint, cityName));
            PageWeather.ForecastData = forecastData;
            MainPageWeather newWether = new MainPageWeather() 
            { 
                ForecastData = PageWeather.ForecastData,
                WeatherData = PageWeather.WeatherData
            };
            BindingContext = newWether;
        }

string GenerateRequestUri(string endpoint, string cityName)
{
    string requestUri = endpoint;
    requestUri += $"?q={cityName}";
    requestUri += "&units=metric"; // or units=imperials
    requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
    return requestUri;
}
     */