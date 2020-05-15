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
        internal List<Workshop.PartsAdded> CurrentData { get; set; }
        internal List<Workshop.PartsAdded> Update { get; set; }
        public RecentPartsIn()
        {
            InitializeComponent();
        }

        private async void RecentPartsIn_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                CurrentData = (List<Workshop.PartsAdded>) RecentPartsInData.ItemsSource;
                Update = await Task.Run(Workshop.RecentAddedParts).ConfigureAwait(true);
                if (CurrentData != null) RecentPartsInData.ItemsSource = Update;
                else if (CurrentData != Update) RecentPartsInData.ItemsSource = Update;
                await Task.Delay(10000).ConfigureAwait(true);
                
            } while (RecentPartsInData.IsVisible);
        }
    }
}
