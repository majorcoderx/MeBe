using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeBe.Views
{
    public sealed partial class ViewNote : Page
    {
        public ViewNote()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage.dispatcherTime.Tick+=dispatcherTime_Tick;
            MainPage.dispatcherTime.Interval = new TimeSpan(0, 0, 30);
            MainPage.dispatcherTime.Start();

            if (Note.notes.ID != 0)
            {
                
                InputTitleNote.Text = Note.notes.Title;
                InputNote.Text = Note.notes.Content;
            }
            else
            {
                InputNote.Text = "Nhập nội dung";
                InputTitleNote.Text = "Nhập tiêu đề";
            }
            TimeCreateNote.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
        }

        private void dispatcherTime_Tick(object sender, object e)
        {
            TimeCreateNote.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
        }

        private void BackNoteList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                SaveNote();
                this.Frame.GoBack();
                e.Handled = true;
            }
        }

        private void InputTitleNote_Keyup(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SaveNote();
                this.Frame.GoBack();
            }
        }

        private void SaveNote()
        {
            Note.notes.TimeCreate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            if (InputNote.Text == "")
            {
                Note.notes.Content = "Chưa nhập nội dung";
            }
            else Note.notes.Content = InputNote.Text;
            if (InputTitleNote.Text == "")
            {
                Note.notes.Title = "Tiêu đề trống";
            }
            else Note.notes.Title = InputTitleNote.Text;

            bool checkCreate = true;

            foreach (MeBe.Models.Notes value in Note.obnote)
            {
                if (Note.notes.ID == value.ID)
                {
                    checkCreate = false;
                }
                //System.Diagnostics.Debug.WriteLine(Note.notes.ID + " : " + value.ID);
            }

            if (checkCreate)
            {
                Note.pnote.InsertObject(Note.notes);
            }
            else
            {
                Note.pnote.UpdateObject(Note.notes);
            }
            Note.obnote = Note.pnote.GetListObject();

        }

        private void PointNote_Pressed(object sender, PointerRoutedEventArgs e)
        {
            if (InputTitleNote.Text.Equals("Nhập tiêu đề"))
            {
                InputTitleNote.Text = "";
            }
        }

        //private void ContentNote_KeyUp(object sender, KeyRoutedEventArgs e)
        //{
        //    if (e.Key == Windows.System.VirtualKey.Enter)
        //    {
        //        SaveNote();
        //        this.Frame.GoBack();
        //    }
        //}

        private void PointContentNote_Pressed(object sender, PointerRoutedEventArgs e)
        {
            if (InputNote.Text.Equals("Nhập nội dung"))
            {
                InputNote.Text = "";
            }
        }

        private void SaveNote_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SaveNote();
            this.Frame.GoBack();
        }

        private void DeleteNote_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Note.pnote.DeleteObject(Note.notes);
            Note.obnote = Note.pnote.GetListObject();
            this.Frame.GoBack();
        }

    }
}
