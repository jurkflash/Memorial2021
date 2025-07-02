using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleCentreRepository : IRepository<QuadrangleCentre>
    {
        IEnumerable<QuadrangleCentre> GetBySite(byte siteId);
    }
}
