using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
