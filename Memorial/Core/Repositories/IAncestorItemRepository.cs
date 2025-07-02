using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IAncestorItemRepository : IRepository<AncestorItem>
    {
        IEnumerable<AncestorItem> GetByAncestorArea(int ancestorAreaId);
    }
}
