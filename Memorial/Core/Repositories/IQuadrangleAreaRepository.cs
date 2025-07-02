using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleAreaRepository : IRepository<QuadrangleArea>
    {
        IEnumerable<QuadrangleArea> GetByCentre(int quadrangleCentreId);
    }
}
