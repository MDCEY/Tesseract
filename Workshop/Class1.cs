using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Workshop
{
    public class Database
    {
        internal SqlConnection Connection;

        internal Database()
        {
           string connectionString = @"RGF0YSBTb3VyY2U9MTAuMTIxLjY4LjY2XENPT1BFU09MQlJBTkNITElWRSwxODM3OyBJbnRlZ3JhdGVkIFNlY3VyaXR5PUZhbHNlO1VzZXIgSUQ9dGVzc2VyYWN0O1Bhc3N3b3JkPVRlNTVlcmFjdA==";
           var dbBytes = Convert.FromBase64String(connectionString);
           var decodedConnectionString = Encoding.UTF8.GetString(dbBytes);
           Connection = new SqlConnection(decodedConnectionString);
        }
    }
    
    public class Team
    {
        public static string RepairsToday()
        {
            string total = "";

            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT ISNULL(COUNT(Call_Num),0) FROM COOPESOLBRANCHLIVE.dbo.SCCall WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))";
            
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = reader.GetInt32(0).ToString();
            }
            reader.Close();
            command.Dispose();
            return total;
        }
    }

    public class Engineer
    {
        public static string RepairsToday(string engineer)
        {
            string total = "";

            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT ISNULL(COUNT(Call_Num),0) FROM COOPESOLBRANCHLIVE.dbo.SCCall WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) AND Call_Employ_Num = @User";
            command.Parameters.AddWithValue("@User", engineer);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = reader.GetInt32(0).ToString();
            }
            reader.Close();
            command.Dispose();
            return total;
        }

        public static string RepairedWorkingTime(string engineer)
        {
            string total = "";

            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT isnull(SUM(FSR_Work_Time),0) " +
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
            command.Parameters.AddWithValue("@User", engineer);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = TimeSpan.FromHours((double) reader.GetDecimal(0)).ToString();
            }
            reader.Close();
            command.Dispose();
            return total;
        }

        public static Dictionary<string,string> CurrentStock(string engineer)
        {
            Dictionary<string, string> stockList = new Dictionary<string, string>();
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();

            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT Part_Desc, Stock_Total_Qty FROM COOPESOLBRANCHLIVE.dbo.SPStock JOIN COOPESOLBRANCHLIVE.dbo.SCPart ON Part_Num = Stock_Part_Num  WHERE Stock_Total_Qty > 0 and Stock_Site_Num = @User";
            command.Parameters.AddWithValue("@User", $"{engineer}BK");
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                stockList.Add(reader["Part_Desc"].ToString() ,reader["Stock_Total_Qty"].ToString());
            }
            reader.Close();
            command.Dispose();
            return stockList;

        }

        public static IEnumerable<string> PartsInStock(string engineer)
        {
            IEnumerable<string> stockList = new List<string>(); 
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();

            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT Stock_Part_Num FROM COOPESOLBRANCHLIVE.dbo.SPStock WHERE Stock_Total_Qty > 0 and Stock_Site_Num = @User";
            command.Parameters.AddWithValue("@User", $"{engineer}BK");
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                stockList.Append(reader["Stock_Part_Num"].ToString());
            }
            reader.Close();
            command.Dispose();
            return stockList;
        }
    }

    public class Parts
    {
        public static string AverageRepairTime(string partNumber)
        {
            string timeTaken = null;
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT AVG(FSR_WORK_TIME) as workTime from COOPESOLBRANCHLIVE.dbo.SCFSR WHERE FSR_PROD_NUM = @Part";
            command.Parameters.AddWithValue("@Part", partNumber);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                timeTaken = TimeSpan.FromHours((double)reader.GetDecimal(0)).ToString();
            }
            reader.Close();
            command.Dispose();
            return timeTaken;
        }
    }

    public class Other
    {
        public static int UnRepair(string call)
        {

                Database db = new Database();
                SqlCommand command = new SqlCommand();
                db.Connection.Open();
                command.Connection = db.Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE COOPESOLBRANCHLIVE.dbo.SCCall SET Job_CDate = NULL, Job_CTime = NULL WHERE Call_Num = @call";
                command.Parameters.AddWithValue("@call", call);
                return command.ExecuteNonQuery();
        }

        public static Dictionary<string,string> BerDetails(string call)
        {
            Dictionary<string,string> Args = new Dictionary<string,string>();
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();

            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT Employ_Name, Call_Prod_Num, Call_Ser_Num, job_ref6, Call_Num, FSR_Solution FROM COOPESOLBRANCHLIVE.dbo.SCCall  LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCEmploy ON Call_Employ_Num = Employ_Num JOIN COOPESOLBRANCHLIVE.dbo.SCFSR ON Call_Num = FSR_Call_Num WHERE Call_Num = @Call AND FSR_Area_Code='BW3RP'";
            command.Parameters.AddWithValue("@Call", $"{call}");
            var reader = command.ExecuteReader();


            while (reader.Read())
            {
                Args.Add("Engineer Name", reader["Employ_Name"].ToString());
                Args.Add("BER Date", DateTime.Today.ToLongDateString());
                Args.Add("MaterialNumber", reader["Call_Prod_Num"].ToString());
                Args.Add("Serial Number", reader["Call_Ser_Num"].ToString());
                Args.Add("RO Number", reader["job_ref6"].ToString());
                Args.Add("Call Number", reader["Call_Num"].ToString());
                Args.Add("Service Report", reader["FSR_Solution"].ToString());

            }
            reader.Close();
            command.Dispose();
            return Args;
        }
    }
}