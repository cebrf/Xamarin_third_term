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
            numStep.ValueChanged += (sender, e) =>
            {
                numberOfProduct = numStep.Value;
                numOf.Text = numberOfProduct.ToString();
            };

            List<string> products = new List<string> { "water", "biscuit", "juice", "nukacola" };
            List<string> pict = new List<string> { "water.jpg", "biscuit.jpg", "juice.jpg", "nukacola.png" };
            choosePr.ItemsSource = products;
            choosePr.SelectedIndex = 0;
            chosenProduct = products[choosePr.SelectedIndex];
            choosePr.SelectedIndexChanged += (e, f) =>
            { 
                if (choosePr.SelectedIndex != -1)
                {
                    chosenProduct = products[choosePr.SelectedIndex];
                    ima.Source = ImageSource.FromFile(pict[choosePr.SelectedIndex]);
                }
            };
            mainStack.Children.Add(choosePr);

            numOf.TextChanged += (e, f) =>
            {
                if (numOf.Text == null || numOf.Text == "")
                {
                    numStep.Value = 0;
                    numberOfProduct = numStep.Value;
                    numOf.Text = "0";
                }
                else
                {
                    numStep.Value = Double.Parse(numOf.Text);
                    numberOfProduct = numStep.Value;
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