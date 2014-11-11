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

namespace Mathbrain
{
    public partial class score : PhoneApplicationPage
    {
        bool savestatus;
        public score()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel=true;

            NavigationService.Navigate(new Uri("/mainpage.xaml",UriKind.Relative));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationService.RemoveBackEntry();
            string d;   
            string s;
            Random rnd=new Random();
            int c;
            if (NavigationContext.QueryString.TryGetValue("data", out d))
            {
                time.Text = d.Split(' ')[0];
                wrong.Text = d.Split(' ')[1];
                total.Text = (double.Parse(time.Text) + double.Parse(wrong.Text)).ToString();
                nametextblock.Visibility=System.Windows.Visibility.Collapsed;
                name.Visibility=System.Windows.Visibility.Collapsed;
                colon.Visibility = System.Windows.Visibility.Collapsed;
                savestatus = false;
                save.Content = "Highscores";
            }
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists("prevname"))
                {
                    using (IsolatedStorageFileStream file = isf.OpenFile("prevname", System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(file);
                        name.Text = reader.ReadLine();
                        reader.Close();
                    }
                }
                double t=double.Parse(total.Text);
                int status=0;
                if (isf.FileExists("fifthscore"))
                {
                    using (IsolatedStorageFileStream file = isf.OpenFile("fifthscore", System.IO.FileMode.Open))
                    {
                        string fifth;
                        StreamReader reader = new StreamReader(file);
                        fifth = reader.ReadLine();
                        reader.Close();
                        if (fifth == "" || (double.Parse(total.Text) < double.Parse(fifth)))
                        {
                            c = rnd.Next(0, 5);
                            switch (c)
                            {
                                case 0: s = "Well done!!!";
                                break;
                                case 1: s = "Way to go :)";
                                break;
                                case 2: s = "Nice job!!!";
                                break;
                                case 3: s = "Good to see you in the list";
                                break;
                                default:
                                s = "Keep it up!!!";
                                break;
                            }
                            status = 1;
                            using (IsolatedStorageFileStream file1 = isf.OpenFile("firstscore", System.IO.FileMode.Open))
                            {
                                string first;
                                StreamReader reader1 = new StreamReader(file1);
                                first = reader1.ReadLine();
                                if (first == "" || (double.Parse(total.Text) < double.Parse(first)))
                                {
                                    s = "New Highscore";
                                    status = 2;
                                }
                            }
                            reader.Close();
                            nametextblock.Visibility = System.Windows.Visibility.Visible;
                            name.Visibility = System.Windows.Visibility.Visible;
                            colon.Visibility = System.Windows.Visibility.Visible;
                            save.Content = "Save Score";
                            savestatus = true;
                        }
                        else
                        {
                            c = rnd.Next(0, 4);
                            switch (c)
                            {
                                case 0: s = "Good job, not your best :)";
                                    break;
                                case 1: s = "You can do better :)";
                                    break;
                                case 2: s = "Nice one, but not enough!";
                                    break;
                                default: s = "See you soon in the list";
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    s = "Well done!!!";
                }
                if (t < 61)
                {
                    if (status > 0)
                    {
                        c = rnd.Next(0, 5);
                        switch (c)
                        {
                            case 0: s = "Mr. Genius :)";
                                break;
                            case 1: s = "Master!!!";
                                break;
                            case 2: s = "You are genius";
                                break;
                            case 3: s = "Einstein reloaded :)";
                                break;
                            default:
                                s = "Mathemagician!";
                                break;
                        }
                    }
                    else
                    {
                        s = "Talent!!!";
                    }
                }
                else if (t < 95)
                {
                    if (status > 0)
                    {
                        c = rnd.Next(0, 5);
                        switch (c)
                        {
                            case 0: s = "Wow!!! :)";
                                break;
                            case 1: s = "Skill!!!";
                                break;
                            case 2: s = "Mathbrain!!!";
                                break;
                            case 3: s = "Amazing!!!";
                                break;
                            default:
                                s = "Bravo!!!";
                                break;
                        }
                    }
                    else
                    {
                        s = "Good job, but seen better!";
                    }
                }
            }
                                        
            comment.Text=s;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (savestatus)
                {

                    using (IsolatedStorageFileStream file = isf.CreateFile("prevname"))
                    {
                        StreamWriter writer = new StreamWriter(file);
                        writer.WriteLine(name.Text);
                        writer.Close();
                    }
                    using (IsolatedStorageFileStream file = isf.CreateFile("newscore"))
                    {
                        StreamWriter writer = new StreamWriter(file);
                        writer.WriteLine(total.Text);
                        writer.Close();
                    }
                }
                else
                {
                    using (IsolatedStorageFileStream file = isf.CreateFile("newscore"))
                    {
                        StreamWriter writer = new StreamWriter(file);
                        writer.WriteLine("");
                        writer.Close();
                    }
                }
            }
            NavigationService.Navigate(new Uri("/highscores.xaml", UriKind.Relative));
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