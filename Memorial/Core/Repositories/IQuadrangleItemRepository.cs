using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleItemRepository : IRepository<QuadrangleItem>
    {
        IEnumerable<QuadrangleItem> GetByCentre(int quadrangleCentreId);
    }
}
