using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kansū;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentPartsIn.xaml
    /// </summary>
    public partial class RecentPartsIn : Page
    {
        public RecentPartsIn()
        {
            InitializeComponent();
        }

        private async void RecentPartsIn_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                RecentPartsInData.ItemsSource = await Task.Run(Kansū.Workshop.RecentAddedParts).ConfigureAwait(true);
                await Task.Delay(10000).ConfigureAwait(true);
            } while (RecentPartsInData.IsVisible);
        }
    }
}
