﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kansū;

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
