using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Advertising.Mobile.UI;
using Microsoft.Phone.Tasks;

namespace Mathbrain
{
    public partial class MainPage : PhoneApplicationPage
    {
        AdControl adcontrol;
        MobFox.Ads.AdControl mobfox;
        Grid grid;
        int adcount = 0;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            grid = (Grid)this.LayoutRoot.Children[2];
        }

        void pubcenterAd(string apid, string adunitid)
        {
            try
            {
                adcontrol = new AdControl(apid, adunitid, true);
                adcontrol.Height = 80;
                adcontrol.Width = 480;
                adcontrol.Name = "pubcenterAd";
                adcontrol.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                grid.Children.Add(adcontrol);
                adcontrol.ErrorOccurred += new EventHandler<Microsoft.Advertising.AdErrorEventArgs>(sAdControl_ErrorOccurred);
            }
            catch (Exception e)
            {
                mobfoxAd("3246e432e6f0ff40964d3410edc52a27");
            }
        }

        void sAdControl_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            ++adcount;
            if (adcount==2)
            {
                grid.Children.Remove(adcontrol);
                mobfoxAd("3246e432e6f0ff40964d3410edc52a27");
            }
        }

        void mobfoxAd(string publisherid)
        {
            try
            {
                mobfox = new MobFox.Ads.AdControl { PublisherID = publisherid, TestMode = false };
                mobfox.Name = "mobfoxAd";
                grid.Children.Add(mobfox);
            }
            catch (Exception e)
            { }
        }
      
        bool file(string filename,string write)
        {
            bool status;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isf.FileExists(filename))
                {
                    using (IsolatedStorageFileStream file = isf.CreateFile(filename))
                    {
                        StreamWriter writer = new StreamWriter(file);
                        writer.WriteLine(write);
                        writer.Close();
                    }
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            var askforReview = (bool)IsolatedStorageSettings.ApplicationSettings["askforreview"];
            if (askforReview)
            {
                IsolatedStorageSettings.ApplicationSettings["askforreview"] = false;
                var returnvalue = MessageBox.Show("Thank you for using Mathbrain", "Please rate our mathbrain", MessageBoxButton.OKCancel);
                if (returnvalue == MessageBoxResult.OK)
                {
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }
            }
            NavigationService.Navigate(new Uri("/game.xaml", UriKind.Relative));
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
 	        base.OnNavigatedTo(e);

      //      pubcenterAd("test_client", "Image480_80");
            pubcenterAd("9d3674f3-74e3-4d9a-b064-9f46fc443b5a", "142084");
            while(NavigationService.BackStack.Count()!=0)
                NavigationService.RemoveBackEntry();
            if(file("fifth", "Job"))
                if(file("fifthscore", "197.27"))
                    if(file("fourth", "Vikie"))
                        if(file("fourthscore", "105.50"))
                            if(file("third", "Luis"))
                                if(file("thirdscore", "88.73"))
                                    if(file("second", "Li"))
                                        if(file("secondscore", "74.41"))
                                            if(file("first", "Kidu"))
                                                if (file("firstscore", "60.15"))
                                                { }
        }

        private void how_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage.xaml?goto=1", UriKind.Relative));
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage.xaml?goto=2", UriKind.Relative));
        }
    }
}