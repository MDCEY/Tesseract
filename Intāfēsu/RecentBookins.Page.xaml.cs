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
