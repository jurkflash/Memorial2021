using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class UrnTransactionRepository : Repository<UrnTransaction>, IUrnTransactionRepository
    {
        public UrnTransactionRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<UrnTransaction> GetByItem(int itemId)
        {
            return MemorialContext.UrnTransactions
                .Where(ut => ut.UrnItemId == itemId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}