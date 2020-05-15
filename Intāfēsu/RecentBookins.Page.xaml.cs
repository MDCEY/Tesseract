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
        internal List<Workshop.BookedIn> CurrentData { get; set; }
        internal List<Workshop.BookedIn> Update { get; set; }
        public RecentBookins()
        {
            InitializeComponent();
        }

        private async void RecentBookins_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                CurrentData = (List<Workshop.BookedIn>) RecentBookinData.ItemsSource;
                Update = await Task.Run(Workshop.RecentlyBookedIn).ConfigureAwait(true);
                if (CurrentData != null) RecentBookinData.ItemsSource = Update;
                else if (Update != CurrentData) RecentBookinData.ItemsSource = Update;
                await Task.Delay(10000).ConfigureAwait(true);

            } while (RecentBookinData.IsVisible);
        }
    }
}
