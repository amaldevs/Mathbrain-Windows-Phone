using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using Microsoft.Advertising.Mobile.UI;

namespace Mathbrain
{
    public partial class game : PhoneApplicationPage
    {      
        Random rnd;
        int flag,i;
        int finished,result;
        DateTime initial;
        DispatcherTimer timer;
        double dif,final;
        Grid grid;
        AdControl adcontrol;
        MobFox.Ads.AdControl mobfox;
        int adcount = 0;

        public game()
        {
            rnd = new Random();
            flag = 1;
            InitializeComponent();
            grid = (Grid)this.LayoutRoot.Children[2];
            //         pubcenterAd("test_client", "Image480_80");
            pubcenterAd("9d3674f3-74e3-4d9a-b064-9f46fc443b5a", "142082");   // game educational
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
      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            start();
    }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (timer != null)
                timer.Stop();
        }
        public void showtime(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                flag = 0;
                if (i == 6)
                {
                    question.TextAlignment = System.Windows.TextAlignment.Left;
                    dif = Math.Round(DateTime.Now.Subtract(initial).TotalSeconds, 2);
                    time.Text = dif.ToString();
                    if (finished == 1)
                    {
                        finished = 0;
                        qno.Text = (int.Parse(qno.Text) + 1).ToString();
                        result=generateQuestion(int.Parse(qno.Text));

                    }
                }
                else if(i<4)
                {
                    question.TextAlignment = System.Windows.TextAlignment.Center;
                    question.Text = (4-i).ToString();
                    Thread.Sleep(650);
                    ++i;
                }
                else if (i < 5)
                {
                    question.Text = "START";
                    Thread.Sleep(1000);
                    ++i;
                }
                else if(i==5) 
                {
                    Thread.Sleep(1000);
                    initial = DateTime.Now;
                    ++i;
                }
                flag = 1;
            }
        }

        public void start()
        {
            Thread.Sleep(1000);
            finished = 0;
            answer.Text = "";
            question.Text = "";
            qno.Text = "0";
            wrong.Text = "0";
            finished = 1;
            i = 1;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += showtime;
            timer.Start();
        }

        public void stop()
        {
            final=double.Parse(time.Text);
            if (timer != null)
                timer.Stop();
            NavigationService.Navigate(new Uri("/score.xaml?data="+time.Text+" "+wrong.Text,UriKind.Relative));
        }

        public int generateQuestion(int qn)
        {
            int op,first,second,result;
            op = rnd.Next(1, 5);
            if(op==1) // addition
            {
                if (qn <= 5)
                {
                    first = rnd.Next(0, 11);
                    second = rnd.Next(0, 11);
                }
                else if (qn <= 10)
                {
                    first = rnd.Next(10, 21);
                    second = rnd.Next(0, 11);
                }
                else if (qn <= 15)
                {
                    first = rnd.Next(25, 51);
                    second = rnd.Next(0, 101);
                }
                else if (qn <= 20)
                {
                    first = rnd.Next(50, 151);
                    second = rnd.Next(0, 101);
                }
                else if (qn <= 25)
                {
                    first = rnd.Next(150, 301);
                    second = rnd.Next(0, 201);
                }
                else
                {
                    first = rnd.Next(300, 500);
                    second = rnd.Next(0, 401);
                }
                result = first + second;
                question.Text = first.ToString() + " + " + second.ToString();
            }
            else if (op == 2) // subtraction
            {
                if (qn <= 10)
                {
                    second = rnd.Next(0, 10);
                    first = rnd.Next(second, 11);

                }
                else if (qn <= 15)
                {
                    second = rnd.Next(10, 51);
                    first = rnd.Next(second, 71);
                }
                else if (qn <= 20)
                {
                    second = rnd.Next(20, 51);
                    first = rnd.Next(second, 151);
                }
                else if (qn <= 25)
                {
                    second = rnd.Next(100, 201);
                    first = rnd.Next(second, 301);
                }
                else
                {
                    second = rnd.Next(0, 500);
                    first = rnd.Next(((second>300)?second:300), 501);
                }
                result = first - second;
                question.Text = first.ToString() + " - " + second.ToString();
            }
            else if (op == 3) // multiplication
            {
                if (qn <= 10)
                {
                    first = rnd.Next(0, 6);
                    second = rnd.Next(0, 6);

                }
                else if (qn <= 15)
                {
                    first = rnd.Next(10, 16);
                    second = rnd.Next(0, 16);
                }
                else if (qn <= 20)
                {
                    first = rnd.Next(15, 21);
                    second = rnd.Next(0, 16);
                }
                else if (qn <= 25)
                {
                    first = rnd.Next(20, 26);
                    second = rnd.Next(0, 21);
                }
                else
                {
                    first = rnd.Next(20, 26);
                    second = rnd.Next(15, 21);
                }
                result = first * second;
                question.Text = first.ToString() + " x " + second.ToString();
            }
            else // division
            {
                if (qn <= 10)
                {
                    second = rnd.Next(1, 6);
                    result = rnd.Next(1, 6);

                }
                else if (qn <= 15)
                {
                    second = rnd.Next(1, 11);
                    result = rnd.Next(5, 11);
                }
                else if (qn <= 20)
                {
                    second = rnd.Next(1, 11);
                    result = rnd.Next(10, 16);
                }
                else if (qn <= 25)
                {
                    second = rnd.Next(10, 21);
                    result = rnd.Next(10, 21);
                }
                else
                {
                    second = rnd.Next(15, 31);
                    result = rnd.Next(15, 26);
                }
                first = result * second;
                question.Text = first.ToString() + " / " + second.ToString();
            }
            answer.Text = "";
            return result;
        }

        public void validate()
        {
            int v = int.Parse(answer.Text);
            if (v == result)
            {
                finished = 1;
                if (qno.Text=="30")
                    stop();
            }
            else if (v.ToString().Length >= result.ToString().Length)
            {
                if(i==6)
                    wrong.Text = (int.Parse(wrong.Text) + 1).ToString();
                answer.Text = "";
            }
        }
       
        private void _1_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 1).ToString();
            validate();
        }

        private void _2_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 2).ToString();
            validate();
        }

        private void _3_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 3).ToString();
            validate();
        }

        private void _4_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 4).ToString();
            validate();
        }

        private void _5_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 5).ToString();
            validate();
        }

        private void _6_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var) * 10 + 6).ToString();
            validate();
        }

        private void _7_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var) * 10 + 7).ToString();
            validate();
        }

        private void _8_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var) * 10 + 8).ToString();
            validate();
        }

        private void _9_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var) * 10 + 9).ToString();
            validate();
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            answer.Text = "";
        }

        private void _0_Click(object sender, RoutedEventArgs e)
        {
            string var = answer.Text;
            if (var == "")
                var = "0";
            answer.Text = (Int32.Parse(var)*10 + 0).ToString();
            validate();
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            string s = answer.Text;
            if (s.Length > 0)
            {
                int l = s.Length;
                answer.Text=s.Remove(l - 1);
            }
        }

        private void ApplicationBar_Restart(object sender, EventArgs e)
        {
            time.Text = "0";
            if(timer!=null)
                timer.Stop();
            start();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationService.Navigate(new Uri("/mainpage.xaml", UriKind.Relative));
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