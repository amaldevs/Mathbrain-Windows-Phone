using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Advertising.Mobile.UI;

namespace Mathbrain
{
    public partial class Highscores : PhoneApplicationPage
    {
        AdControl adcontrol;
        MobFox.Ads.AdControl mobfox;
        Grid grid;
        string n1, n2, n3, n4, n5, n, s1, s2, s3,s4,s5, s;
        int adcount = 0;
        public Highscores()
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
            if (adcount == 2)
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

        string rfile(string filename)
        {
            string read;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(filename))
                {
                    using (IsolatedStorageFileStream file = isf.OpenFile(filename, System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(file);
                        read = reader.ReadLine();
                        reader.Close();
                        return read;
                    }
                }
            }
            return "";
        }
        void wfile(string filename,string write)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream file = isf.CreateFile(filename))
                {
                    StreamWriter writer = new StreamWriter(file);
                    writer.WriteLine(write);
                    writer.Close();
                }
            }
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            n1 = rfile("first");
            s1 = rfile("firstscore");
            n2 = rfile("second");
            s2 = rfile("secondscore");
            n3 = rfile("third");
            s3 = rfile("thirdscore");
            n4 = rfile("fourth");
            s4 = rfile("fourthscore");
            n5 = rfile("fifth");
            s5 = rfile("fifthscore");
            s=rfile("newscore");
            if (s != "")
            {
                using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isf.FileExists("prevname"))
                    {
                        using (IsolatedStorageFileStream file = isf.OpenFile("prevname", System.IO.FileMode.Open))
                        {
                            StreamReader reader = new StreamReader(file);
                            n = reader.ReadLine();
                            reader.Close();
                        }
                    }
                }

                if (s1 == "" || (double.Parse(s) < double.Parse(s1)))
                {
                    s5 = s4;
                    n5 = n4;
                    s4 = s3;
                    n4 = n3;
                    s3 = s2;
                    n3 = n2;
                    s2 = s1;
                    n2 = n1;
                    s1 = s;
                    n1 = n;
                }
                else if (s2 == "" || (double.Parse(s) < double.Parse(s2)))
                {
                    s5 = s4;
                    n5 = n4;
                    s4 = s3;
                    n4 = n3;
                    s3 = s2;
                    n3 = n2;
                    s2 = s;
                    n2 = n;
                }
                else if (s3 == "" || (double.Parse(s) < double.Parse(s3)))
                {
                    s5 = s4;
                    n5 = n4;
                    s4 = s3;
                    n4 = n3;
                    s3 = s;
                    n3 = n;
                }
                else if (s4 == "" || (double.Parse(s) < double.Parse(s4)))
                {
                    s5 = s4;
                    n5 = n4;
                    s4 = s;
                    n4 = n;
                }
                else if (s5 == "" || (double.Parse(s) < double.Parse(s5)))
                {
                    s5 = s;
                    n5 = n;
                }
                wfile("first", n1);
                wfile("second", n2);
                wfile("third", n3);
                wfile("fourth", n4);
                wfile("fifth", n5);
                wfile("firstscore", s1);
                wfile("secondscore", s2);
                wfile("thirdscore", s3);
                wfile("fourthscore", s4);
                wfile("fifthscore", s5);
                wfile("newscore", "");
            }
            first.Text = n1;
            firstscore.Text = rfile("firstscore");
            second.Text = n2;
            secondscore.Text = rfile("secondscore");
            third.Text = n3;
            thirdscore.Text = rfile("thirdscore");
            fourth.Text = n4;
            fourthscore.Text = rfile("fourthscore");
            fifth.Text = n5;
            fifthscore.Text = rfile("fifthscore");
            base.OnNavigatedTo(e);
            pubcenterAd("9d3674f3-74e3-4d9a-b064-9f46fc443b5a", "142083");
  //          pubcenterAd("test_client", "Image480_80");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationService.Navigate(new Uri("/mainpage.xaml", UriKind.Relative));
        }

        private void ApplicationBar_Play(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/game.xaml", UriKind.Relative));
        }
        private void help_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage.xaml?goto=1", UriKind.Relative));
        }

        private void credits_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PanoramaPage.xaml?goto=2", UriKind.Relative));
        }

    }
}
