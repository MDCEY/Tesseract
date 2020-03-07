using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Kansū;

namespace Kurikku
{
    public partial class PartsCageMovements : Window
    {
        public PartsCageMovements()
        {
            InitializeComponent();
            FetchParts();
        }

        private async void FetchParts()
        {
            while (this.IsLoaded)
            {
                MovementList.ItemsSource = await Task.Run(() => Kansū.PartsCage.EngineerParts());
                await Task.Delay(10000);
            }
        }
    }
}