using System.Windows;
using System.Windows.Data;
using Workshop;
namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();


        }



        private void GetRepairs_OnClick(object sender, RoutedEventArgs e)
        {
            if (GetRepairs.IsChecked.Value)
            {
                TotalRepairs.Content = EngineerInput.Text.Length > 0
                    ? $"{Workshop.Engineer.RepairsToday(EngineerInput.Text)} Repairs"
                    : $"{Workshop.Team.RepairsToday()} Total Repairs";
            }
            else
            {
                TotalRepairs.Content = "";
            }

            if (GetTime.IsChecked.Value)
            {
                TotalTime.Content = EngineerInput.Text.Length > 0
                    ? $"{Workshop.Engineer.RepairedWorkingTime(EngineerInput.Text)}"
                    : "";
            }
            else
            {
                TotalTime.Content = "";
            }

        }
    }
}
