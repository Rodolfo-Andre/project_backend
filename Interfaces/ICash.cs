using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface ICash
    {

        Task<List<CashSchema>> getAllCash();
        Task<Cash> getCash(int id);
        Task<bool> createCash(Cash cash);
        Task<bool> updateCash(Cash cash);
        Task<bool> deleteCash(Cash cash);

    }
}
