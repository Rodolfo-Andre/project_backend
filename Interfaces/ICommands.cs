using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface ICommands
    {
        public Task<List<Commands>> GetAll();
        public Task<Commands> GetById(int id);
        public Task<bool> CreateCommand(Commands command);
        public Task<bool> DeleteCommand(Commands command);
        public Task<bool> UpdateCommand(Commands command);
    }
}
