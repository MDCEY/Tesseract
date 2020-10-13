using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

 

        public void updatePageSourceOnClick(object sender, RoutedEventArgs e)
        {
            // Convert RoutedEventArgs to a button
            if (e == null) return;
            // Retrieve element name
            if (!(e.Source is Button buttonSrc)) return;
            var buttonName = buttonSrc.Name;

            string[] separator = {"Page"};
            // Split the buttonName to parse the matching page name.
            var pageName = buttonName.Split(separator,2,StringSplitOptions.RemoveEmptyEntries);
            // Update the frame to the relevant page
            MainFrame.Source = new Uri("Pages/" + pageName[0] + ".Page.xaml", UriKind.Relative);
           
            // set active buttons to disabled
            foreach (var b in MainNavigation.FindChildren<Button>())
            {
                b.IsEnabled = b != buttonSrc;
            }
            // trigger page transition
            FrameTransition.Reload();



        }


        /*private async Task indieStats()
        {
            while (EngineerNumber.Length == 3) {
                var repairs = Kansū.Workshop.RepairsToday(EngineerNumber).ToString(CultureInfo.CurrentCulture);
                var TimeSpent = Kansū.Workshop.TimeTaken(EngineerNumber).ToString();
                IndieRepairs.Text = repairs;
                IndieTimeSpent.Text = TimeSpent;
                await Task.Delay(10000).ConfigureAwait(true);
            }
        }

        private async void EngineerNumberInput_OnKeyUp(object sender, KeyEventArgs e)
        {
            EngineerNumber = EngineerNumberInput.Text;
            
            if (EngineerNumber.Length == 3)
            {
                ConfigIcon.Visibility = Visibility.Hidden;
                IndieStats.Visibility = Visibility.Visible;
                await indieStats().ConfigureAwait(true);
            }
            else
            {
                ConfigIcon.Visibility = Visibility.Visible;
                IndieStats.Visibility = Visibility.Hidden;
            }
            

        }


        private  async void RecentParts_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            while (WorkshopTab.IsSelected)
            {
                RecentPartsData.ItemsSource = await Task.Run(Kansū.Workshop.RecentAddedParts).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
            }
        }

        private async void RecentRepairs_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            while (WorkshopTab.IsSelected)
            {
                RecentRepairData.ItemsSource = await Task.Run(Kansū.Workshop.RecentRepairs).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
            }
        }


        private void ConfigTile_OnClick(object sender, RoutedEventArgs e)
        {
            ConfigFlyout.IsOpen = true;
        }*/
    }


}
