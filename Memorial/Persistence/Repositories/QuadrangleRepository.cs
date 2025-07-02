using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleRepository : Repository<Quadrangle>, IQuadrangleRepository
    {
        public QuadrangleRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Quadrangle> GetByArea(int quadrangleAreaId)
        {
            return MemorialContext.Quadrangles
                .Where(q => q.QuadrangleAreaId == quadrangleAreaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}