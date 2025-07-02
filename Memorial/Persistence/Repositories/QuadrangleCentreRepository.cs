using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Memorial.Persistence.Repositories
{
    public class QuadrangleCentreRepository : Repository<QuadrangleCentre>, IQuadrangleCentreRepository
    {
        public QuadrangleCentreRepository(MemorialContext context) : base(context)
        {
        }

        public IEnumerable<QuadrangleCentre> GetBySite(byte siteId)
        {
            return MemorialContext.QuadrangleCentres
                .Where(qc => qc.SiteId == siteId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}