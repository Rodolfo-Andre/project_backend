using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ICash
    {
        public Task<List<Cash>> GetAll();
        public Task<Cash> GetById(int id);
        public Task<bool> CreateCash(Cash cash);
        public Task<bool> UpdateCash(Cash cash);
        public Task<bool> DeleteCash(Cash cash);
        public Task<int> GetNumberVouchersInCash(int idCash);

    }
}
