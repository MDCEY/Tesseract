using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kansū;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentBookins.xaml
    /// </summary>
    public partial class RecentBookins : Page
    {
        public RecentBookins()
        {
            InitializeComponent();
        }

        private async void RecentBookins_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                RecentBookinData.ItemsSource = await Task.Run(Kansū.Workshop.RecentlyBookedIn).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
            } while (RecentBookinData.IsVisible);
        }
    }
}
