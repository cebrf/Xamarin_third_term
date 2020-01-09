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
        public ChooseCityPage(List<string> cities)
        {
            InitializeComponent();

            foreach (var cityName in cities)
            {
                Label cityLabel = new Label()
                {
                    Margin = new Thickness(5, 0, 0, 5),
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

        private void addCity_Clicked(object sender, EventArgs e)
        {

        }
    }
}