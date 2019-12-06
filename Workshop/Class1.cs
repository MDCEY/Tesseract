using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    }
}