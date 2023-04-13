
using Microsoft.EntityFrameworkCore;
using project_backend.Models;

namespace project_backend.Data
{
    public class CommandsContext : DbContext
    {
        public CommandsContext(DbContextOptions<CommandsContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<Aperture> Aperture { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<CategoryDish> CategoryDish { get; set; } = default!;
        public DbSet<TableRestaurant> TableRestaurant { get; set; } = default!;
        public DbSet<StatesCommand> StatesCommand { get; set; } = default!;
        public DbSet<Commands> Commands { get; set; } = default!;
        public DbSet<DetailsComand> DetailsComands { get; set; } = default!;
        public DbSet<Establishment> Establishment { get; set; } = default!;
        public DbSet<Cash> Cash { get; set; } = default!;
        public DbSet<PayMethod> PayMethods { get; set; } = default!;
        public DbSet<VoucherType> VoucherType { get; set; } = default!;
        public DbSet<VoucherDetail> VoucherDetail { get; set; } = default!;
    }
}
