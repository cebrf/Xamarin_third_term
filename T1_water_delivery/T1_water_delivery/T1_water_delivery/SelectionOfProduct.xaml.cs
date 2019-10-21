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
                numberOfProduct = stefan.Value;  //когда можно бует вводить кол-во самостоятельно, просто сделать стефана размером с введенное значение
                numOf.Text = numberOfProduct.ToString();
            };

            List<string> sone = new List<string> { "water", "bisque" };
            Picker pine = new Picker
            {
                ItemsSource = sone,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand
                WidthRequest = 90
            };
            pine.SelectedIndex = 0;
            chosenProduct = sone[pine.SelectedIndex];
            pine.SelectedIndexChanged += (e, f) =>
            { 
                if (pine.SelectedIndex != -1)
                {
                    chosenProduct = sone[pine.SelectedIndex];
                    numberOfProduct = 0;
                    numOf.Text = numberOfProduct.ToString();
                }
            };
            stive.Children.Add(pine);


            numOf.Placeholder = "number of product";
            numOf.PlaceholderColor = Color.Olive; //Color.OldLace
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

        private void confirm_Clicked(object sender, EventArgs e)
        {
            if (chosenProduct != null)
                Navigation.PopAsync();
        }
    }
}