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
