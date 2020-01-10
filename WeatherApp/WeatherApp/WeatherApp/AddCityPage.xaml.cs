using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCityPage : ContentPage
    {
        public string chosenCity;
        public JArray AllCities;
        //SortedSet<CityData> AllCities;
        void GetJsonData()
        {
            string jsonFileName = "city.list.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();
                AllCities = JsonConvert.DeserializeObject<JArray>(jsonString);
            }

            // TODO use this
            // deserialize JSON directly from a file
            /*using (StreamReader reader = new System.IO.StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                AllCities = (SortedSet<CityData>)serializer.Deserialize(reader, typeof(CityData[]));
            }*/
        }

        public AddCityPage()
        {
            InitializeComponent();
            GetJsonData();
        }

        private void cityInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            citiesContainer.Children.Clear();
            if (cityInput.Text == null || cityInput.Text == "")
                return;

            foreach (var city in AllCities
                .Where(obj => obj["name"].Value<string>().ToLower().StartsWith(cityInput.Text)))
            {
                Label cityLabel = new Label()
                {
                    Margin = new Thickness(5, 0, 0, 5),
                    TextColor = Color.GhostWhite,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = city["name"].Value<string>() + ",  " + city["country"].Value<string>()
                };
                Frame cityFrame = new Frame()
                {
                    BackgroundColor = Color.DimGray,
                    Margin = new Thickness(0, 0, 0, 0),
                    Padding = new Thickness(15, 15, 0, 15),
                    Content = cityLabel
                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (_s, _e) => {
                    chosenCity = cityLabel.Text;
                    Navigation.PopAsync();
                };
                cityFrame.GestureRecognizers.Add(tapGestureRecognizer);
                citiesContainer.Children.Add(cityFrame);
            }
        }
    }
}