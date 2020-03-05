using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kansū
{
    class Workshop
    {
        public static int RepairsToday(string engineerNumber)
        {
            int total = 0;
            const string query = "SELECT " +
                                   "ISNULL(COUNT(Call_Num),0) as 'Repairs'" +
                                   "FROM COOPESOLBRANCHLIVE.dbo.SCCall " +
                                   "WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) " +
                                   "AND Call_Employ_Num = @User";

            Database database = new Database();
            var connection = database.Connection;
            var command = database.CreateCommand(connection, query);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = reader.GetInt32(0);
            }
            return total;
        }

    }
}
