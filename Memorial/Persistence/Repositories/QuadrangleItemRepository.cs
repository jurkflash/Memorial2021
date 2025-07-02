using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleItemRepository : Repository<QuadrangleItem>, IQuadrangleItemRepository
    {
        public QuadrangleItemRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<QuadrangleItem> GetByCentre(int quadrangleCentreId)
        {
            return MemorialContext.QuadrangleItems
                .Where(qi => qi.QuadrangleCentreId == quadrangleCentreId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}