using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Kansū
{
    public static class Workshop
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
            database.CreateCommand(query);
            var command = database.Command;
            command.Parameters.AddWithValue("@User", engineerNumber);

            var reader = command.ExecuteReader();
            while (reader.Read()) total = reader.GetInt32(0);
            database.Dispose();
            return total;
        }
    }

    public static class PartsCage
    {
        public static List<MovedPart> EngineerParts()
        {
            const string query =
                @"SELECT Audit_Part_Num as 'PartNumber', Part_Desc as 'Description', Audit_Last_Update as 'MovedAt', Employ_Name 'Engineer', Stock_Bin 'Location', Audit_Qty 'Quantity'   FROM COOPESOLBRANCHLIVE.dbo.SCAudit
                    JOIN COOPESOLBRANCHLIVE.dbo.SCPart on Part_Num = Audit_Part_Num
                    JOIN COOPESOLBRANCHLIVE.dbo.SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Source_Site_Num = Stock_Site_Num
                    JOIN COOPESOLBRANCHLIVE.dbo.SCEmploy ON Employ_Para = Audit_Dest_Site_Num
                    WHERE
                    Audit_Move_Date between
                        Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and
                        Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))
                    AND Audit_Part_Num is not null
                    AND Audit_Source_Site_Num = 'STOWPARTS'
                    AND Audit_Dest_Site_Num LIKE '%BK'";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            var reader = command.ExecuteReader();
            var rows = new List<MovedPart>();
            while (reader.Read())
            {
                MovedPart row = new MovedPart
                {
                    PartNumber = reader["PartNumber"].ToString(),
                    PartDescription = reader["Description"].ToString(),
                    MovedAt = (DateTime)reader["MovedAt"],
                    Engineer = reader["Engineer"].ToString(),
                    Location = reader["Location"].ToString(),
                    Quantity = (int)reader["Quantity"]
                }; 
                rows.Add(row);
            }
            database.Dispose();
            reader.Dispose();
            return rows;
        }

        public class MovedPart
        {
            public string PartNumber  { get; set; }
            public string PartDescription  { get; set; }
            public DateTime MovedAt { get; set; }
            public string Engineer { get; set; }
            public string Location { get; set; }
            public int Quantity { get; set; }
        }
    }
}