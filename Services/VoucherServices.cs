using Mapster;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Dto;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class VoucherServices : IVoucher
    {
        private readonly CommandsContext _context;

        public VoucherServices(CommandsContext context)
        {
            _context = context;
        }

        public async Task<List<Voucher>> GetAll()
        {
            return await _context.Voucher
                .Include(x => x.Employee)
                .Include(x => x.Cash)
                .ThenInclude(x => x.Establishment)
                .Include(x => x.Customer)
                .Include(x => x.VoucherType)
                .Include(x => x.VoucherDetails)
                .ThenInclude(x => x.PayMethod)
                .Include(x => x.Commands.Employee)
                .Include(x => x.Commands)
                .ThenInclude(x => x.DetailsComand)
                .ThenInclude(x => x.Dish)
                .ToListAsync();
        }

        public async Task<Voucher> GetById(int id)
        {
            Voucher voucher = await _context.Voucher
                .Include(x => x.Employee)
                .Include(x => x.Cash)
                .ThenInclude(x => x.Establishment)
                .Include(x => x.Customer)
                .Include(x => x.VoucherType)
                .Include(x => x.VoucherDetails)
                .ThenInclude(x => x.PayMethod)
                .Include(x => x.Commands.Employee)
                .Include(x => x.Commands)
                .ThenInclude(x => x.DetailsComand)
                .ThenInclude(x => x.Dish)
                .FirstOrDefaultAsync(x => x.Id == id);

            return voucher;
        }

        public async Task<bool> CreateVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Voucher.Add(voucher);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Entry(voucher).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteVoucher(Voucher voucher)
        {
            bool result = false;

            try
            {
                _context.Voucher.Remove(voucher);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<List<SalesDataPerDate>> GetSalesDataPerDate()
        {
            var query = from v in _context.Voucher
                        join c in _context.Commands on v.CommandsId equals c.Id
                        join dc in _context.DetailsComands on c.Id equals dc.CommandsId
                        join d in _context.Dish on dc.DishId equals d.Id
                        group new { v, dc } by v.DateIssued into g
                        orderby g.Sum(x => x.v.TotalPrice) descending
                        select new SalesDataPerDate
                        {
                            DateIssued = g.Key,
                            AccumulatedSales = g.Sum(x => x.v.TotalPrice),
                            NumberOfGeneratedReceipts = g.Count(),
                            QuantityOfDishSales = g.Sum(x => x.dc.CantDish),
                            BestSellingDish = (from v2 in _context.Voucher
                                               join c2 in _context.Commands on v2.CommandsId equals c2.Id
                                               join dc2 in _context.DetailsComands on c2.Id equals dc2.CommandsId
                                               join d2 in _context.Dish on dc2.DishId equals d2.Id
                                               where v2.DateIssued == g.Key
                                               group dc2 by new { dc2.DishId, d2.NameDish } into g2
                                               orderby g2.Sum(dc2 => dc2.CantDish) descending
                                               select g2.Key.NameDish).FirstOrDefault()
                        };

            var result = await query.ToListAsync();
            return result;
        }
    }
}
