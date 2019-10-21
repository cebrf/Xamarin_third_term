using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace T1_water_delivery
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            mainLabel.Text = "Astaroth";
        }

        private void goNext_Clicked(object sender, EventArgs e)
        {
            SelectionOfProduct newPage = new SelectionOfProduct();
            Navigation.PushAsync(newPage);
        }
    }
}
