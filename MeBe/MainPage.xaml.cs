using MeBe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MeBe
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.Loaded += HiddenStatusBar;

            RootLayout.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateRailsY;
            RootLayout.ManipulationCompleted += GridLayout_ManipulationCompleted;

            LoadPage();

        }   

        private void GridLayout_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var velocity = e.Velocities;
            if (velocity.Linear.Y - velocity.Linear.X > 0.4)
            {
                this.Frame.Navigate(typeof(Views.Menu));
            }
        }

        public static DispatcherTimer dispatcherTime = new DispatcherTimer();

        private void LoadPage()
        {
            HaveWeek.Text = "0";
            bool checkBox = GetCheckBoxSetting();
            if (checkBox)
            {
                CheckBoxImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/check.png", UriKind.Absolute));
                TextStart.Text = "Nhập ngày sinh bé";
                TextHaveTime.Text = "Bé đã được";
            }
            else
            {
                CheckBoxImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/uncheck.png", UriKind.Absolute));
                TextStart.Text = "Nhập kỳ kinh cuối";
                TextHaveTime.Text = "Thai nhi được";
            }
        }

        private async void HiddenStatusBar(object sender, RoutedEventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
        }
        

        private void MenuIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.Menu));
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            object value = localSetting.Values["TimeApp"];
            string timeapp;
            if (value == null)
            {
                localSetting.Values["TimeApp"] = "01/06/2014";
                timeapp = "01/06/2014";
            }
            else
            {
                timeapp = (string)value;
            }
            DatePicker picker = sender as DatePicker;
            DateTimeOffset newDate = DateTimeOffset.Now;
            DateTimeOffset.TryParse(timeapp, out newDate);
            if (picker != null)
            {
                picker.Date = newDate;
            }
        }

        private void CheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            bool checkBox = GetCheckBoxSetting();
            if (checkBox == false)
            {
                SetCheckBox("true");
                CheckBoxImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/check.png", UriKind.Absolute));
                TextStart.Text = "Nhập ngày sinh bé";
                TextHaveTime.Text = "Bé đã được";
            }
            else
            {
                SetCheckBox("false");
                CheckBoxImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/uncheck.png", UriKind.Absolute));
                TextStart.Text = "Nhập kỳ kinh cuối";
                TextHaveTime.Text = "Thai nhi được";
            }
        }

        private bool GetCheckBoxSetting()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            Object _checkbox = localSettings.Values["CheckBorn"];
            if (_checkbox == null)
            {
                localSettings.Values["CheckBorn"] = "false";
                return false;
            }
            else
            {
                if (_checkbox.ToString().Equals("true"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void SetCheckBox(string p)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["CheckBorn"] = p;
        }

        public static int _week = 0;
        public static int _month = 0;

        private void DateChanged_Apps(object sender, DatePickerValueChangedEventArgs e)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            Object value = localSetting.Values["CheckBorn"];
            string born = (string)value;

            if (DateTime.Now.Year < DatePickerApp.Date.Year)
            {
                TextNotification.Text = "Bạn nhập thời gian trong tương lai, vui lòng nhập lại.";
                HaveWeek.Text = "0";
            }
            else if (DateTime.Now.Year == DatePickerApp.Date.Year && DateTime.Now.Month < DatePickerApp.Date.Month)
            {
                TextNotification.Text = "Bạn nhập thời gian trong tương lai, vui lòng nhập lại.";
                HaveWeek.Text = "0";
            }
            else if (DateTime.Now.Year == DatePickerApp.Date.Year && DateTime.Now.Month == DatePickerApp.Date.Month
                && DateTime.Now.Day < DatePickerApp.Date.Day)
            {
                TextNotification.Text = "Bạn nhập thời gian trong tương lai, vui lòng nhập lại.";
                HaveWeek.Text = "0";
            }
            else if ((DateTime.Now.Year - DatePickerApp.Date.Year) >= 1)
            {
                TextNotification.Text = "Có vẻ không đúng, vui lòng nhập lại.";
                HaveWeek.Text = "0";
            }
            else
            {
                CountTime c_Time = new CountTime(DatePickerApp.Date);
                _week = c_Time.CountWeek();
                HaveWeek.Text = _week.ToString();
                _month = c_Time.HaveMonth();
                localSetting.Values["TimeApp"] = DatePickerApp.Date.ToString("dd/MM/yyyy");
            }
        }

        
    }
}
