using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Memorial.Persistence.Repositories
{
    public class NumberRepository : INumberRepository
    {
        private const string _AF = "AF";
        private const string _PO = "PO";
        private const string _IV = "IV";
        private const string _RE = "RE";
        private const string _cremation = "Cremation";
        private const string _miscellaneous = "Miscellaneous";

        private MemorialContext _context;

        public NumberRepository()
        {
            _context = new MemorialContext();
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public int GetCremationNewAF(int itemId, int year)
        {
            return GetNumber(_cremation, _AF, itemId, year);
        }

        public int GetCremationNewIV(int itemId, int year)
        {
            return GetNumber(_cremation, _IV, itemId, year);
        }

        public int GetCremationNewRE(int itemId, int year)
        {
            return GetNumber(_cremation, _RE, itemId, year);
        }

        public int GetMiscellaneousNewAF(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _AF, itemId, year);
        }

        public int GetMiscellaneousNewIV(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _IV, itemId, year);
        }

        public int GetMiscellaneousNewRE(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _RE, itemId, year);
        }

        private int GetNumber(string catalog, string type, int itemId, int year)
        {
            var outId = new SqlParameter();
            outId.ParameterName = "@NewId";
            outId.Direction = ParameterDirection.Output;
            outId.SqlDbType = SqlDbType.Int;
            var authors = _context.Database.ExecuteSqlCommand("procNumbering @Catalog, @ItemID, @Type, @Year, @NewId OUT",
                new SqlParameter("@Catalog", catalog),
                new SqlParameter("@ItemID", itemId),
                new SqlParameter("@Type", type),
                new SqlParameter("@Year", year),
                outId);
            return Convert.ToInt32(outId.Value.ToString());
        }
    }
}