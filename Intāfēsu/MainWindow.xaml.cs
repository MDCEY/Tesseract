using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        // Declare variables
        private int _repairs;
        // Declare event
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        



        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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


        private async void RecentlyIn_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            while (WorkshopTab.IsSelected)
            {
                RecentlyInData.ItemsSource = await Task.Run(Kansū.Workshop.RecentlyBookedIn).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
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
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string myuri = string.Empty;
            if (WorkshopTab.IsSelected)
            {
                myuri = "RecentRepairs.page.xaml";
            }

            if (LogisticsTab.IsSelected)
            {
                myuri = "RecentRepairs.page.xaml";
            }

            if (PartsCageTab.IsSelected)
            {
                myuri = "RecentRepairs.page.xaml";
            }
            MainFrame.Source = new Uri(myuri, UriKind.Relative);
        }
    }


}
