using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IDetailsCommand
    {
        Task<List<DetailsComand>> getAllDetailsCommands();
        Task<List<DetailsComand>> GetDetailsComandsByNumCommand(int idCommand);
        Task<DetailsComand> GetDetailsCommand(int idCommand, string idDish);
        Task<bool> createDetailCommand(DetailsComand dc);
        Task<bool> deleteDetailCommand(DetailsComand dc);
        Task<bool> updateDetailCommand(DetailsComand dc);
    }
}
