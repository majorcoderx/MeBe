using MeBe.Models;
using MeBe.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeBe.Views
{

    public sealed partial class Baby : Page
    {
        public Baby()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeViewBaby.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
            MainPage.dispatcherTime.Tick += dispatcherTime_Tick;
            MainPage.dispatcherTime.Interval = new TimeSpan(0, 0, 30);

            var localSettings = ApplicationData.Current.LocalSettings;
            Object value = localSettings.Values["CheckSex"];
            string checkSex = (string)value;
            if (value == null)
            {
                CheckSexImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/check.png"));
                localSettings.Values["CheckSex"] = "Girls";
            }
            else
            {
                if (!checkSex.Equals("Girls"))
                {
                    CheckSexImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/uncheck.png"));
                    localSettings.Values["CheckSex"] = "Boy";
                }
                else
                {
                    CheckSexImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/check.png"));
                    localSettings.Values["CheckSex"] = "Girls";
                }
            }
        }

        void dispatcherTime_Tick(object sender, object e)
        {
            TimeViewBaby.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void CheckSex_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            Object value = localSetting.Values["CheckSex"];
            string checkSex = (string)value;
            if (!checkSex.Equals("Girls"))
            {
                localSetting.Values["CheckSex"] = "Girls";
                CheckSexImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/check.png"));
            }
            else
            {
                localSetting.Values["CheckSex"] = "Boy";
                CheckSexImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/uncheck.png"));
            }
        }

        private void CanelClick(IUICommand command)
        {
            this.Frame.GoBack();
        }

        private void OkClick(IUICommand command)
        {
            InputHeightBaby.Text = "";
            InputWeightBaby.Text = "";
        }

        public async void MessageBox(string title, string content)
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand("Ok", OkClick));
            dialog.Commands.Add(new UICommand("Canel", CanelClick));

            dialog.DefaultCommandIndex = 1;
            await dialog.ShowAsync();
        }

        public Index ProcessTextInput(string txtHeight, string txtWeight)
        {
            Index index = new Index();
            bool checkHeght = true;
            bool checkWeight = true;
            double _height = 0;
            double _weight = 0;
            if (!txtHeight.Equals(""))
            {
                if (txtHeight.Contains("."))
                {
                    if (txtHeight.IndexOf(".") == txtHeight.LastIndexOf(".") && txtHeight.Length > 2)
                    {
                        txtHeight = txtHeight.Replace(".", ",");
                        _height = Convert.ToDouble(txtHeight);
                    }
                    else
                    {
                        MessageBox("Nhập lỗi", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
                    }
                }
                else _height = Convert.ToDouble(txtHeight);
            }
            else checkHeght = false;
            if (!txtWeight.Equals(""))
            {
                if (txtWeight.Contains("."))
                {
                    if (txtWeight.IndexOf(".") == txtWeight.LastIndexOf(".") && txtWeight.Length > 2)
                    {
                        txtWeight = txtWeight.Replace(".", ",");
                        _weight = Convert.ToDouble(txtWeight);
                    }
                    else
                    {
                        MessageBox("Nhập lỗi số cân", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
                    }
                }
                else _weight = Convert.ToDouble(txtWeight);
            }
            else checkWeight = false;
            if (!checkHeght && !checkWeight)
            {
                MessageBox("Chưa nhập số liệu", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
            }
            else if (!checkWeight || !checkHeght)
            {
                if (!checkHeght)
                {
                    MessageBox("Chưa nhập chiều cao", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
                }
                else MessageBox("Chưa nhập cân nặng", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
            }
            else { }
            index.height = _height;
            index.weight = _weight;
            return index;
        }

        private void ResultBaby_Click(object sender, RoutedEventArgs e)
        {
            Index index = new Index();
            index = ProcessTextInput(InputHeightBaby.Text, InputWeightBaby.Text);

            var localSetting = ApplicationData.Current.LocalSettings;
            Object value = localSetting.Values["CheckSex"];
            string sex = (string)value;

            BabyCheck baby = new BabyCheck();

            ProcessBabyCheck prbb = new ProcessBabyCheck(MeBe.MainPage._month, sex, index.weight, index.height);
            AdviceForBaby.Text = prbb.SmartCheck();
        }

        private void MapIndex_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }

    public class Index
    {
        public double height { get; set; }
        public double weight { get; set; }
    }
    //cho nay coi bieu do tom tat
    public class MapIndex
    {
        public string TimeIndex { get; set; }
        public string TextIndex { get; set; }
    }
}
