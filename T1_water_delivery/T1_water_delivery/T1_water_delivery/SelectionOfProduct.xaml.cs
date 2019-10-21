using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T1_water_delivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectionOfProduct : ContentPage
    {
        public string chosenProduct = null;
        public SelectionOfProduct()
        {
            InitializeComponent();
        }

        private void water_Clicked(object sender, EventArgs e)
        {
            chosenProduct = "water";
            Navigation.PopAsync();
        }

        private void bisque_Clicked(object sender, EventArgs e)
        {
            chosenProduct = "bisque";
            Navigation.PopAsync();
        }
    }
}