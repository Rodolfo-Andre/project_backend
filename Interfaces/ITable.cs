using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ITableRestaurant
    {
        public Task<List<TableRestaurant>> GetAll();
        public Task<TableRestaurant> GetById(int id);
        public Task<bool> CreateTable(TableRestaurant table);
        public Task<bool> UpdateTable(TableRestaurant table);
        public Task<bool> DeleteTable(TableRestaurant table);
    }
}
