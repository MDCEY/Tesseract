using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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
            while (true)
            {
                if (this.IsVisible)
                {
                    MovementList.ItemsSource = await Task.Run(() => PartsCage.EngineerParts());
                    CollectionView view =
                        (CollectionView) CollectionViewSource.GetDefaultView(MovementList.ItemsSource);
                    view.SortDescriptions.Add(new SortDescription("MovedAt", ListSortDirection.Descending));
                    await Task.Delay(10000);
                }
                else
                {
                    await Task.Delay(10);
                }
                
            }
        }
        
    }
}