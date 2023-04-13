using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class RoleService : IRole
    {
        private readonly CommandsContext _commandsContext;

        public RoleService(CommandsContext commandsContext)
        {
            _commandsContext = commandsContext;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _commandsContext.Role.ToListAsync();
        }
    }
}
