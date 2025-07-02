using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface IQuadrangleRepository : IRepository<Quadrangle>
    {
        IEnumerable<Quadrangle> GetByArea(int quadrangleAreaId);
    }
}
