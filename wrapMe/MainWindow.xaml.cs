using System.Windows;
using Logistics;

namespace wrapMe
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TotalJobs.Content = $"{Repairs.Wrapme()} Jobs to wrap.";
        }
    }
    
}