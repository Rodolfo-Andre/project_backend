using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ICommands
    {
        //Parte Gary
        Task<List<Commands>> getCommands();
        Task<Commands> getComand(int id);
        Task<bool> createCommand(Commands command);
        Task<bool> deleteCommand(Commands command);
        Task<bool> updateCommand(Commands command);
    }
}
