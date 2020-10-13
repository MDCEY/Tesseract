using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using Dapper;

namespace Intāfēsu
{
    public class RecentBookinsLogic
    {
        public IEnumerable<dynamic> Result;

        public RecentBookinsLogic()
        {
            byte[] _ = Convert.FromBase64String(Properties.Resources.ConnectionString);

            using (var connection = new SqlConnection(Encoding.UTF8.GetString(_)))
            {
                Result = connection.Query(TesseractDb.Queries.RecentlyBookedIn).ToList();
            }
        }
    }
}
