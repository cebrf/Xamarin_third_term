using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;

namespace grad
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "SkiaGradi";
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSerface;
            Content = canvasView;


            InitializeComponent();
        }

        void OnCanvasViewPaintSerface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            //background
            using (SKPaint paint = new SKPaint())
            {
                SKRect rect = new SKRect(0, 0, info.Width, info.Height);

                //create linear gradient
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(rect.Left, rect.Bottom),
                    new SKPoint(rect.Right, rect.Top),
                    new SKColor[] { SKColors.DarkOliveGreen, SKColors.CadetBlue },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Repeat);

                canvas.DrawRect(rect, paint);
            }

            using (SKPaint paint = new SKPaint())
            {
                //create 300-pixel square rectangle
                float x = (info.Width - 300) / 2;
                float y = (info.Height - 300) / 2;
                SKRect rect = new SKRect(x, y, x + 300, y + 300);

                //create linear gradient
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(rect.Left, rect.Top),
                    new SKPoint(rect.Right, rect.Bottom),
                    new SKColor[] { SKColors.Blue, SKColors.Turquoise },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Repeat);

                canvas.DrawRect(rect, paint);
            }

            using (SKPaint paint = new SKPaint())
            {
                float x = (info.Width - 200);
                float y = (info.Height - 740);
                SKRect rect = new SKRect(x, y, x + 75, y + 507);

                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(rect.Left, rect.Bottom),
                    new SKPoint(rect.Right, rect.Top),
                    new SKColor[] { SKColors.CadetBlue, SKColors.DimGray },
                    new float[] { 0, 1 },
                    SKShaderTileMode.Repeat);

                canvas.DrawRect(rect, paint);
            }

        }
    }
}
