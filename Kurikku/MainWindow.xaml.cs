using System.Windows;

namespace Kurikku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PartsCageMovements PartsCageMovementsWindow = new PartsCageMovements();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPartsCage_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            PartsCageMovementsWindow.Show();
        }
    }
}
