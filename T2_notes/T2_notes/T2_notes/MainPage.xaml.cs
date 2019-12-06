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
                    panGesture.PanUpdated += (object senderP, PanUpdatedEventArgs eP) =>
                    {
                        switch (eP.StatusType)
                        {
                            case GestureStatus.Started:
                                Content.TranslationX = eP.TotalX;
                                Content.TranslationY = eP.TotalY;
                                break;

                            case GestureStatus.Completed:
                                if (leftContainer.Height <= rightContainer.Height)
                                { 
                                    if (Content.TranslationX > eP.TotalX)
                                    {
                                        leftContainer.Children.Remove(newNoteFrame);
                                    }
                                }
                                else
                                {
                                    if (Content.TranslationX < eP.TotalX)
                                    {
                                        rightContainer.Children.Remove(newNoteFrame);
                                    }
                                }
                                break;
                        }
                    };
                    newNote.GestureRecognizers.Add(panGesture);

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
