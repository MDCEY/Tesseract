using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using System.Xml;
using DYMO.Label.Framework;
using Label = DYMO.Label.Framework.Label;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentBookins.xaml
    /// </summary>
    public partial class RecentBookins : Page
    {

        internal List<dynamic> CurrentData = new List<dynamic>();
        public RecentBookins()
        {
            InitializeComponent();
            this.DataContext = this;
        }



private async void RecentBookins_OnLoaded(object sender, RoutedEventArgs e)
{
    
            do
            {
                var update = await Task.Run(() => new RecentBookinsLogic().Result);
                if (CurrentData.Count > 1)
                {
                    CurrentData.Union(update);
                }
                else
                {
                    CurrentData = (List<dynamic>)update;
                }
                RecentBookedInData.ItemsSource = CurrentData;
                await Task.Delay(10000).ConfigureAwait(true);

            } while (RecentBookedInData.IsVisible);
        }

    }
}
