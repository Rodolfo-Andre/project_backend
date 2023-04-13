using Microsoft.EntityFrameworkCore;
using project_backend.Controllers;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Utils;

namespace project_backend.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly CommandsContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(CommandsContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            bool isOk = false;

            try
            {
                var passwordGenerated = User.GeneratePassword(employee);

                _logger.LogInformation(passwordGenerated);

                employee.User.Password = SecurityUtils.HashPassword(passwordGenerated);
                _context.Add(employee);

                await _context.SaveChangesAsync();

                isOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return isOk;
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            bool isOk = false;

            try
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();

                isOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return isOk;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            List<Employee> employees = await _context.Employee
                .Include(e => e.Role)
                .Include(e => e.User)
                .ToListAsync();

            return employees;
        }

        public async Task<Employee> GetById(int id)
        {
            Employee employee = await _context.Employee
                 .Include(e => e.Role)
                 .Include(e => e.User)
                 .FirstOrDefaultAsync(e => e.Id == id);

            return employee;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            bool isOk = false;

            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                isOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return isOk;
        }
    }
}
