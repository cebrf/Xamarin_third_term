using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        protected void checkCount()
        {
            while (leftContainer.Children.Count <= rightContainer.Children.Count - 2) // add in left from right
            {
                var note = rightContainer.Children[rightContainer.Children.Count - 1];
                rightContainer.Children.Remove(note);

                leftContainer.Children.Add(note);
            }

            while (rightContainer.Children.Count <= leftContainer.Children.Count - 2) // add in right from left
            {
                var note = leftContainer.Children[leftContainer.Children.Count - 1];
                leftContainer.Children.Remove(note);

                rightContainer.Children.Add(note);
            }

        }

        protected void addNote(string text, DateTime timeChanged, char pos = ' ')
        {            
            Label noteText = new Label()
            {
                Margin = new Thickness(0, 0, 0, 5),
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = text
            };
            Label noteTime = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = timeChanged.ToString()
            };
            StackLayout newNote = new StackLayout()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Children = { noteText, noteTime }
            };

            Frame newNoteFrame = new Frame()
            {
                BackgroundColor = Color.Bisque,
                BorderColor = Xamarin.Forms.Color.DarkSeaGreen,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(15, 15, 0, 15),
                Content = newNote
            };

            var pan = new PanGestureRecognizer();
            double totalX = 0;
            pan.PanUpdated += async (panSender, panArgs) =>
            {
                switch (panArgs.StatusType)
                {
                    case GestureStatus.Canceled:
                    case GestureStatus.Started:
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Completed:
                        if (totalX > 0 && rightContainer.Children.Contains(newNoteFrame))
                        {
                            if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                            {
                                rightContainer.Children.Remove(newNoteFrame);
                                checkCount();
                                safeNotes();
                            }
                            totalX = 0;
                        }
                        else if (totalX < 0 && leftContainer.Children.Contains(newNoteFrame))
                        {
                            if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                            {
                                leftContainer.Children.Remove(newNoteFrame);
                                checkCount();
                                safeNotes();
                            }
                            totalX = 0;
                        }
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Running:
                        if (panArgs.TotalX > 0 && rightContainer.Children.Contains(newNoteFrame))
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        else if (panArgs.TotalX < 0 && leftContainer.Children.Contains(newNoteFrame))
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        break;
                }
            };
            newNote.GestureRecognizers.Add(pan);

            if (pos == 'l')
            {
                leftContainer.Children.Add(newNoteFrame);
            }
            else if (pos == 'r')
            {
                rightContainer.Children.Add(newNoteFrame);
            }
            else
            {
                if (leftContainer.Children.Count <= rightContainer.Children.Count)
                {
                    leftContainer.Children.Add(newNoteFrame);
                }
                else
                {
                    rightContainer.Children.Add(newNoteFrame);
                }
            }



            var tap = new TapGestureRecognizer();
            tap.Tapped += (tapSender, tapEven) =>
            {
                EditorPage editorPage_ = new EditorPage(timeChanged, text);
                Navigation.PushAsync(editorPage_);
                editorPage_.Disappearing += (_, __) =>
                {
                    text = editorPage_.text;
                    noteText.Text = text;
                    timeChanged = editorPage_.timeChanged;
                    noteTime.Text = timeChanged.ToString();
                };
            };
            newNote.GestureRecognizers.Add(tap);
        }

        public MainPage()
        {
            InitializeComponent();

            string newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savedNotes.json");
            if (File.Exists(newFile))
            {
                JObject savedNotes = JObject.Parse(File.ReadAllText(newFile));
                foreach (JObject item in savedNotes["left"])
                {
                    addNote((string)item.GetValue("text"), DateTime.Parse((string)item.GetValue("time")), 'l');
                }
                foreach (JObject item in savedNotes["right"])
                {
                    addNote((string)item.GetValue("text"), DateTime.Parse((string)item.GetValue("time")), 'r');
                }
            }
        }

        private void safeNotes()
        {
            string newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savedNotes.json");

            JArray left = new JArray();
            JArray right = new JArray();
            foreach (Frame note in leftContainer.Children)
            {
                left.Add(new JObject(
                    new JProperty("text", ((Label)((StackLayout)note.Content).Children[0]).Text),
                    new JProperty("time", ((Label)((StackLayout)note.Content).Children[1]).Text)
                ));
            }
            foreach (Frame note in rightContainer.Children)
            {
                right.Add(new JObject(
                    new JProperty("text", ((Label)((StackLayout)note.Content).Children[0]).Text),
                    new JProperty("time", ((Label)((StackLayout)note.Content).Children[1]).Text)
                ));
            }

            JObject savedNotes = new JObject
            (
                new JProperty("left", left),
                new JProperty("right", right)
            );
            File.WriteAllText(newFile, savedNotes.ToString());

        }

        private void add_Clicked(object sender, EventArgs e)
        {
            EditorPage editorPage = new EditorPage();
            editorPage.Disappearing += (object editorPageSender, EventArgs editorPageargs) =>
            {
                if (editorPage.text == null)
                    return;

                string text = editorPage.text;
                DateTime timeChanged = DateTime.Now;
                addNote(text, timeChanged);
                safeNotes();
            };
            Navigation.PushAsync(editorPage);
        }
    }
}
