using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IRole
    {
        public Task<IEnumerable<Role>> GetAll();
    }
}
