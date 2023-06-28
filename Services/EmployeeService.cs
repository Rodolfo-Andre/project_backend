﻿using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Utils;

namespace project_backend.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly CommandsContext _context;
        private readonly IEmail _emailService;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(CommandsContext context, ILogger<EmployeeService> logger, IEmail emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            bool result = false;

            try
            {
                var passwordGenerated = User.GeneratePassword(employee);

                _logger.LogInformation($"La contraseña generada es: {passwordGenerated}");

                employee.User.Password = SecurityUtils.HashPassword(passwordGenerated);
                _context.Add(employee);

                await _context.SaveChangesAsync();

                _emailService.SendEmail(employee.User.Email, "Bienvenido al sistema de comandas", $"Tu contraseña para acceder a nuestra plataforma es: {passwordGenerated}");

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            bool result = false;

            try
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<Employee>> GetAll()
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
            bool result = false;

            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<int> GetNumberCommandsInEmployee(int idEmployee)
        {
            var employee = await _context.Employee
               .Include(c => c.Commands)
               .Where(c => c.Id == idEmployee)
               .FirstOrDefaultAsync();

            return employee.Commands.Count;
        }

        public async Task<bool> IsEmailUnique(string email, int? idEmployee = null)
        {
            if (idEmployee != null)
            {
                return await _context.Employee.AllAsync(e => e.User.Email != email || e.Id == idEmployee);
            }

            return await _context.Employee.AllAsync(e => e.User.Email != email);
        }

        public async Task<bool> IsDniUnique(string dni, int? idEmployee = null)
        {
            if (idEmployee != null)
            {
                return await _context.Employee.AllAsync(e => e.Dni != dni || e.Id == idEmployee);
            }

            return await _context.Employee.AllAsync(e => e.Dni != dni);
        }

        public async Task<bool> IsPhoneUnique(string phone, int? idEmployee = null)
        {
            if (idEmployee != null)
            {
                return await _context.Employee.AllAsync(e => e.Phone != phone || e.Id == idEmployee);
            }

            return await _context.Employee.AllAsync(e => e.Phone != phone);
        }
    }
}
