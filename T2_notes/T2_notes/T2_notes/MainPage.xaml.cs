using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace T2_notes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void add_Clicked(object sender, EventArgs e)
        {
            EditorPage newPage = new EditorPage();
            newPage.Disappearing += (object a, EventArgs b) =>
            {
                string text;
                DateTime timeChanged = DateTime.Now;

                Label newNote = new Label();
                newNote.Margin = new Thickness(0, 0, 0, 0);
                newNote.LineBreakMode = LineBreakMode.TailTruncation;
                text = newPage.text;
                newNote.Text = text;

                if (text != null)
                {
                    Frame newNoteFrame = new Frame()
                    {
                        BackgroundColor = Color.Bisque,
                        BorderColor = Xamarin.Forms.Color.DarkSeaGreen,
                        Margin = new Thickness(0, 0, 0, 0),
                        Padding = new Thickness(5, 5, 0, 5),
                        Content = newNote
                    };

                    PanGestureRecognizer panGesture = new PanGestureRecognizer();
                    newNote.GestureRecognizers.Add(panGesture);
                    panGesture.PanUpdated += async (object panSender, PanUpdatedEventArgs panArgs) =>
                    {
                        switch (panArgs.StatusType)
                        {
                            case GestureStatus.Started:
                                newNote.TranslationX = panArgs.TotalX;
                                newNote.TranslationY = panArgs.TotalY;
                                break;

                            case GestureStatus.Completed:
                                var ab = newNote.TranslationX; // = 0
                                var ba = panArgs.TotalX; // = 0
                                if (newNote.TranslationX > panArgs.TotalX)
                                {
                                    if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                                    {
                                        //right.Children.Remove(panSender as Frame);
                                        leftContainer.Children.Remove(newNoteFrame);
                                    }
                                }
                                else
                                {
                                    if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                                    {
                                        //right.Children.Remove(panSender as Frame);
                                        rightContainer.Children.Remove(newNoteFrame);
                                    }
                                }
                                break;
                        }
                    };

                    if (leftContainer.Height <= rightContainer.Height)
                    {
                        leftContainer.Children.Add(newNoteFrame);
                    }
                    else
                    {
                        rightContainer.Children.Add(newNoteFrame);
                    }

                    var tap = new TapGestureRecognizer();
                    tap.Tapped += (tapSender, tapEven) =>
                    {
                        EditorPage editorPage = new EditorPage(timeChanged, text);
                        Navigation.PushAsync(editorPage);
                        editorPage.Disappearing += (_, __) =>
                        {
                            text = editorPage.text;
                            newNote.Text = text;
                            timeChanged = editorPage.timeChanged;
                        };
                    };
                    newNote.GestureRecognizers.Add(tap);
                }
            };
            Navigation.PushAsync(newPage);
        }
    }
}
