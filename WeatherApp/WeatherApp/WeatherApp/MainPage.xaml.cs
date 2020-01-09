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
        public MainPage()
        {
            //TODO load chosen city from json
            InitializeComponent();
        }

        private void ChooseCity_Clicked(object sender, EventArgs e)
        {
            ChooseCityPage cityPage = new ChooseCityPage();
            cityPage.Disappearing += (object cityPageSender, EventArgs cityPageargs) =>
            {
                // TODO get name of city
                // TODO get info about city

                //TODO save chosen city to json
            };
            Navigation.PushAsync(cityPage);
        }
    }
}
