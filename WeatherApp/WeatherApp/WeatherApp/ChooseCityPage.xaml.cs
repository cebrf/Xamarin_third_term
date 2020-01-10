using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseCityPage : ContentPage
    {
        public string chosenCity = null;
        public ChooseCityPage()
        {
            InitializeComponent();

            foreach (var cityName in App.chosenCities)
            {
                Label cityLabel = new Label()
                {
                    Margin = new Thickness(5, 0, 0, 5),
                    TextColor = Color.FloralWhite,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = cityName
                };
                Frame cityFrame = new Frame()
                {
                    BackgroundColor = Color.DimGray,
                    Margin = new Thickness(0, 0, 0, 0),
                    Padding = new Thickness(15, 15, 0, 15),
                    Content = cityLabel
                };
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    chosenCity = cityLabel.Text;
                    Navigation.PopAsync();
                };
                cityFrame.GestureRecognizers.Add(tapGestureRecognizer);
                chosenCities.Children.Add(cityFrame);
            }
        }

        private void addCity_Clicked(object sender, EventArgs eArgs)
        {
            AddCityPage addPage = new AddCityPage();
            addPage.Disappearing += (s, e) =>
            {
                Label cityLabel = new Label()
                {
                    Margin = new Thickness(5, 0, 0, 5),
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = addPage.chosenCity
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
                chosenCities.Children.Add(cityFrame);

                chosenCity = addPage.chosenCity;
                if (!App.chosenCities.Contains(chosenCity))
                    App.chosenCities.Add(chosenCity);
                Navigation.PopAsync();
            };
            Navigation.PushAsync(addPage);
        }
    }
}
 