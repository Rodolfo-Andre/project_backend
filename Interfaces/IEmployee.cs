using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IEmployee
    {
        public Task<List<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task<bool> CreateEmployee(Employee employee);
        public Task<bool> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(Employee employee);
    }
}
