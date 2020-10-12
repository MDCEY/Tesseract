using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentPartsIn.xaml
    /// </summary>
    public partial class RecentPartsIn : Page
    {
        internal List<RecentAddedParts.PartsAdded> CurrentData { get; set; }
        internal List<RecentAddedParts.PartsAdded> Update { get; set; }
        public RecentPartsIn()
        {
            InitializeComponent();
        }

        private async void RecentPartsIn_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                CurrentData = (List<RecentAddedParts.PartsAdded>) RecentPartsInData.ItemsSource;
                Update = (List<RecentAddedParts.PartsAdded>) await Task.Run(()=> new RecentAddedParts().Result).ConfigureAwait(true);
                if (CurrentData != null) RecentPartsInData.ItemsSource = Update;
                else if (CurrentData != Update) RecentPartsInData.ItemsSource = Update;
                await Task.Delay(10000).ConfigureAwait(true);
                
            } while (RecentPartsInData.IsVisible);
        }
    }
}
