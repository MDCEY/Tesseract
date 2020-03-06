using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Kansū
{
    internal class Workshop
    {
        public static int RepairsToday(string engineerNumber)
        {
            var total = 0;
            const string query = "SELECT " +
                                 "ISNULL(COUNT(Call_Num),0) as 'Repairs'" +
                                 "FROM COOPESOLBRANCHLIVE.dbo.SCCall " +
                                 "WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) " +
                                 "AND Call_Employ_Num = @User";

            var database = new Database();
            var connection = database.Connection;
            var command = database.CreateCommand(connection, query);
            var reader = command.ExecuteReader();
            while (reader.Read()) total = reader.GetInt32(0);
            return total;
        }
    }

    internal class PartsCage
    {
        private static List<MovedPart> engineerParts()
        {
            const string query =
                @"SELECT Audit_Part_Num as 'PartNumber', Part_Desc as 'Description', Audit_Last_Update as 'MovedAt', Employ_Name 'Engineer', Stock_Bin 'Location', Audit_Qty 'Quantity'   FROM SCAudit
                    JOIN SCPart on Part_Num = Audit_Part_Num
                    JOIN SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Source_Site_Num = Stock_Site_Num
                    JOIN SCEmploy ON Employ_Para = Audit_Dest_Site_Num
                    WHERE
                    Audit_Move_Date between
                        Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and
                        Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))
                    AND Audit_Part_Num is not null
                    AND Audit_Source_Site_Num = 'STOWPARTS'
                    AND Audit_Dest_Site_Num LIKE '%BK'";
            var database = new Database();
            var connection = database.Connection;
            var command = database.CreateCommand(connection, query);
            var reader = command.ExecuteReader();
            var rows = new List<MovedPart>();
            while (reader.Read())
            {
                MovedPart row = new MovedPart
                {
                    partNumber = reader["PartNumber"].ToString(),
                    partDescription = reader["Description"].ToString(),
                    movedAt = (DateTime)reader["MovedAt"],
                    engineer = reader["Engineer"].ToString(),
                    location = reader["Location"].ToString(),
                    quantity = (int)reader["Quantity"]
                };
            }

        }

        public struct MovedPart
        {
            public string partNumber;
            public string partDescription;
            public DateTime movedAt;
            public string engineer;
            public string location;
            public int quantity;
        }
    }
}