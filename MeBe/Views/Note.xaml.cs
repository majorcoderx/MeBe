using MeBe.Models;
using MeBe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Note : Page
    {
        public Note()
        {
            this.InitializeComponent();
        }

        public static ObservableCollection<Notes> obnote = new ObservableCollection<Notes>();
        public static Notes notes = new Notes();
        public static PNote pnote = new PNote();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SearchNote.Text = "Tìm kiếm";
            SearchNote.Foreground = new SolidColorBrush(Windows.UI.Colors.Silver);
            obnote = pnote.GetListObject();
            ListNote.ItemsSource = obnote;
        }

        private void NoteItem_Click(object sender, ItemClickEventArgs e)
        {
            notes = e.ClickedItem as Notes;
            this.Frame.Navigate(typeof(ViewNote));
        }

        private void SearchNote_keyUp(object sender, KeyRoutedEventArgs e)
        {
            listSearch.Clear();
            //processing text input, show result a search
            int[] index = new int[obnote.Count];
            string search = FilterMark(SearchNote.Text);
            int j = 0;
            foreach (Notes value in obnote)
            {
                string str = FilterMark(value.Title);
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

            for (int i = 0; i < obnote.Count; ++i)
            {              
                if (max == index[i] && max != 0)
                {
                    index[i] = 0;
                    listSearch.Add(obnote[i]);
                    max = MaxMin(index);
                }
            }
            ListNote.ItemsSource = listSearch;
            if (SearchNote.Text.Equals(""))
            {
                ListNote.ItemsSource = obnote;
            }
        }

        public int MaxMin(int[] index)
        {
            int max = 0;
            for (int i = 0; i < obnote.Count; ++i)
            {
                if (max < index[i])
                {
                    max = index[i];
                }
            }
            return max;
        }

        private static readonly string[] VietNamChar = new string[] 
        { 
        "aAeEoOuUiIdDyY", 
        "áàạảãâấầậẩẫăắằặẳẵ", 
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", 
        "éèẹẻẽêếềệểễ", 
        "ÉÈẸẺẼÊẾỀỆỂỄ", 
        "óòọỏõôốồộổỗơớờợởỡ", 
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", 
        "úùụủũưứừựửữ", 
        "ÚÙỤỦŨƯỨỪỰỬỮ", 
        "íìịỉĩ", 
        "ÍÌỊỈĨ", 
        "đ", 
        "Đ", 
        "ýỳỵỷỹ", 
        "ÝỲỴỶỸ" 
        };
        public static string FilterMark(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }

        ObservableCollection<Notes> listSearch = new ObservableCollection<Notes>(); 

        private void SearchNote_Pressed(object sender, PointerRoutedEventArgs e)
        {
            if (SearchNote.Text.Equals("Tìm kiếm"))
            {
                SearchNote.Text = "";
                SearchNote.Foreground = new SolidColorBrush(Windows.UI.Colors.HotPink);
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            if (senderElement != null)
            {
                var value = senderElement.DataContext as Notes;
                if (value != null)
                {
                    pnote.DeleteObject(value);
                    obnote.Remove(value);
                }
            }
        }

        private void ListNote_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);

            flyoutBase.ShowAt(senderElement);
        }

        private void CreateNote_Tapped(object sender, TappedRoutedEventArgs e)
        {
            notes = new Notes();
            this.Frame.Navigate(typeof(ViewNote));
        }
    }
}
