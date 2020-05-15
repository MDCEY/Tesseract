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
        internal List<Workshop.RecentRepair> CurrentData { get; set; }

        internal List<Workshop.RecentRepair> Update { get; set; }

        public RecentRepairs()
        {
            InitializeComponent();
        }
        private async void FetchRecentRepairs()
        {
            do
            {
                CurrentData = (List<Workshop.RecentRepair>) RecentRepairData.ItemsSource;
                Update = await Task.Run(Workshop.RecentRepairs).ConfigureAwait(true);
                if (CurrentData == null) RecentRepairData.ItemsSource = Update;
                else if (CurrentData.Count != Update.Count) RecentRepairData.ItemsSource = Update;                    
                await Task.Delay(10000).ConfigureAwait(true);
            } while (RecentRepairData.IsVisible);
        }

        private void RecentRepairs_OnLoaded(object sender, RoutedEventArgs e)
        {
            FetchRecentRepairs();
        }
    }
}
