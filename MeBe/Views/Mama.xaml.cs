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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeBe.Views
{
    public sealed partial class Mama : Page
    {
        public Mama()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeViewMama.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
            MainPage.dispatcherTime.Tick += dispatcherTime_Tick;
            MainPage.dispatcherTime.Interval = new TimeSpan(0, 0, 30);
            MainPage.dispatcherTime.Start();
        }

        void dispatcherTime_Tick(object sender, object e)
        {
            TimeViewMama.Text = DateTime.Now.ToString("dd/MM/yyyy        hh:mm tt");
        }

        private void CanelClick(IUICommand command)
        {
            this.Frame.GoBack();
        }

        private void OkClick(IUICommand command)
        {
            InputHeightMama.Text = "";
            InputOldMama.Text = "";
            InputWeightMama.Text = "";
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

        private void ResultMama_Click(object sender, RoutedEventArgs e)
        {
            Index index = new Index();
            index = ProcessTextInput(InputHeightMama.Text, InputWeightMama.Text);

            string _old = InputOldMama.Text;
            int old = 0;
            if (index.height != 0 && index.weight != 0)
            {
                if (_old.Equals(""))
                {
                    MessageBox("Chưa nhập số tuổi", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
                }
                else
                {
                    if (!_old.Contains("."))
                    {
                        old = Convert.ToInt32(_old);
                    }
                    else
                    {
                        MessageBox("Nhập lỗi ", "Bạn nhập lại không?\r\nOk tiếp tục,Canel thoát");
                    }
                }
            }

            var localSetting = ApplicationData.Current.LocalSettings;
            string value = localSetting.Values["CheckBorn"].ToString();
            ProcessMamaCheck prMama = new ProcessMamaCheck(MeBe.MainPage._week, old, index.weight, index.height);
            if (value.Equals("false"))
            {
                AdviceForMama.Text = prMama.AdviceForMama();
            }
            else
            {
                AdviceForMama.Text = prMama.AdviceMamaBeautiful();
            }
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void MapIndex_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
