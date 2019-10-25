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
        public double numberOfProduct = 0;
        public SelectionOfProduct()
        {
            InitializeComponent();

            numOf.Text = numberOfProduct.ToString();
            stefan.ValueChanged += (sender, e) =>
            {
                numberOfProduct = stefan.Value;
                numOf.Text = numberOfProduct.ToString();
            };

            List<string> sone = new List<string> { "water", "biscuit", "juice", "nukacola" };
            List<string> pict = new List<string> { "water.jpg", "biscuit.jpg", "juice.jpg", "nukacola.png" };
            pine.ItemsSource = sone;
            pine.SelectedIndex = 0;
            chosenProduct = sone[pine.SelectedIndex];
            pine.SelectedIndexChanged += (e, f) =>
            { 
                if (pine.SelectedIndex != -1)
                {
                    chosenProduct = sone[pine.SelectedIndex];
                    ima.Source = ImageSource.FromFile(pict[pine.SelectedIndex]);
                }
            };
            stive.Children.Add(pine);

            numOf.TextChanged += (e, f) =>
            {
                if (numOf.Text == null || numOf.Text == "")
                {
                    stefan.Value = 0;
                    numberOfProduct = stefan.Value;
                    numOf.Text = "0";
                }
                else
                {
                    stefan.Value = Double.Parse(numOf.Text);
                    numberOfProduct = stefan.Value;
                }
            };


            ima.Source = ImageSource.FromFile("water.jpg");
        }

        private void confirm_Clicked(object sender, EventArgs e)
        {
            if (chosenProduct != null)
                Navigation.PopAsync();
        }
    }
}