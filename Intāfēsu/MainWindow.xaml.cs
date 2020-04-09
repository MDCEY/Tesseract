using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EngineerNumberInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            InputSync.EngineerNumber = EngineerNumberInput.Text;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WorkshopTab.IsSelected)
            {
                InputSync.ActiveTab = WorkshopTab;
            } else if (PartsCageTab.IsSelected)
            {
                InputSync.ActiveTab = PartsCageTab;
            }
        }
    }

    public static class InputSync
    {
        public static string EngineerNumber { get; set; }
        public static TabItem ActiveTab { get; set; }
    }
}
