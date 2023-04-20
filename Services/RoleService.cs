﻿using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class RoleService : IRole
    {
        private readonly CommandsContext _context;

        public RoleService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRole(Role role)
        {
            bool result = false;

            try
            {
                _context.Role.Add(role);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteRole(Role role)
        {
            bool result = false;

            try
            {
                _context.Role.Remove(role);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            var role = await _context.Role.FirstOrDefaultAsync(x => x.Id == id);

            return role;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            bool result = false;

            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }
    }
}
