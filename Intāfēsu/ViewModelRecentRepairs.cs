using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Dapper;
using Intāfēsu.Properties;
using LiveCharts;
using LiveCharts.Wpf;

namespace Intāfēsu
{

    public class ViewModelRecentRepairs : ViewModelBase
    {
        private Repair _repair;
        private ObservableCollection<Repair> _repairs;
        private SeriesCollection _repairBreakDown;
        private List<string> _graphLabelList;

        public Repair Repair
        {
            get => _repair;
            set
            {
                _repair = value;
                NotifyPropertyChanged(nameof(Repair));
            }
        }

        public ObservableCollection<Repair> Repairs
        {
            get => _repairs;
            set
            {
                _repairs = value;
                NotifyPropertyChanged(nameof(Repairs));
            }
        }

        public SeriesCollection RepairBreakDown
        {
            get => _repairBreakDown;
            set
            {
                _repairBreakDown = value;
            }
        }

        public List<string> GraphLabelList
        {
            get => _graphLabelList;
            set => _graphLabelList = value;
        }

        public ViewModelRecentRepairs()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            Repair = new Repair();
            Repairs = new ObservableCollection<Repair>();
            Repairs.CollectionChanged += Repairs_CollectionChanged;
            RepairBreakDown = new SeriesCollection();
            GraphLabelList = new List<string>();

        }

        void Repairs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Repairs));
        }

        private void Update()
        {
            var _ = Convert.FromBase64String(Resources.ConnectionString);
            var connection = new SqlConnection(Encoding.UTF8.GetString(_));
            var update = connection.Query<Repair>(TesseractDb.Queries.RecentRepairs).ToList();

            UpdateGraph(update);


            foreach (Repair updateRepair in update)
            {
                if (Repairs.Any(x => x.SerialNumber == updateRepair.SerialNumber))
                {
                }
                else
                {
                    Repairs.Add(updateRepair);
                }
            }

        }

        public void UpdateGraph(List<Repair> allRepairs)
        {
            foreach (var up in allRepairs.GroupBy(x => x.EngineerName))
            {

                if (RepairBreakDown.All(x => x.Title != up.Key))
                {
                    //If engineer not in graph
                    RepairBreakDown.Add(new RowSeries
                    {
                        Title = up.Key,
                        Values = new ChartValues<int> { allRepairs.Count(x => x.EngineerName == up.Key) },
                    });
                    //GraphLabelList.Add(up.Key);
                }
                else
                {
                    // Engineer already in breakdown. Select and update their object
                    var breakDownRow = RepairBreakDown.SingleOrDefault(x => x.Title == up.Key);
                    if (breakDownRow != null && (int)breakDownRow.Values[0] != allRepairs.Count(x => x.EngineerName == up.Key))
                    {
                        breakDownRow.Values[0] = allRepairs.Count(x => x.EngineerName == up.Key);
                    }
                }
            }

            

        }

        public  void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
    }

}
