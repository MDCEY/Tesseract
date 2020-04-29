using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Kurikku
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PartsCageMovements PartsCageMovementsWindow = new PartsCageMovements();
        private readonly Workshop WorkshopWindow = new Workshop();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPartsCage_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            PartsCageMovementsWindow.Show();
        }

        private void ShowWorkshop_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            WorkshopWindow.Show();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }
    }
}