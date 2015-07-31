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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Foetus : Page
    {
        public Foetus()
        {
            this.InitializeComponent();
        }

        private DateTime dateCount;
        private int CountClick = 0;
        private bool CheckClickCountTapped = false;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeViewFoetus.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
            MainPage.dispatcherTime.Tick += dispatcherTime_Tick;
            MainPage.dispatcherTime.Interval = new TimeSpan(0, 0, 30);
            MainPage.dispatcherTime.Start();
        }

        void dispatcherTime_Tick(object sender, object e)
        {
            TimeViewFoetus.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
            string timeText = "02:00";
            if (CheckClickCountTapped)
            {
                var c_time = dateCount + TimeSpan.FromHours(2);
                var _time = c_time - DateTime.Now;
                if (_time.Hours >= 0 && _time.Minutes >= 0)
                {
                    timeText = _time.Hours.ToString("00") + ":" + _time.Minutes.ToString("00");
                }
                else CountClick = 0;
            }
            TimeTap.Text = timeText;
        }

        private void CountClick_Click(object sender, RoutedEventArgs e)
        {
            if (CountClick == 0)
            {
                dateCount = DateTime.Now;
                CheckClickCountTapped = true;
                CountClick = 1;
                CountWasTap.Text = CountClick.ToString("00");
                InsertHistoryFoutesTapped();
            }
            else
            {
                CountClick += 1;
                CountWasTap.Text = CountClick.ToString("00");
                InsertHistoryFoutesTapped();
            }
        }

        private void ResetCountTime_Click(object sender, RoutedEventArgs e)
        {
            CheckClickCountTapped = false;
            CountClick = 0;
            CountWasTap.Text = "00";
            TimeTap.Text = "02:00";
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void InsertHistoryFoutesTapped()
        {
            //chen vao database xem lich su
            //dua vao du lieu : so gio con lai
            //dua vao cung 1 ID
        }
    }
}
