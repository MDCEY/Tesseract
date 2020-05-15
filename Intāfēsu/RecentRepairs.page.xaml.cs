using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kansū;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentRepairs.xaml
    /// </summary>
    public partial class RecentRepairs : Page
    {
        public RecentRepairs()
        {
            InitializeComponent();
        }
        private async void FetchRecentRepairs()
        {
            while (this.Visibility == Visibility.Visible)
            {
                RecentRepairData.ItemsSource = await Task.Run(Kansū.Workshop.RecentRepairs).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
            }
        }

        private void RecentRepairs_OnLoaded(object sender, RoutedEventArgs e)
        {
            FetchRecentRepairs();
        }
    }
}
