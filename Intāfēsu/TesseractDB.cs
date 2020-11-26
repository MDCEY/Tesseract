using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Intāfēsu.Properties;


namespace Intāfēsu
{
    internal class TesseractDb : IDisposable
    {

        private protected readonly string ConnString = Properties.Resources.ConnectionString;
        internal IDbConnection Connection;
        internal SqlCommand Command;

        public TesseractDb()
        {
            EstablishConnection();
        }

        private protected void EstablishConnection()
        {
            byte[] _ = Convert.FromBase64String(ConnString);
            Connection = new SqlConnection(Encoding.UTF8.GetString(_));
        }
        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "No user input. Just developer input")]
        internal SqlCommand CreateCommand(string query)
        {
            SqlCommand command = new SqlCommand();
            Connection.Open();
            command.Connection = Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            Command = command;
            return Command;
        }*/

        public void Dispose()
        {
            Connection.Dispose();
            Command.Dispose();
        }

        internal class Queries
        {
            public static string UserRepairsByDate => ReadResource("UserRepairsByDate.sql");
            public static string UserRepairTimeByDate => ReadResource("UserRepairTimeByDate.sql");
            public static string RecentlyBookedIn => ReadResource(nameof(RecentlyBookedIn) + ".sql"); //Done
            public static string RecentlyAddedParts => ReadResource(nameof(RecentlyAddedParts) + ".sql"); //Done
            public static string RecentRepairs => ReadResource(nameof(RecentRepairs) + ".sql"); //Done
            public static string BookedInBreakDown => ReadResource(nameof(BookedInBreakDown) + ".sql"); //Done
            public static string ProductDetailsFromCall => ReadResource(nameof(ProductDetailsFromCall) + ".sql"); 
            public static string EngineerPartsMovement => ReadResource(nameof(EngineerPartsMovement) + ".sql"); //Done
            public static string SerialMonitor => ReadResource(nameof(SerialMonitor) + ".sql"); //Done
            private static string ReadResource(string fileName)
            {
                // Determine path
                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(fileName));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}