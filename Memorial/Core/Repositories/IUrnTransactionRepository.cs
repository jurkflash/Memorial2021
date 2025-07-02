using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IUrnTransactionRepository : IRepository<UrnTransaction>
    {
        IEnumerable<UrnTransaction> GetByItem(int itemId);
    }
}
