using System.Collections.Generic;
using Dapper;

namespace Intāfēsu
{
    public class BookedInBreakdown
    {
        private readonly TesseractDb _db = new TesseractDb();
        internal IEnumerable<Bookedin> Result;

        BookedInBreakdown()
        {
            Result = _db.Connection.Query<Bookedin>(TesseractDb.Queries.BookedInBreakDown);
        }

        public class Bookedin
        {
            public string User { get; set; }
            public int Total { get; set; }
        }
    }
}