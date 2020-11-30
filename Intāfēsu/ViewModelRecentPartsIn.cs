using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Dapper;
using Intāfēsu.Properties;

namespace Intāfēsu
{
    public class ViewModelRecentPartsIn : ViewModelBase
    {
        #region Property Declaration
        private PartIn _partIn;
        private ObservableCollection<PartIn> _partsIn;
        public PartIn PartIn
        {
            get => _partIn;
            set
            {
                _partIn = value;
                NotifyPropertyChanged(nameof(PartIn));
            }
        }
        public ObservableCollection<PartIn> PartsIn
        {
            get => _partsIn;
            set
            {
                _partsIn = value;
                NotifyPropertyChanged(nameof(PartsIn));
            }
        }
        #endregion

        public ViewModelRecentPartsIn()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();

            PartIn = new PartIn();
            PartsIn = new ObservableCollection<PartIn>();
            PartsIn.CollectionChanged += PartsIn_CollectionChanged;
            Update();

        }
        void PartsIn_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) { NotifyPropertyChanged(nameof(PartsIn)); }


        private void Update()
        {
            var _ = Convert.FromBase64String(Resources.ConnectionString);
            var connection = new SqlConnection(Encoding.UTF8.GetString(_));
            var update = connection.Query<PartIn>(TesseractDb.Queries.RecentlyAddedParts).ToList();

            foreach (var part in update)
            {
                var currentStock = part.CurrentStock;
                var partNumber = part.PartNumber;


                var TempPart = PartsIn.FirstOrDefault(x => x.PartNumber == partNumber);
                if (TempPart == null)
                {
                    PartsIn.Add(part);
                    continue;
                }

                if (TempPart.CurrentStock == currentStock) continue;
                TempPart.CurrentStock = currentStock;
                TempPart.AddedOnDate = part.AddedOnDate;




            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
    }
}