using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using MonoGame.Framework.WindowsPhone;
using vservWindowsPhone;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using GoogleAds;

namespace MCGRunToDeath
{
    public partial class GamePage : PhoneApplicationPage
    {
        private Random random = new Random();
        private int clickclose = 0;
        private VservAdControl VAC = VservAdControl.Instance;
        private InterstitialAd interstitialAd;
        private AdView bannerAd;
        private Button VservNote = new Button();
        private Boolean interadmob = true;//bang true thi bo qua LoadAdmobBillbBoard nua

        public GamePage()
        {
            InitializeComponent();
            if (NetworkInterface.GetIsNetworkAvailable()) { App.mang = true; }
            App.game = XamlGame<RunToDeath>.Create("", this);
            if (App.mang) LoadVservBillbBoard();
            else ExecuteGame();
        }
        private void LoadVservBillbBoard()
        {
            adGridVserv.Visibility = Visibility.Visible;
            adGridVserv.Width = Application.Current.Host.Content.ActualWidth;
            adGridVserv.Height = Application.Current.Host.Content.ActualHeight;
            VAC.SetRequestTimeOut(5);
            VAC.DisplayAd("ab881246", adGridVserv);
            VAC.VservAdClosed += new EventHandler(VACCallback_OnVservAdClosing);
            VAC.VservAdError += new EventHandler(VACCallback_OnVservAdNetworkError);
            VAC.VservAdNoFill += new EventHandler(VACCallback_OnVservAdNoFill);
            VservNote.Content = "Please wait a moment ...";
            VservNote.Width = Application.Current.Host.Content.ActualWidth;
            VservNote.Height = 70;
            VservNote.BorderBrush = new SolidColorBrush(Colors.DarkGray);
            VservNote.Background = new SolidColorBrush(Colors.Gray);
            VservNote.VerticalAlignment = VerticalAlignment.Bottom;
            VservNote.Click += adVservClose_Click;
            adGridVserv.Children.Add(VservNote);
        }
        private void adVservClose_Click(object sender, RoutedEventArgs e)
        {
            VservNote.Content = "Click the icon (X) above to continue.";
        }
        private void VACCallback_OnVservAdClosing(object sender, EventArgs e)
        {
            ExecuteGame();
        }
        private void VACCallback_OnVservAdNetworkError(object sender, EventArgs e)
        {
            ExecuteGame();
        }
        private void VACCallback_OnVservAdNoFill(object sender, EventArgs e)
        {
            ExecuteGame();
        }
        private void LoadAdmobBillbBoard()
        {
            adGridVserv.Visibility = Visibility.Collapsed;
            interstitialAd = new InterstitialAd("ca-app-pub-4387121956419078/7678717748");
            interstitialAd.ReceivedAd += interstitialAd_ReceivedAd;
            interstitialAd.FailedToReceiveAd += interstitialAd_FailedToReceiveAd;
            interstitialAd.DismissingOverlay += interstitialAd_DismissingOverlay;
            AdRequest adRequest = new AdRequest();
            interstitialAd.LoadAd(adRequest);
        }
        void interstitialAd_ReceivedAd(object sender, AdEventArgs e)
        {
            interstitialAd.ShowAd();
        }
        void interstitialAd_DismissingOverlay(object sender, AdEventArgs e)
        {
            App.naplai = true;
        }
        void interstitialAd_FailedToReceiveAd(object sender, AdErrorEventArgs e)
        {
            App.naplai = true;
        }
        private void LoadAdmobBanner()
        {
            adGridAdmob.Visibility = Visibility.Visible;
            adGridAdmob.Background = new SolidColorBrush(Colors.Gray);
            adGridAdmob.Width = Application.Current.Host.Content.ActualHeight;
            adGridAdmob.Height = 50;
            bannerAd = new AdView { Format = AdFormats.SmartBanner, AdUnitID = "ca-app-pub-4387121956419078/4565725745" };
            bannerAd.Width = Application.Current.Host.Content.ActualHeight - 50;
            bannerAd.Height = 50;
            bannerAd.HorizontalAlignment = HorizontalAlignment.Left;
            bannerAd.ReceivedAd += OnAdReceived;
            bannerAd.FailedToReceiveAd += OnFailedToReceiveAd;
            TextBlock adNote = new TextBlock();
            adNote.Text = "Click the icon [X] the right to close advertise.";
            adNote.HorizontalAlignment = HorizontalAlignment.Left;
            adNote.VerticalAlignment = VerticalAlignment.Center;
            adNote.Margin = new Thickness(12, 0, 0, 0);
            Button adClose = new Button();
            adClose.Content = "X";
            adClose.Width = 74;
            adClose.Height = 74;
            adClose.Background = new SolidColorBrush(Colors.Gray);
            adClose.HorizontalAlignment = HorizontalAlignment.Right;
            adClose.Margin = new Thickness(0, -12, -12, 0);
            adClose.Click += adClose_Click;
            adGridAdmob.Children.Add(adNote);
            adGridAdmob.Children.Add(bannerAd);
            adGridAdmob.Children.Add(adClose);
            AdRequest adRequest = new AdRequest();
            bannerAd.LoadAd(adRequest);
        }
        private void OnAdReceived(object sender, AdEventArgs e)
        {
            int number = random.Next(0, clickclose * 2);
            if (number == 0) adGridAdmob.Visibility = Visibility.Visible;
            interadmob = true;
        }
        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {
            adGridAdmob.Visibility = Visibility.Collapsed;
            if (!interadmob)
            {
                interadmob = true;
                LoadAdmobBillbBoard();
            }
        }
        private void adClose_Click(object sender, RoutedEventArgs e)
        {
            adGridAdmob.Visibility = Visibility.Collapsed;
            clickclose++;
            if (!interadmob)
            {
                interadmob = true;
                LoadAdmobBillbBoard();
            }
        }
        private void ExecuteGame()
        {
            this.SupportedOrientations = SupportedPageOrientation.Landscape;
            this.Orientation = PageOrientation.Landscape;
            if (XnaSurface.Visibility == Visibility.Collapsed)
            {
                XnaSurface.Visibility = Visibility.Visible;
                if (App.mang) LoadAdmobBanner();
            }
        }
    }
}