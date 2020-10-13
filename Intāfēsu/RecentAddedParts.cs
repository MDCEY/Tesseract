using System;
using System.Collections.Generic;
using Dapper;

namespace Intāfēsu
{
    public class RecentAddedParts
    {
        private readonly TesseractDb _db = new TesseractDb();
            internal IEnumerable<PartsAdded> Result;

            public RecentAddedParts()
            {
                Result = _db.Connection.Query<PartsAdded>(TesseractDb.Queries.RecentlyAddedParts);
            }

            public class PartsAdded
            {
                public string PartNumber { get; set; }
                public string PartDescription { get; set; }
                public int TotalInStock { get; set; }
                public DateTime DateAdded { get; set; }
            }
    }
}