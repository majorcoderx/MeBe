using MeBe.Models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeBe.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewKnowledge : Page
    {
        public ViewKnowledge()
        {
            this.InitializeComponent();
            TextContentKnowledge.ManipulationMode = ManipulationModes.TranslateX;
            TextContentKnowledge.ManipulationCompleted += LayoutViewKnowLedge_ManipulationCompleted;
        }

        void LayoutViewKnowLedge_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var velocity = e.Velocities;
            if (velocity.Linear.X < -0.4)
            {
                NextView();
            }
            if (velocity.Linear.X > 0.4)
            {
                BackView();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextContentKnowledge.Text = Knowledge.knowledge.Content;
            TitleContentKnowledge.Text = Knowledge.knowledge.Title;
            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if ( Knowledge.knowledge.ID == Knowledge.know[i].ID)
                {
                    CountPageKnowledge.Text = (i + 1).ToString() + "/" + Knowledge.know.Count.ToString();
                }
            }
            if (Knowledge.knowledge.Like == 0)
            {
                LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/unlike.png",UriKind.Absolute));
            }
            else
            {
                LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pink-heart.png", UriKind.Absolute));
            }
        }

        private void NotesIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Note));
        }

        private void NextView()
        {
            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if (Knowledge.knowledge.ID == Knowledge.know[i].ID)
                {
                    if (i < Knowledge.know.Count - 1)
                    {
                        Knowledge.knowledge = Knowledge.know[i + 1];
                        CountPageKnowledge.Text = (i + 2).ToString() + "/" + Knowledge.know.Count.ToString();
                        TextContentKnowledge.Text = Knowledge.knowledge.Content;
                        TitleContentKnowledge.Text = Knowledge.knowledge.Title;
                        if (Knowledge.knowledge.Like == 0)
                        {
                            LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/unlike.png", UriKind.Absolute));
                        }
                        else
                        {
                            LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pink-heart.png", UriKind.Absolute));
                        }
                    }
                    break;
                }
            }
        }

        private void NextKnowLedgeView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NextView();
        }

        private void BackView()
        {
            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if (Knowledge.knowledge.ID == Knowledge.know[i].ID)
                {
                    if (i > 0)
                    {
                        Knowledge.knowledge = Knowledge.know[i - 1];
                        CountPageKnowledge.Text = i.ToString() + "/" + Knowledge.know.Count.ToString();
                        TextContentKnowledge.Text = Knowledge.knowledge.Content;
                        TitleContentKnowledge.Text = Knowledge.knowledge.Title;
                        if (Knowledge.knowledge.Like == 0)
                        {
                            LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/unlike.png", UriKind.Absolute));
                        }
                        else
                        {
                            LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pink-heart.png", UriKind.Absolute));
                        }
                    }
                    break;
                }
            }
        }

        private void BackKnowledgeView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BackView();
        }

        private void InsertLikeView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Knowledge.knowledge.Like == 0)
            {
                Knowledge.knowledge.Like = 1;
                UpdateList(1);
                Knowledge.pknow.UpdateObject(Knowledge.knowledge);
                LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/pink-heart.png", UriKind.Absolute));
            }
            else
            {
                Knowledge.knowledge.Like = 0;
                UpdateList(0);
                Knowledge.pknow.UpdateObject(Knowledge.knowledge);
                LikeKnowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Icons/unlike.png", UriKind.Absolute));
            }
        }

        private void UpdateList(int index)
        {
            for (int i = 0; i < Knowledge.know.Count; ++i)
            {
                if (Knowledge.knowledge.ID == Knowledge.know[i].ID)
                {
                    Knowledge.know[i].Like = index;
                }
            }
        }
    }
}
