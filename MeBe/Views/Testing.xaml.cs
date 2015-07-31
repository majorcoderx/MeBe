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
    public sealed partial class Testing : Page
    {
        TestMama testMama = new TestMama();
        ObservableCollection<TestMama> listTestMama = new ObservableCollection<TestMama>();
        PTestForMama pTest = new PTestForMama();

        public Testing()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadDataTest();
        }

        public void LoadDataTest()
        {
            var localSetting = ApplicationData.Current.LocalSettings;
            string _check = (string)localSetting.Values["CheckBorn"];
            if (_check.Equals("false"))
            {
                listTestMama = pTest.GetListObject(0, MeBe.MainPage._month);
                TestListView.ItemsSource = listTestMama;
            }
            else
            {
                listTestMama = pTest.GetListObject(1, MeBe.MainPage._month);
                TestListView.ItemsSource = listTestMama;
            }
            IndexTestResult.Text = "0";
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void ResultTestMama_Click(object sender, RoutedEventArgs e)
        {
            GetIndexTest();
        }

        private void GetIndexTest()
        {
            int diem = 0;
            foreach (TestMama item in listTestMama)
            {
                if (item.Result == item.Answers)
                {
                    diem += 1;
                }
            }
            IndexTestResult.Text = diem.ToString();
        }

        private void TestListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            testMama = e.ClickedItem as TestMama;
        }

        private void CheckTest_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (testMama.Answers == 1)
            {
                testMama.Answers = 0;
                testMama.UriImage = "/Assets/Icons/uncheck.png";
                for (int i = 0; i < listTestMama.Count; ++i)
                {
                    if (testMama.ID == listTestMama[i].ID)
                    {
                        listTestMama[i] = testMama;
                    }
                }
            }
            else
            {
                testMama.Answers = 1;
                testMama.UriImage = "/Assets/Icons/check.png";
                for (int i = 0; i < listTestMama.Count; ++i)
                {
                    if (testMama.ID == listTestMama[i].ID)
                    {
                        listTestMama[i] = testMama;
                    }
                }
            }
            pTest.UpdateObject(testMama);
        }
    }
}
