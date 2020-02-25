using System.Windows;


namespace callUnrepairGui
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
            string callNum = CallInput.Text;
            Workshop.Other.UnRepair(callNum);
            CallInput.Text = "";
        }
    }
}
