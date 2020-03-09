using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Backend stuffs
namespace Kansū
{
    internal class Database : IDisposable
    {
        internal SqlConnection Connection;
        internal SqlCommand Command;

        internal Database()
        {
            byte[] connectionString = Convert.FromBase64String(@"RGF0YSBTb3VyY2U9MTAuMTIxLjY4LjY2XENPT1BFU09MQlJBTkNITElWRSwxODM3OyBJbnRlZ3JhdGVkIFNlY3VyaXR5PUZhbHNlO1VzZXIgSUQ9dGVzc2VyYWN0O1Bhc3N3b3JkPVRlNTVlcmFjdA==");
            string decodedConnectionString = Encoding.UTF8.GetString(connectionString);
            Connection = new SqlConnection(decodedConnectionString);
        }

        internal void CreateCommand(string query)
        {
            SqlCommand command = new SqlCommand();
            Connection.Open();
            command.Connection = Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            Command = command;
        }

        public void Dispose()
        {
            Connection.Dispose();
            Command.Dispose();        }
    }
}
