using project_backend.Models;
using project_backend.Dto;
using project_backend.Dto.inputs;
namespace project_backend.Interfaces
{
    public interface ICommands
    {
        public Task<List<Commands>> GetAll();
        public Task<Commands> GetById(int id);
        public Task<bool> CreateCommand(CommandInput command);
        public Task<bool> DeleteCommand(int id);
        public Task<bool> UpdateCommand(Commands command);
        public  Task<GetCommandWithTable> getCommandByTableId (int id);
    }
}
