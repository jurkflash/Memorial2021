using Memorial.Core.Domain;

namespace Memorial.Core.Repositories
{
    public interface INumberRepository
    {
        int GetCremationNewAF(int itemId, int year);

        int GetCremationNewIV(int itemId, int year);

        int GetCremationNewRE(int itemId, int year);

        int GetMiscellaneousNewAF(int itemId, int year);

        int GetMiscellaneousNewIV(int itemId, int year);

        int GetMiscellaneousNewRE(int itemId, int year);
    }
}
