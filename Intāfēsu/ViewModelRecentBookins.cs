using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.TextFormatting;
using ControlzEx.Standard;
using Dapper;

namespace Intāfēsu
{
    public class ViewModelRecentBookins : ViewModelBase
    {
        private BookedInPart _bookedInPart;
        private ObservableCollection<BookedInPart> _bookedInParts;

        public BookedInPart BookedInPart
        {
            get => _bookedInPart;
            set
            {
                _bookedInPart = value;
                NotifyPropertyChanged(nameof(BookedInPart));
            }
        }

        public ObservableCollection<BookedInPart> BookedInParts
        {
            get => _bookedInParts;
            set
            {
                _bookedInParts = value;
                NotifyPropertyChanged(nameof(BookedInParts));
            }
        }

        public ViewModelRecentBookins()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            BookedInPart = new BookedInPart();
            BookedInParts = new ObservableCollection<BookedInPart>();
            BookedInParts.CollectionChanged += new NotifyCollectionChangedEventHandler(BookedInParts_CollectionChanged);
        }

        void BookedInParts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(BookedInParts));
        }


        private void Update()
        {
            byte[] _ = Convert.FromBase64String(Properties.Resources.ConnectionString);

            using (var connection = new SqlConnection(Encoding.UTF8.GetString(_)))
            {
                var Result = connection.Query<BookedInPart>(TesseractDb.Queries.RecentlyBookedIn).ToList();
                foreach (var Part in Result)
                {
                    var Date = Part.LastBookedIn;
                    var Description = Part.PartDescription;
                    var Total = Part.Total;

                    var TempPart = BookedInParts.FirstOrDefault(x => x.PartDescription == Description);
                    if (TempPart == null)
                    {
                        BookedInParts.Add(Part);
                        continue;
                    }

                    if (TempPart.Total == Total) continue;
                    TempPart.Total = Total;
                    TempPart.LastBookedIn = Date;

                }
            }
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Update();
        }
    }
}