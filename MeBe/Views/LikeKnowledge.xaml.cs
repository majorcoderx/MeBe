using MeBe.Models;
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
    public sealed partial class LikeKnowledge : Page
    {
        public LikeKnowledge()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            string check = (string) localSetting.Values["CheckBorn"];
            if (check.Equals("false"))
            {
                Knowledge.know = Knowledge.pknow.GetListObject(0);
            }
            else
            {
                Knowledge.know = Knowledge.pknow.GetListObject(0);
            }
            CountLikeKnowledgeText.Text = Knowledge.know.Count.ToString();
            ListLikeKnowledge.ItemsSource = Knowledge.know;
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void MenuIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void ListLikeKnowledge_ItemClick(object sender, ItemClickEventArgs e)
        {
            Knowledge.knowledge = e.ClickedItem as Knowledges;
            this.Frame.Navigate(typeof(ViewKnowledge));
        }

        private void UnLikeKnowledge_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            if (senderElement != null)
            {
                var value = senderElement.DataContext as Knowledges;
                if (value != null)
                {
                    value.Like = 0;
                    Knowledge.pknow.UpdateObject(value);
                    Knowledge.know.Remove(value);
                }
            }
        }

        private void LikeKnowledge_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            flyoutBase.ShowAt(senderElement);
        }

        private void SearchLikeKnowledge_Pressed(object sender, PointerRoutedEventArgs e)
        {
            if (SearchLikeKnowledge.Text.Equals("Tìm kiếm"))
            {
                SearchLikeKnowledge.Text = "";
                SearchLikeKnowledge.Foreground = new SolidColorBrush(Windows.UI.Colors.HotPink);
            }
        }

        ObservableCollection<Knowledges> listSearch = new ObservableCollection<Knowledges>();

        private void SearchLikeKnowledge_keyUp(object sender, KeyRoutedEventArgs e)
        {
            listSearch.Clear();
            //processing text input, show result a search
            int[] index = new int[Knowledge.know.Count];
            string search = Note.FilterMark(SearchLikeKnowledge.Text);
            int j = 0;
            foreach (Knowledges value in Knowledge.know)
            {
                string str = Note.FilterMark(value.Title);
                if (str.Length >= search.Length)
                {
                    for (int i = 0; i < search.Length; ++i)
                    {
                        if (string.Compare(str[i].ToString(), search[i].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ++index[j];
                        }
                        else break;
                    }
                }
                else
                {
                    for (int i = 0; i < str.Length; ++i)
                    {
                        if (string.Compare(str[i].ToString(), search[i].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ++index[j];
                        }
                        else break;
                    }
                }
                ++j;
            }

            int max = 0;
            max = MaxMin(index);

            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if (max == index[i] && max != 0)
                {
                    index[i] = 0;
                    listSearch.Add(Knowledge.know[i]);
                    max = MaxMin(index);
                }
            }
            ListLikeKnowledge.ItemsSource = listSearch;
            if ( SearchLikeKnowledge.Text.Equals(""))
            {
                ListLikeKnowledge.ItemsSource = Knowledge.know;
            }
        }

        public int MaxMin(int[] index)
        {
            int max = 0;
            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if (max < index[i])
                {
                    max = index[i];
                }
            }
            return max;
        }
    }
}
