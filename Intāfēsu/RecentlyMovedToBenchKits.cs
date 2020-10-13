using System;
using System.Collections.Generic;
using Dapper;

namespace Intāfēsu
{
    public class RecentlyMovedToBenchKits
    {
        private readonly TesseractDb _db = new TesseractDb();
        internal IEnumerable<MovedPart> Result;

        RecentlyMovedToBenchKits()
        {
            Result = _db.Connection.Query<MovedPart>(TesseractDb.Queries.EngineerPartsMovement);
        }

        public class MovedPart
        {
            public string PartNumber { get; set; }
            public string PartDescription { get; set; }
            public DateTime MovedAt { get; set; }
            public string Engineer { get; set; }
            public string Location { get; set; }
            public int Quantity { get; set; }
        }
    }
}