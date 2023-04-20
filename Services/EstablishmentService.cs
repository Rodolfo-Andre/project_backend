﻿using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class EstablishmentService : IEstablishment
    {
        private readonly CommandsContext _context;

        public EstablishmentService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEstablishment(Establishment establishment)
        {
            bool result = false;

            try
            {
                _context.Establishment.Add(establishment);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteEstablishment(Establishment establishment)
        {
            bool result = false;

            try
            {
                _context.Establishment.Remove(establishment);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<Establishment>> GetAll()
        {
            var establishments = await _context.Establishment.ToListAsync();

            return establishments;
        }

        public async Task<Establishment> GetById(int id)
        {
            var establishment = await _context.Establishment.FirstOrDefaultAsync(x => x.Id == id);

            return establishment;
        }

        public async Task<Establishment> GetFirstOrDefault()
        {
            var establishment = await _context.Establishment.FirstOrDefaultAsync();

            return establishment;
        }

        public async Task<bool> UpdateEstablishment(Establishment establishment)
        {
            bool result = false;

            try
            {
                _context.Entry(establishment).State = EntityState.Modified;
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
