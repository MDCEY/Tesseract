using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Text;

namespace Logistics
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

    public class Repairs
    {
        public static string Wrapme()
        {
            string total = String.Empty;
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText =
                "SELECT COUNT(Call_Num) as 'totalcount' FROM COOPESOLBRANCHLIVE.dbo.SCCALL WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE())) AND Job_Ship_Date is null AND Call_Area_Code = 'WREP' AND Call_CalT_Code = 'ZR1'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = reader["totalcount"].ToString();
            }

            return total;
        }
    }

    public class Other
    {
        public static int AddDestructionTag(String serialNumber, string reference)
        {
            Database db = new Database();
            SqlCommand command = new SqlCommand();
            db.Connection.Open();
            command.Connection = db.Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE COOPESOLBRANCHLIVE.dbo.SPNloc SET Nloc_Rev_Level = @reference WHERE Nloc_ID_Num = @serialNumber";
            command.Parameters.AddWithValue("@reference", reference);
            command.Parameters.AddWithValue("@serialNumber", serialNumber);
            return command.ExecuteNonQuery();

            }
        }
    }
