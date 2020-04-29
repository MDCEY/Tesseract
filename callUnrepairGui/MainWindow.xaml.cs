using System.Windows;
using Workshop;

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
            Other.UnRepair(callNum);
            CallInput.Text = "";
        }
    }
}
