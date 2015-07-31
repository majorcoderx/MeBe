using MeBe.Models;
using MeBe.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeBe.Views
{

    public sealed partial class Hospital : Page
    {

        MapIcon icon = new MapIcon();
        ObservableCollection<HospitalAddress> listAdd = new ObservableCollection<HospitalAddress>();
        PAddressHospital pAdd = new PAddressHospital();

        public Hospital()
        {
            this.InitializeComponent();

            HospitalChoose.IsChecked = true;
            VaccinationChoose.IsChecked = false;
            ShowCenterLocation();
            listAdd = pAdd.GetListObject(1);

            TimeSpan timeSpan = new TimeSpan(0, 0, 8);
            var handler = new TimerElapsedHandler(UpdateTimer);
            ThreadPoolTimer threadPool = ThreadPoolTimer.CreatePeriodicTimer(handler, timeSpan);
        }

        private async void UpdateTimer(ThreadPoolTimer timer)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ShowIconLocation();   
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            //TimeSpan delay = TimeSpan.FromSeconds(2);
            //ThreadPoolTimer delayTimer = ThreadPoolTimer.CreateTimer((source) =>
            //{
            //    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //    {
            //        ShowCenterLocation();
            //        listAdd = pAdd.GetListObject(1);
            //    });

            //}, delay);
        }

        //view your location

        //get geopostion
        public async Task<Geoposition> GetGeoPosition()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            Geoposition position = await geolocator.GetGeopositionAsync();
            return position;
        }

        //View center location
        public async void ShowCenterLocation()
        {
            ShowIconLocation();
            Geoposition position = await GetGeoPosition();
            xMe = position.Coordinate.Point.Position.Latitude;
            yMe = position.Coordinate.Point.Position.Longitude;
            await MyMap.TrySetViewAsync(position.Coordinate.Point, 18D);
        }

        private double xMe = 0;
        private double yMe = 0;

        //draw icon your location
        public async void ShowIconLocation()
        {
            icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icons/location.png"));

            Geoposition point = await GetGeoPosition();
            icon.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = point.Coordinate.Point.Position.Latitude,
                Longitude = point.Coordinate.Point.Position.Longitude
            });
            xMe = point.Coordinate.Point.Position.Latitude;
            yMe = point.Coordinate.Point.Position.Longitude;
            icon.NormalizedAnchorPoint = new Point(0.5, 0.5);
            icon.Title = "Tôi";
            MyMap.MapElements.Add(icon);
        }

        MapIcon iconHospital = new MapIcon();

        public void ShowIconHospital(double x, double y, string name)
        {
            iconHospital.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icons/hospital.png"));
            iconHospital.Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = x,
                Longitude =y
            });
            iconHospital.NormalizedAnchorPoint = new Point(0.5, 0.5);
            iconHospital.Title = name;
            MyMap.MapElements.Add(iconHospital);
        }
       

        private async Task<MapRoute> GetMapRoute(double x,double y)
        {
            Geoposition position = await GetGeoPosition();
            BasicGeoposition startLocation = new BasicGeoposition()
            {
                Latitude = position.Coordinate.Point.Position.Latitude,
                Longitude = position.Coordinate.Point.Position.Longitude
            };
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = new BasicGeoposition()
            {
                Latitude = x,
                Longitude = y
            };
            Geopoint endPoint = new Geopoint(endLocation);

            var routedResult = await MapRouteFinder.GetDrivingRouteAsync(startPoint, endPoint);

            if (routedResult.Status == MapRouteFinderStatus.Success)
            {
                DistanceText.Text = "Khoảng cách: " + routedResult.Route.LengthInMeters / 1000 + "km";
                TimeDriveText.Text = "Thời gian: " + routedResult.Route.EstimatedDuration.Minutes + " phút";
                return routedResult.Route;
            }
            else
            {
                return await GetMapRoute(x, y);
            }
        }

        private async void DrawLineDirection(double x,double y)
        {
            MapRoute routed = await GetMapRoute(x,y);
            var color = Windows.UI.Colors.Green;
            color.A = 128;
            var line = new MapPolyline
            {
                StrokeThickness = 6,
                StrokeColor = color,
                StrokeDashed = false,
                ZIndex = 2
            };

            line.Path = new Geopath(routed.Path.Positions);
            MyMap.MapElements.Add(line);
        }
        
        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        //3 event botton
        private void MyLocation_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShowCenterLocation();
            MyMap.MapElements.Clear();
            DistanceText.Text = "";
            TimeDriveText.Text = "";
        }

        private void NextHospital_Tapped(object sender, TappedRoutedEventArgs e)
        {
            foreach (var item in listDis)
            {
                if (item.ID == disMin.ID && listDis.Count > 1)
                {
                    listDis.Remove(item);
                    disMin = new Distance();
                    break;
                }
            }
            if (listDis.Count != 0)
            {
                disMin = listDis[0];
                foreach (var item in listDis)
                {
                    if (disMin.distanceHospital < item.distanceHospital)
                    {
                        disMin = item;
                    }
                }
                ShowDrirectLine();
            }
            else
            {
                DistanceText.Text = "Lỗi, mời bạn chọn tìm kiếm điểm gần nhất";
            }
        }

        List<Distance> listDis = new List<Distance>();
        Distance disMin = new Distance();

        private void FindHospital_Tapped(object sender, TappedRoutedEventArgs e)
        {
            listDis.Clear();
            double disHos = 0;
            bool check = true;
            for (int i = 0; i < listAdd.Count; ++i)
            {
                int j = 0;
                double x = 0;
                double y = 0;
                string addText = listAdd[i].XY;
                Regex theReg = new Regex(@"(\S+)");
                MatchCollection theMatches = theReg.Matches(addText);
                foreach (Match item in theMatches)
                {
                    if (j == 0)
                    {
                        x = Convert.ToDouble(item.ToString().Replace(".", ","));
                        ++j;
                    }
                    if (j == 1)
                    {
                        y = Convert.ToDouble(item.ToString().Replace(".", ","));
                    }
                }
                if (check == true)
                {
                    disHos = (x - xMe) * (x - xMe) + (y - yMe) * (y - yMe);
                    disMin.x = x;
                    disMin.y = y;
                    disMin.ID = listAdd[i].ID;
                    disMin.Name = listAdd[i].Name;
                }
                else
                {
                    if ((x - xMe) * (x - xMe) + (y - yMe) * (y - yMe) < disHos)
                    {
                        disHos = (x - xMe) * (x - xMe) + (y - yMe) * (y - yMe);
                        disMin.x = x;
                        disMin.y = y;
                        disMin.ID = listAdd[i].ID;
                        disMin.Name = listAdd[i].Name;
                    }
                }
                Distance dis = new Distance(x,y,listAdd[i].ID,listAdd[i].Name,disHos);
                listDis.Add(dis);
            }
            ShowDrirectLine();
        }

        private void ShowDrirectLine()
        {
            MyMap.MapElements.Clear();
            ShowIconLocation();
            DrawLineDirection(disMin.x, disMin.y);
            ShowIconHospital(disMin.x, disMin.y, disMin.Name);
            ShowHospitalLocation(disMin.x,disMin.y);

            MainPage.dispatcherTime.Tick+=dispatcher_Tick;
            MainPage.dispatcherTime.Interval = new TimeSpan(0, 0, 5);
            MainPage.dispatcherTime.Start();
        }

        private void dispatcher_Tick(object sender, object e)
        {
            ShowCenterLocation();
            MainPage.dispatcherTime.Stop();
        }

        private async void ShowHospitalLocation(double x,double y)
        {
            BasicGeoposition hosLocation = new BasicGeoposition()
            {
                Latitude = x,
                Longitude = y
            };
            Geopoint point = new Geopoint(hosLocation);
            await MyMap.TrySetViewAsync(point, 16D);
        }

        private void ChooseHospital_Checked(object sender, RoutedEventArgs e)
        {
            HospitalChoose.IsChecked = true;
            VaccinationChoose.IsChecked = false;
            listAdd = pAdd.GetListObject(1);
            MyMap.MapElements.Clear();  
        }

        private void ChooseVaccination_Checked(object sender, RoutedEventArgs e)
        {
            HospitalChoose.IsChecked = false;
            VaccinationChoose.IsChecked = true;
            listAdd = pAdd.GetListObject(0);
            MyMap.MapElements.Clear();
        }
    }

    public class Distance
    {
        public Distance() { }
        public Distance(double x,double y, int id,string name,double ditance)
        {
            this.x = x;
            this.y = y;
            this.ID = id;
            this.Name = name;
        }

        public double x;
        public double y;
        public int ID;
        public string Name;
        public double distanceHospital;
    }
}
