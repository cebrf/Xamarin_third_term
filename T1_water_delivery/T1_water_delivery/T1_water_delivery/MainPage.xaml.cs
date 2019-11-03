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
            public Product(int numberOfProd) 
            {
                this.numberOfProd = numberOfProd;
            }

            public string typeOfPr;
            protected int numberOfProd;
            protected Label currentProduct = new Label
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

        public class WaterProduct : Product
        {
            public WaterProduct(int numberOfProd) : base(numberOfProd) 
            {
                typeOfPr = "water";
            }
        }

        public class BiscuitProduct : Product
        {
            public BiscuitProduct(int numberOfProd) : base(numberOfProd)
            {
                typeOfPr = "biscuit";
            }
        }

        public class JuiceProduct : Product
        {
            public JuiceProduct(int numberOfProd) : base(numberOfProd)
            {
                typeOfPr = "juice";
            }
        }

        public class NukacolaProduct : Product
        {
            public NukacolaProduct(int numberOfProd) : base(numberOfProd)
            {
                typeOfPr = "nukacola";
            }
        }

        List<Product> allProducts = new List<Product>()
        {
            new WaterProduct(0),
            new BiscuitProduct(0),
            new JuiceProduct(0),
            new NukacolaProduct(0)
        };

        static protected Dictionary<string, int> numberOfProducts = new Dictionary<string, int>();

        public MainPage()
        {
            InitializeComponent();
        }

        private List<string> getProductsTipes()
        {
            List<string> products = new List<string>();
            for (int i = 0; i < allProducts.Count; i++)
            {
                products.Add(allProducts[i].typeOfPr);
            }
            return products;
        }
        private void addNewProduct_Clicked(object sender, EventArgs e)
        {
            List<string> products = getProductsTipes();
            SelectionOfProduct newPage = new SelectionOfProduct(products);
            newPage.Disappearing += (a, b) =>
            {
                if (newPage.chosenProduct != null && newPage.numberOfProduct != 0)
                {
                    if (numberOfProducts.ContainsKey(newPage.chosenProduct) && numberOfProducts[newPage.chosenProduct] != 0)
                    {
                        numberOfProducts[newPage.chosenProduct] += Convert.ToInt32(newPage.numberOfProduct);
                        allProducts.Find(i => i.typeOfPr == newPage.chosenProduct).makeChange(numberOfProducts[newPage.chosenProduct]);
                    }
                    else
                    {
                        numberOfProducts[newPage.chosenProduct] = Convert.ToInt32(newPage.numberOfProduct);
                        allProducts.Where(i => i.typeOfPr == newPage.chosenProduct).FirstOrDefault().makeChange(numberOfProducts[newPage.chosenProduct]);
                        allProducts.Find(i => i.typeOfPr == newPage.chosenProduct).deleteProduct.Clicked += (c, d) =>
                        {
                            numberOfProducts[newPage.chosenProduct] = 0;
                            mainStack.Children.Remove(allProducts.Find(i => i.typeOfPr == newPage.chosenProduct).st);
                        };
                        mainStack.Children.Add(allProducts.Find(i => i.typeOfPr == newPage.chosenProduct).st);
                    }
                }
            };
            Navigation.PushAsync(newPage);
        }

        async private void order_Clicked(object sender, EventArgs e)
        {
            if (mainStack.Children.Count == 0)
            {
                await DisplayAlert("", "Basket is empty", "Ok");
            }
            else
            {
                bool answer = await DisplayAlert("accept?", "Do you want to accept your order?", "Yes", "No");
                if (answer)
                {
                    await DisplayAlert("Your order is accepted", 
                        "Your order is accepted.\nPlease expect.\n\n" +
                        "Soon you will be contacted to clarify your order", "OK");
                    mainStack.Children.Clear();
                    numberOfProducts.Clear();
                }
            }
        }
    }
}
