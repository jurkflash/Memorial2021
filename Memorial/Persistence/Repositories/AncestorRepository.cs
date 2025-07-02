using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class AncestorRepository : Repository<Ancestor>, IAncestorRepository
    {
        public AncestorRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<Ancestor> GetByAncestorArea(int ancestorAreaId)
        {
            return MemorialContext.Ancestors
                .Where(a => a.AncestorAreaId == ancestorAreaId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}