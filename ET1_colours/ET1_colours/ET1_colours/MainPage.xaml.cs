using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ET1_colours
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public class RgbColorExtension : IMarkupExtension<Color>
    {
        public int R { set; get; }

        public int G { set; get; }

        public int B { set; get; }

        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            Color a = Color.FromRgb(R / 255.0, G / 255.0, B / 255.0);
            return a;
        }
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Color>).ProvideValue(serviceProvider);
        }
    }

    public partial class MainPage : ContentPage
    {
        private Color myColourProperty;
        public Color MyColourProperty
        {
            get { return myColourProperty; }
            set
            {
                myColourProperty = value;
                OnPropertyChanged(nameof(MyColourProperty));
                Console.WriteLine();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            List<string> _colors = new List<string>();
            foreach (var field in typeof(Xamarin.Forms.Color).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (field != null && !String.IsNullOrEmpty(field.Name))
                {
                    _colors.Add(field.Name);
                }
            }

            ColorTypeConverter converter = new ColorTypeConverter();
            picker.Title = "Select colour";
            picker.ItemsSource = _colors;


            StackLayout sliserContainer = new StackLayout
            {
                Margin = 10,
                BackgroundColor = Color.FromRgb(102, 102, 153),
                VerticalOptions = LayoutOptions.EndAndExpand,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Label rValLabel = new Label
            {
                TextColor = Color.FromRgb(255, 255, 240),
                FontSize = 16,
                Text = "0",
            };
            Slider rSlider = new Slider
            {
                Minimum = 0,
                Maximum = 255,
                Margin = new Thickness(10, 10, 10, 0)
            };
            sliserContainer.Children.Add(rValLabel);
            sliserContainer.Children.Add(rSlider);

            Label gValLabel = new Label
            {
                TextColor = Color.FromRgb(255, 255, 240),
                FontSize = 16,
                Text = "0",
            };
            Slider gSlider = new Slider
            {
                Minimum = 0,
                Maximum = 255,
                Margin = new Thickness(10, 0, 10, 00)
            };
            sliserContainer.Children.Add(gValLabel);
            sliserContainer.Children.Add(gSlider);

            Label bValLabel = new Label
            {
                TextColor = Color.FromRgb(255, 255, 240),
                FontSize = 16,
                Text = "0",
            };
            Slider bSlider = new Slider
            {
                Minimum = 0,
                Maximum = 255,
                Margin = new Thickness(10, 0, 10, 10)
            };
            sliserContainer.Children.Add(bValLabel);
            sliserContainer.Children.Add(bSlider);

            mainSt.Children.Add(sliserContainer);


            rSlider.ValueChanged += (sender, args) =>
            {
                rValLabel.Text = "  red: " + ((int)args.NewValue).ToString();
                MyColourProperty = Color.FromRgb((int)rSlider.Value, (int)gSlider.Value, (int)bSlider.Value);

            };
            gSlider.ValueChanged += (sender, args) =>
            {
                gValLabel.Text = "  green: " + ((int)args.NewValue).ToString();
                MyColourProperty = Color.FromRgb((int)rSlider.Value, (int)gSlider.Value, (int)bSlider.Value);
            };
            bSlider.ValueChanged += (sender, args) =>
            {
                bValLabel.Text = "  blue: " + ((int)args.NewValue).ToString();
                MyColourProperty = Color.FromRgb((int)rSlider.Value, (int)gSlider.Value, (int)bSlider.Value);
            };

            picker.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                Color newColor = (Color)(converter.ConvertFromInvariantString(_colors[picker.SelectedIndex]));
                picker.TitleColor = newColor;
                MyColourProperty = newColor;

                rSlider.Value = (int)(newColor.R * 256);
                gSlider.Value = (int)(newColor.G * 256);
                bSlider.Value = (int)(newColor.B * 256);
                rValLabel.Text = "red: " + rSlider.Value.ToString();
                gValLabel.Text = "green: " + gSlider.Value.ToString();
                bValLabel.Text = "blue: " + bSlider.Value.ToString();
            };
            picker.SelectedIndex = 1;
        }
    }
}
