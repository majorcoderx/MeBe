using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
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
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Note));
        }

        private void BackHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Knowledge_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Knowledge));
        }

        private void HealthMama_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Mama));
        }

        private void HealthBaby_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            Object value = localSetting.Values["CheckBorn"];
            string _checkBorn = (string)value;
            if (_checkBorn.Equals("false"))
            {
                this.Frame.Navigate(typeof(Foetus));
            }
            else
            {
                this.Frame.Navigate(typeof(Baby));
            }
        }

        private void TestToMama_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Testing));
        }

        private void Hospital_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Hospital));
        }

        private void Nutritious_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Nutrition));
        }
    }
}
