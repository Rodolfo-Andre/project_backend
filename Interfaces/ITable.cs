using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ITableRestaurant
    {

        Task<List<TableRestaurant>> GetTables();
        Task<TableRestaurant> GetTableById(int id);

        Task<bool> createTable(TableRestaurant table);
        Task<bool> UpdateStateTable(TableRestaurant tableUpdate);
        Task<bool> DeleteStateTable(TableRestaurant table);

    }
}
