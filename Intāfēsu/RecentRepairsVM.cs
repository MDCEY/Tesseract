using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Dapper;

namespace Intāfēsu
{

    public class RecentRepairsViewModel : ViewModelBase
    {

        private Repair _repair;
        private ObservableCollection<Repair> _repairs;

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

        public RecentRepairsViewModel()
        {
            Repair = new Repair();
            Repairs = new ObservableCollection<Repair>();
            Repairs.CollectionChanged += new NotifyCollectionChangedEventHandler(Repairs_CollectionChanged);
            
        }

        void Repairs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Repairs));
        }

        private void Update()
        {
            var _ = Convert.FromBase64String(Properties.Resources.ConnectionString);
            var Connection = new SqlConnection(Encoding.UTF8.GetString(_));
            var Update = Connection.Query<Repair>(TesseractDb.Queries.RecentRepairs).ToList();
            if (Repairs.Any())
            {
                Repairs = Repairs.Concat(Update) as ObservableCollection<Repair>;
            }
            else
            {
                foreach (var r in Update)
                {
                    Repairs.Add(r);
                }
            }
        }
        public  void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
    }

}
