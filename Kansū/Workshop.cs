using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

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
        public static TimeSpan TimeTaken(string engineerNumber)
        {
            var total = new TimeSpan(0,0,0,0);
            const string query = "SELECT isnull(SUM(FSR_Work_Time),0) " +
                                 "FROM COOPESOLBRANCHLIVE.dbo.SCCall " +
                                 "JOIN COOPESOLBRANCHLIVE.dbo.SCFSR ON " +
                                 "FSR_Call_Num = Call_Num AND " +
                                 "Call_FSR_Count = FSR_Num " +
                                 "JOIN COOPESOLBRANCHLIVE.dbo.SCEmploy ON " +
                                 "Call_Employ_Num = Employ_Num " +
                                 "WHERE Employ_Para like '%BK' AND " +
                                 "Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and " +
                                 "Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) AND " +
                                 "Call_Employ_Num = @User";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            command.Parameters.AddWithValue("@User", engineerNumber);
            
            var reader = command.ExecuteReader();
            var worktime = new decimal();
            while (reader.Read()) worktime = reader.GetDecimal(0);
            total = TimeSpan.FromHours((double)worktime);
            database.Dispose();
            return total;
        }

        public static List<BookedIn> RecentlyBookedIn()
        {
            const string query =
                "SELECT count(Part_Num) 'total',isnull(Part_Desc, Prod_Desc) 'Description' ,MAX(Call_InDate) 'Last in' FROM COOPESOLBRANCHLIVE.dbo.SCCall " +
                "left join COOPESOLBRANCHLIVE.dbo.SCPart ON Job_Part_Num = Part_Num " +
                "LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCProd ON Job_Part_Num = Prod_Desc " +
                "WHERE DATEDIFF(hh,Call_InDate, GETDATE()) < 16 " +
                "AND Job_CDate is null " +
                "AND Call_CalT_Code = 'ZR1' " +
                "GROUP BY Part_Desc, Prod_Desc " +
                "ORDER BY [Last in] DESC, [Description] ";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            var reader = command.ExecuteReader();
            var rows = new List<BookedIn>();
            while (reader.Read())
            {
                BookedIn row = new BookedIn
                {
                    Total = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    MostRecent =  reader.GetDateTime(2)
                };
                rows.Add(row);
            }
            database.Dispose();
            reader.Dispose();
            return rows;
        }
        public class BookedIn
        {
            public int Total { get; set; }
            public string Description { get; set; }
            public DateTime MostRecent { get; set; }
        }

        public static List<PartsAdded> RecentAddedParts()
        {
            const string query = "SELECT Audit_Move_Date, " +
                                 "Audit_Part_Num, " +
                                 "Audit_Type, " +
                                 "Audit_Dest_Site_Num, " +
                                 "Audit_Qty, " +
                                 "Part_Desc, " +
                                 "Stock_Total_Qty " +
                                 "FROM   COOPESOLBRANCHLIVE.dbo.SCPart " +
                                 "INNER JOIN COOPESOLBRANCHLIVE.dbo.SCAudit ON Part_Num = Audit_Part_Num " +
                                 "INNER JOIN COOPESOLBRANCHLIVE.dbo.SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Dest_Site_Num = Stock_Site_Num " +
                                 "WHERE  Audit_Dest_Site_Num = 'STOWPARTS' " +
                                 "AND Audit_Type IN ('PO','SC') " +
                                 "AND Audit_Qty > 0 " +
                                 "AND Stock_Total_Qty > 0 " +
                                 "AND DATEDIFF(dd,Audit_Move_Date,GETDATE()) <= 7 " +
                                 "ORDER BY Audit_Move_Date DESC, Part_Desc";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            var reader = command.ExecuteReader();
            var rows = new List<PartsAdded>();
            while (reader.Read())
            {
                PartsAdded row = new PartsAdded
                {
                    PartNumber = reader.GetString(1),
                    PartDescription = reader.GetString(5),
                    TotalInStock = reader.GetInt32(6),
                    DateAdded = reader.GetDateTime(0)
                };
                rows.Add(row);
            }
            database.Dispose();
            reader.Dispose();
            return rows.DistinctBy(i => i.PartNumber).ToList();
        }
        public class PartsAdded
        {
            public string PartNumber { get; set; }
            public string PartDescription { get; set; }
            public int TotalInStock { get; set; }
            public DateTime DateAdded { get; set; }
        }

        public static List<RecentRepair> RecentRepairs()
        {
            const string query =
                "SELECT Job_CDate,Call_Employ_Num, Call_Ser_Num, isnull(Part_Desc, Prod_Desc), isnull(FSRL_Cost, 0) from COOPESOLBRANCHLIVE.dbo.SCCall " +
                "LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCFSRL on FSRL_Call_Num = Call_Num " +
                "LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPART on isnull(Call_Prod_Num,Job_Part_Num) = Part_Num " +
                "LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPROD on isnull(Call_Prod_Num,Job_Part_Num) = Prod_Num " +
                "WHERE datediff(day,Job_CDate,getdate()) = 0 " +
                "group by Job_CDate, Call_Ser_Num, isnull(Part_Desc, Prod_Desc), isnull(FSRL_Cost, 0), Call_Employ_Num " +
                "Order by Job_CDate DESC, Call_Ser_Num";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            var reader = command.ExecuteReader();
            var rows = new List<RecentRepair>();
            while (reader.Read())
            {
                RecentRepair row = new RecentRepair
                {
                    RepairedAt = reader.GetDateTime(0),
                    SerialNumber = reader.GetString(2),
                    Description = reader.GetString(3),
                    Cost = reader.GetDecimal(4),
                    Engineer = reader.GetString(1)

                };
                rows.Add(row);
            }
            database.Dispose();
            reader.Dispose();
            return rows;
        }
        public class RecentRepair
        {
            public DateTime RepairedAt {get; set;}
            public string SerialNumber {get; set;}
            public string Description {get; set;}
            public decimal Cost {get; set;}
            public string Engineer { get; set; }
        }
    }

    public static class Logistics
    {
        public static List<BookinBreakdown> FetchBookInBreakdown()
        {
            const string query = "Select Employ_Name, Count(Call_Num) FROM COOPESOLBRANCHLIVE.dbo.SCCall INNER JOIN COOPESOLBRANCHLIVE.dbo.SCEMPLOY On Call_User = Employ_Num WHERE Call_InDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) and Call_CalT_Code='ZR1' GROUP BY Employ_Name";
            var database = new Database();
            var connection = database.Connection;
            database.CreateCommand(query);
            var command = database.Command;
            var reader = command.ExecuteReader();
            var rows = new List<BookinBreakdown>();
            while (reader.Read())
            {
                BookinBreakdown row = new BookinBreakdown
                {
                    User = reader.GetString(0),
                    Total = reader.GetInt32(1),
                };
                rows.Add(row);
            }
            database.Dispose();
            reader.Dispose();
            return rows;
        }

        public class BookinBreakdown
        {
            public string User { get; set; }
            public int Total { get; set; }
        }

        public class CallDetails
        {
            public string Product { get; set; }
            public string Description { get; set; }
            public CallDetails(string value)
            {
                const string query = "Select Prod_Num, Prod_Desc  FROM COOPESOLBRANCHLIVE.dbo.SCProd INNER JOIN COOPESOLBRANCHLIVE.dbo.SCCALL On Call_Prod_Num = Prod_Num WHERE Call_Num = @value";
                var database = new Database();
                var connection = database.Connection;
                database.CreateCommand(query);
                var command = database.Command;
                command.Parameters.AddWithValue("@value", value);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product = reader.GetString(0);
                    Description = reader.GetString(1);
                }
                database.Dispose();
                reader.Dispose();
            }
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