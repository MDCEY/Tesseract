﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Backend stuffs
namespace Kansū
{
    internal class Database
    {
        readonly SqlConnection Connection;

        internal Database()
        {
            byte[] connectionString = Convert.FromBase64String(@"RGF0YSBTb3VyY2U9MTAuMTIxLjY4LjY2XENPT1BFU09MQlJBTkNITElWRSwxODM3OyBJbnRlZ3JhdGVkIFNlY3VyaXR5PUZhbHNlO1VzZXIgSUQ9dGVzc2VyYWN0O1Bhc3N3b3JkPVRlNTVlcmFjdA==");
            string decodedConnectionString = Encoding.UTF8.GetString(connectionString);
            Connection = new SqlConnection(decodedConnectionString);
        }
    }
}
