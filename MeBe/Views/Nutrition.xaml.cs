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
    public sealed partial class Nutrition : Page
    {

        ObservableCollection<Food> listFood = new ObservableCollection<Food>();
        List<Food> listVegetable = new List<Food>(); //show 5 object
        List<Food> listShowVegetable;

        public Nutrition()
        {
            this.InitializeComponent();
            listFood = pNutrition.GetListObject();
            foreach (var item in listFood)
            {
                if (item.Type == 1)
                {
                    listVegetable.Add(item);
                    //System.Diagnostics.Debug.WriteLine(item.Name);
                }
            }
        }

        PNutrition pNutrition = new PNutrition();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            listShowVegetable = new List<Food>();
            listShowVegetable = GetListShow(1, listVegetable);

        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void VegetableLayout_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var velocity = e.Velocities;
            System.Diagnostics.Debug.WriteLine(velocity.Linear.Y - velocity.Linear.X);
        }

        private void Image1_Tapped(object sender, TappedRoutedEventArgs e)
        {

            foreach (var item in listShowVegetable)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);
            }
            InformationFood.DataContext = listShowVegetable[0];
            InformationFood.IsOpen = true;
        }
        //get 5 object in list
        //mark index show now
        private int indexType1 = 0;
        private int indexType2 = 0;
        private int indexType3 = 0;
        private int indexType4 = 0;

        public List<Food> GetListShow(int type,List<Food> lists)
        {
            List<Food> list = new List<Food>();
            int index = 0;
            switch (type)
            {
                case 1:
                    index = indexType1;
                    break;
                case 2:
                    index = indexType2;
                    break;
                case 3:
                    index = indexType3;
                    break;
                case 4:
                    index = indexType4;
                    break;
            }
            int count = 0;
            for (; index < lists.Count; ++index)
            {
                list.Add(lists[index]);
                ++count;
                if (count < 5 && index == lists.Count-1 )
                {
                    index = 0;
                }
                if (count == 5) break;
            }
            return list;
        }

        private void ChooseVegetable_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ViewFood
    {
        public ViewFood(string name, string url, string infor)
        {
            this.UrlImage = url;
            this.Name = name;
            this.Information = infor;
        }

        public string UrlImage { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }
}
