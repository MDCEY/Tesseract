using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private async void GetRepairs_OnClick(object sender, RoutedEventArgs e)
        {
            bool fetchRepairs = GetRepairs.IsChecked.Value;
            bool fetchTime = GetTime.IsChecked.Value;
            bool loop = ToggleLoop.IsChecked.Value;
            RunningLoop.IsIndeterminate = loop;


            if (fetchRepairs)
            {
                TotalRepairs.Content = EngineerInput.Text.Length > 0
                    ? $"{Workshop.Engineer.RepairsToday(EngineerInput.Text)} Repairs"
                    : $"{Workshop.Team.RepairsToday()} Total Repairs";
            }
            else
            {
                TotalRepairs.Content = "";
            }

            if (fetchTime)
            {
                TotalTime.Content = EngineerInput.Text.Length > 0
                    ? $"{Workshop.Engineer.RepairedWorkingTime(EngineerInput.Text)}"
                    : "";
            }
            else
            {
                TotalTime.Content = "";
            }


            if (!loop) return;

            await Task.Delay(10000);
            GetRepairs_OnClick(sender, e);
        }

        private void EngineerInput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string engineerNumber = EngineerInput.Text;
            int inputLength = engineerNumber.Length;
            RunButton.IsEnabled = inputLength == 3;
        }
    }
}
 