using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
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
                List<PartsCage.MovedPart> source = new List<PartsCage.MovedPart>();
                source = await Task.Run(() => Kansū.PartsCage.EngineerParts());
                var rows = new List<Row>();
                foreach (var r in source)
                {
                    rows.Add(new Row()
                    {
                        PartNumber = r.PartNumber,
                        PartDescription = r.PartDescription,
                        MovedAt = r.MovedAt.ToString(CultureInfo.CurrentCulture),
                        Engineer = r.Engineer,
                        Location = r.Location,
                        Quantity = r.Quantity.ToString()
                    });
                }
                MovementList.ItemsSource = rows;
                CollectionView view = (CollectionView) CollectionViewSource.GetDefaultView(MovementList.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("MovedAt", ListSortDirection.Descending));
                await Task.Delay(10000);
            }
        }

        public class Row
        {
            public string PartNumber { get; set; } 
            public string PartDescription { get; set; } 
            public string MovedAt { get; set; }
            public string Engineer { get; set; }
            public string Location { get; set; }
            public string Quantity { get; set; } 
        }
    }
}