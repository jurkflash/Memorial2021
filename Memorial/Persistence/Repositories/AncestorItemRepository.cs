using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorItemRepository : Repository<AncestorItem>, IAncestorItemRepository
    {
        public AncestorItemRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<AncestorItem> GetByAncestorArea(int ancestorAreaId)
        {
            return MemorialContext.AncestorItems
                .Where(ai => ai.AncestorAreaId == ancestorAreaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}