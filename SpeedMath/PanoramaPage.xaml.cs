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
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;
namespace Mathbrain
{
    public partial class PanoramaPage : PhoneApplicationPage
    {
        public PanoramaPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            PanoramaControl.DefaultItem = itemhow;
            string item;
            if(NavigationContext.QueryString.TryGetValue("goto",out item))
            {
                if (item == "2")
                    PanoramaControl.DefaultItem = itemabout;
            }
            base.OnNavigatedTo(e);
        }
        private void ApplicationBar_Play(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/game.xaml", UriKind.Relative));
        }

        private void Review_Click(object sender, EventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["started"] = 6;
            var marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

    }
}