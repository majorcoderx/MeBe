using MeBe.Models;
using MeBe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Knowledge : Page
    {
        public static ObservableCollection<Knowledges> know = new ObservableCollection<Knowledges>();
        public static Knowledges knowledge = new Knowledges();
        public static PKnowledge pknow = new PKnowledge();

        public Knowledge()
        {
            this.InitializeComponent();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WeekButton.IsChecked = true;
            MonthButton.IsChecked = false;
            LoadDataKnowledge();
        }

        public void LoadDataKnowledge()
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            string _check = (string)localSetting.Values["CheckBorn"];
            if (_check.Equals("false"))
            {
                if (WeekButton.IsChecked == true)
                {
                    know = pknow.GetListObject(MeBe.MainPage._week, 0,0);
                }
                else
                {
                    know = pknow.GetListObject(MeBe.MainPage._week, 4,0);
                }
            }
            else
            {
                if (WeekButton.IsChecked == true)
                {
                    know = pknow.GetListObject(MeBe.MainPage._week, 0,0);
                }
                else
                {
                    know = pknow.GetListObject(MeBe.MainPage._week, 4,0);
                }
            }
            KnowledgeListView.ItemsSource = know;
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void WeekButton_Checked(object sender, RoutedEventArgs e)
        {
            WeekButton.IsChecked = true;
            MonthButton.IsChecked = false;
            LoadDataKnowledge();
        }

        private void MonthButton_Checked(object sender, RoutedEventArgs e)
        {
            WeekButton.IsChecked = false;
            MonthButton.IsChecked = true;
            LoadDataKnowledge();
        }

        private void ViewLikeKnowledge_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LikeKnowledge));
        }

        private void KnowledgeItem_Click(object sender, ItemClickEventArgs e)
        {
            knowledge = e.ClickedItem as Knowledges;
            this.Frame.Navigate(typeof(ViewKnowledge));
        }
    }
}
