using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Dapper;
using Intāfēsu.Properties;
using LumenWorks.Framework.IO.Csv;
using Microsoft.Win32;

namespace Intāfēsu
{
    class ViewModelSerialMonitor : ViewModelBase
    {
        
        private Visibility _scanningMonitor;

        public Visibility ScanningMonitor
        {
            get => _scanningMonitor;
            set
            {
                _scanningMonitor = value;
                NotifyPropertyChanged(nameof(ScanningMonitor));
            }
        }

        private MonitoredSerialNumber _monitoredSerialNumber; //each collection line is this.
        private ObservableCollection<MonitoredSerialNumber> _monitoredSerialNumbers; // This is the main coolection
        private ICommand _addSerialCommand; // this controls the add button
        private string _userSerialNumber; // text box serial number for user input
        private ICommand _importCsvCommand; //this controls the import button
        public MonitoredSerialNumber MonitoredSerialNumber
        {
            get => _monitoredSerialNumber;
            set
            {
                _monitoredSerialNumber = value;
                NotifyPropertyChanged(nameof(MonitoredSerialNumber));
                OnPropertyChanged(nameof(MonitoredSerialNumber));
            }
        }
        public ObservableCollection<MonitoredSerialNumber> MonitoredSerialNumbers
        {
            get => _monitoredSerialNumbers;
            set
            {
                _monitoredSerialNumbers = value;
                NotifyPropertyChanged(nameof(MonitoredSerialNumbers));
                OnPropertyChanged(nameof(MonitoredSerialNumbers));
            }
        }
        public ICommand AddSerialCommand { get; set; }
        public ICommand ImportCsvCommand { get; set; }
        private DateTime _lastscan;

        public DateTime LastScan
        {
            get => _lastscan;
            set
            {
                _lastscan = value;
                OnPropertyChanged(nameof(LastScan));
            }
        }

        public string UserSerialNumber
        {
            get => _userSerialNumber;
            set
            {
                _userSerialNumber = value;
            }
        }
        private bool CanExecuteAddSerialCommand(object parameter)
        {
            var isInput = string.IsNullOrEmpty(UserSerialNumber);
            if (isInput)
            {
                return false;
            }
            var isDuplicate = MonitoredSerialNumbers.Any(x => x.SerialNumber.ToUpper() == UserSerialNumber.ToUpper());
            if (isDuplicate)
            {
                return false;
            }
            // if there is input and it is not duplicate then true (enable)
            return true;
        }
        
        private bool CanExecuteImportCsvCommand(object parameter)
        {
            return true;
        }

        public ViewModelSerialMonitor()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            LastScan = new DateTime();
            AddSerialCommand = new RelayCommand(executeAddSerial ,CanExecuteAddSerialCommand);
            ImportCsvCommand = new RelayCommand(executeImportCsv, CanExecuteImportCsvCommand);
            MonitoredSerialNumber = new MonitoredSerialNumber();
            MonitoredSerialNumbers = new ObservableCollection<MonitoredSerialNumber>();
            MonitoredSerialNumbers.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(MonitoredSerialNumbers_CollectionChanged);
        }

        public async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateDatagrid();

            try
            {
                CollectionViewSource.GetDefaultView(MonitoredSerialNumbers).Refresh();
            }
            catch (InvalidOperationException)
            {
                //retry on next tick
            }
            LastScan = DateTime.Now;
        }


        private async void updateDatagrid()
        {

            ScanningMonitor = Visibility.Visible;

            // Add tesseract query here

            var _ = Convert.FromBase64String(Resources.ConnectionString);
            var connection = new SqlConnection(Encoding.UTF8.GetString(_));
            

            var rand = new Random();

            foreach (var sn in MonitoredSerialNumbers)
            {
                //QueryDB for sn
                //return results
                if (sn.CallNumber != null)
                {
                    continue;
                }
                var update = connection.QueryFirstOrDefault<dynamic>(TesseractDb.Queries.SerialMonitor, new {sn.SerialNumber});
                if (update == null)
                {
                    continue;
                }
                if (update.call_num != null)
                {
                    var record = MonitoredSerialNumbers.FirstOrDefault(x => x.SerialNumber == sn.SerialNumber);
                    record.CallNumber = update.call_num;
                    record.User = update.Call_User;
                    try
                    {
                        CollectionViewSource.GetDefaultView(MonitoredSerialNumbers).Refresh();
                    } catch (System.InvalidOperationException)
                    {
                        // will retry on next loop
                    }
                }

            }
            connection.Dispose();
            ScanningMonitor = Visibility.Hidden;

        }

        private void MonitoredSerialNumbers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(MonitoredSerialNumbers));
        }

        private void executeAddSerial(object parameter)
        {
            MonitoredSerialNumbers.Add(new MonitoredSerialNumber
            {
                DateAdded = DateTime.Now,
                SerialNumber = UserSerialNumber,
                CallNumber = null,
                User = null
            });
        }

        private void executeImportCsv(object parameter)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string fileName = dlg.FileName;
                using (var csv = new CachedCsvReader(new StreamReader(fileName), false))
                {
                    foreach (var sn in csv)
                    {
                        var isDuplicate = MonitoredSerialNumbers.Any(x => x.SerialNumber.ToUpper() == sn[0].ToUpper());
                        if (isDuplicate)
                        {
                            continue;
                        }
                        else
                        {
                            MonitoredSerialNumbers.Add(new MonitoredSerialNumber
                            {
                                DateAdded = DateTime.Now,
                                SerialNumber = sn[0],
                                CallNumber = null,
                                User = null
                            });
                        }
                    }
                }
            }
        }
    }

    public class MonitoredSerialNumber
    {
        public string SerialNumber { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int? CallNumber { get; set; }
        public string User { get; set; }

    }


}
