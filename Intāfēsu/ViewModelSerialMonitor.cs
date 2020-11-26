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
            }
        }
        public ObservableCollection<MonitoredSerialNumber> MonitoredSerialNumbers
        {
            get => _monitoredSerialNumbers;
            set
            {
                _monitoredSerialNumbers = value;
                NotifyPropertyChanged(nameof(MonitoredSerialNumbers));
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
            AddSerialCommand = new RelayCommand(executeAddSerial ,CanExecuteAddSerialCommand);
        }

        private void executeAddSerial(object parameter)
        {
            MessageBox.Show("zZZZZ");
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
