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
                        select new
                        {
                            DateIssued = g.Key,
                            AccumulatedSales = g.Sum(x => x.v.TotalPrice),
                            NumberOfGeneratedReceipts = g.Count(),
                            QuantityOfDishSales = g.Sum(x => x.dc.CantDish)
                        };

            var maxQuantityPerDate = from v in _context.Voucher
                                     join c in _context.Commands on v.CommandsId equals c.Id
                                     join dc in _context.DetailsComands on c.Id equals dc.CommandsId
                                     group dc.CantDish by v.DateIssued into g
                                     select new
                                     {
                                         DateIssued = g.Key,
                                         MaxQuantity = g.Max()
                                     };

            var query2 = from v in _context.Voucher
                         join c in _context.Commands on v.CommandsId equals c.Id
                         join dc in _context.DetailsComands on c.Id equals dc.CommandsId
                         join d in _context.Dish on dc.DishId equals d.Id
                         join mq in maxQuantityPerDate on new { v.DateIssued, Quantity = dc.CantDish } equals new { mq.DateIssued, Quantity = mq.MaxQuantity }
                         select new
                         {
                             v.DateIssued,
                             d.NameDish
                         };

            var result = await query.Select(x => new SalesDataPerDate
            {
                DateIssued = x.DateIssued,
                AccumulatedSales = x.AccumulatedSales,
                NumberOfGeneratedReceipts = x.NumberOfGeneratedReceipts,
                QuantityOfDishSales = x.QuantityOfDishSales,
                BestSellingDish = query2.First(q => q.DateIssued == x.DateIssued).NameDish
            }).ToListAsync();

            return result;
        }
    }
}
