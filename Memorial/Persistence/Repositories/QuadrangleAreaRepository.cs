using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleAreaRepository : Repository<QuadrangleArea>, IQuadrangleAreaRepository
    {
        public QuadrangleAreaRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<QuadrangleArea> GetByCentre(int quadrangleCentreId)
        {
            return MemorialContext.QuadrangleAreas
                .Where(qa => qa.QuadrangleCentreId == quadrangleCentreId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}