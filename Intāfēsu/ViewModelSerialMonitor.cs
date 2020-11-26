using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Dapper;
using Intāfēsu.Properties;

namespace Intāfēsu
{
    class ViewModelSerialMonitor : ViewModelBase
    {
        private MonitoredSerialNumber _monitoredSerialNumber; //each collection line is this.
        private ObservableCollection<MonitoredSerialNumber> _monitoredSerialNumbers; // This is the main coolection
        private ICommand _addSerialCommand; // this controls the add button
        private string _userSerialNumber; // text box serial number for user input
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
            if (string.IsNullOrEmpty(UserSerialNumber))
            {
                return false;
            }
            return UserSerialNumber != "";
        }

        public ViewModelSerialMonitor()
        {
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();

            AddSerialCommand = new RelayCommand(executeAddSerial ,CanExecuteAddSerialCommand);
            MonitoredSerialNumber = new MonitoredSerialNumber();
            MonitoredSerialNumbers = new ObservableCollection<MonitoredSerialNumber>();
            MonitoredSerialNumbers.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(MonitoredSerialNumbers_CollectionChanged);
        }

        public async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            updateDatagrid();
            CollectionViewSource.GetDefaultView(MonitoredSerialNumbers).Refresh();

        }


        private async void updateDatagrid()
        {

            // Add tesseract query here

            var rand = new Random();

            foreach (var sn in MonitoredSerialNumbers)
            {
                //QueryDB for sn
                //return results
                var record = MonitoredSerialNumbers.FirstOrDefault(x => x.SerialNumber == sn.SerialNumber);
                record.CallNumber = rand.Next(959595).ToString();
                record.User = rand.Next(999).ToString();

            }
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
                CallNumber = "991922",
                User = "406"
            });
        }
    }

    public class MonitoredSerialNumber
    {
        public string SerialNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public string CallNumber { get; set; }
        public string User { get; set; }

    }


}
