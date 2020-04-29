using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kurikku
{
    public partial class Workshop : Window
    {
        public Workshop()
        {
            InitializeComponent();
        }

        private async void EngineerNumber_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var Stats = new Kansū.Workshop.EngineerStats();

            while (EngineerNumber.Text.Length == 3)
            {
                await Task.Delay(100);
                var totalRepairs = Kansū.Workshop.RepairsToday(EngineerNumber.Text);
                var totalTime = Kansū.Workshop.TimeTaken(EngineerNumber.Text);
                Stats.Repairs = totalRepairs;
                Stats.HoursUsed = totalTime;
                EngineerStats.DataContext = Stats;
                await Task.Delay(10000);
            }
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            while (BookInsTab.IsSelected)
            {
                BookInsData.ItemsSource = await Task.Run(Kansū.Workshop.RecentlyBookedIn);
                await Task.Delay(5000);
            }

            while (PartsAddedTab.IsSelected)
            {
                PartsAddedData.ItemsSource = await Task.Run(Kansū.Workshop.RecentAddedParts);
                await Task.Delay(1000 * 120);
            }

            while (RepairedTab.IsSelected)
            {
                RepairData.ItemsSource = await Task.Run(Kansū.Workshop.RecentRepairs);
                await Task.Delay(5000);
            }
        }
    }
}