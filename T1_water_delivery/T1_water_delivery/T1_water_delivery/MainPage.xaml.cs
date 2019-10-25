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
        public class Product
        {
            public Product(int numberOfProd_, string typeOfPr_) 
            {
                typeOfPr = typeOfPr_;
                numberOfProd = numberOfProd_;
            }

            string typeOfPr;
            int numberOfProd;
            Label currentProduct = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(20, 0, 0, 0)
            };

            public Label numberOfProduct = new Label
            {
                FontSize = 18,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            public Button deleteProduct = new Button
            {
                Text = "X",
                Margin = new Thickness(10),
                BackgroundColor = new Color(64, 74, 3),
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 40
            };

            public StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(10, 10, 10, 0),
                BackgroundColor = new Color(255, 228, 196)
            };

            public void makeChange(int num)
            {
                numberOfProd = num;
                currentProduct.Text = typeOfPr;
                numberOfProduct.Text = numberOfProd.ToString();
                st.Children.Add(currentProduct);
                st.Children.Add(numberOfProduct);
                st.Children.Add(deleteProduct);
            }
        };

        protected Dictionary<string, Product> allProducts = new Dictionary<string, Product>
        {
            { "water", new Product(0, "water") },
            { "biscuit", new Product(0, "biscuit") },
            { "juice", new Product(0, "juice") },
            { "nukacola", new Product(0, "nukacola") },
        };

        static protected Dictionary<string, int> numberOfProducts = new Dictionary<string, int>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void addNewProduct_Clicked(object sender, EventArgs e)
        {
            SelectionOfProduct newPage = new SelectionOfProduct();
            newPage.Disappearing += (a, b) =>
            {
                if (newPage.chosenProduct != null && newPage.numberOfProduct != 0)
                {
                    if (numberOfProducts.ContainsKey(newPage.chosenProduct) && numberOfProducts[newPage.chosenProduct] != 0)
                    {
                        numberOfProducts[newPage.chosenProduct] += Convert.ToInt32(newPage.numberOfProduct);
                        allProducts[newPage.chosenProduct].makeChange(numberOfProducts[newPage.chosenProduct]);
                    }
                    else
                    {
                        numberOfProducts[newPage.chosenProduct] = Convert.ToInt32(newPage.numberOfProduct);
                        allProducts[newPage.chosenProduct].makeChange(numberOfProducts[newPage.chosenProduct]);
                        allProducts[newPage.chosenProduct].deleteProduct.Clicked += (c, d) =>
                        {
                            numberOfProducts[newPage.chosenProduct] = 0;
                            mainStack.Children.Remove(allProducts[newPage.chosenProduct].st);
                        };
                        mainStack.Children.Add(allProducts[newPage.chosenProduct].st);
                    }
                }
            };
            Navigation.PushAsync(newPage);
        }

        private void order_Clicked(object sender, EventArgs e)
        {
            if (mainStack.Children.Count == 0)
            {

            }
            else
            {
                DisplayAlert("Your order is accepted", "Your order is accepted.\nPlease expect.\n\nSoon you will be contacted to clarify your order", "OK");
                mainStack.Children.Clear();
                numberOfProducts.Clear();

                //string answer = DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            }
        }
    }
}
