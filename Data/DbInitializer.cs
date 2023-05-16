using project_backend.Models;
using project_backend.Utils;

namespace project_backend.Data
{
    public class DbInitializer
    {
        public async static void Initialize(CommandsContext context)
        {
            if (!context.CategoryDish.Any())
            {
                await context.CategoryDish.AddRangeAsync(new List<CategoryDish>
                {
                    new CategoryDish() { Id = "C-001", NameCatDish = "Bebidas" },
                    new CategoryDish() { Id = "C-002", NameCatDish = "Hamburguesas" },
                    new CategoryDish() { Id = "C-003", NameCatDish = "Postres" },
                    new CategoryDish() { Id = "C-004", NameCatDish = "Sopas" }
                });

                context.SaveChanges();
            }

            if (!context.Establishment.Any())
            {
                await context.Establishment.AddRangeAsync(new List<Establishment>
                {
                    new Establishment() {
                        Name = "Sangunchería Wong",
                        Address = "Av.Izaguirre",
                        Phone = "942850902",
                        Ruc = "20509311412"
                    }
                });

                context.SaveChanges();
            }

            if (!context.PayMethods.Any())
            {
                await context.PayMethods.AddRangeAsync(new List<PayMethod>
                {
                    new PayMethod() { Paymethod = "Pago en efectivo" },
                    new PayMethod() { Paymethod = "BCP" },
                    new PayMethod() { Paymethod = "BBVA" },
                    new PayMethod() { Paymethod = "Scotiabank" },
                    new PayMethod() { Paymethod = "Interbank" },
                });

                context.SaveChanges();
            }

            if (!context.Role.Any())
            {
                await context.Role.AddRangeAsync(new List<Role>
                {
                    new Role() {RoleName = "Administrador"},
                    new Role() {RoleName = "Mesero"},
                    new Role() {RoleName = "Cajero"},
                    new Role() {RoleName = "Cocinero"},
                    new Role() {RoleName = "Gerente"},
                });

                context.SaveChanges();
            }

            if (!context.VoucherType.Any())
            {
                await context.VoucherType.AddRangeAsync(new List<VoucherType>
                {
                    new VoucherType() {Name = "Boleta"},
                    new VoucherType() {Name = "Factura"}
                });

                context.SaveChanges();
            }

            if (!context.Employee.Any())
            {
                await context.Employee.AddRangeAsync(new List<Employee>
                {
                    new Employee()
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Phone = "999999999",
                        RoleId = 1,
                        User = new User()
                        {
                            Email = "admin@admin.com",
                            Password = SecurityUtils.HashPassword("admin")
                        }
                    },
                    new Employee()
                    {
                        FirstName = "Mesero",
                        LastName = "Mesero",
                        Phone = "888888888",
                        RoleId = 2,
                        User = new User()
                        {
                            Email = "mesero@mesero.com",
                            Password = SecurityUtils.HashPassword("mesero")
                        }
                    },
                    new Employee()
                    {
                        FirstName = "Cajero",
                        LastName = "Cajero",
                        Phone = "777777777",
                        RoleId = 3,
                        User = new User()
                        {
                            Email = "cajero@cajero.com",
                            Password = SecurityUtils.HashPassword("cajero")
                        }
                    },
                    new Employee()
                    {
                        FirstName = "Cocinero",
                        LastName = "Cocinero",
                        Phone = "666666666",
                        RoleId = 4,
                        User = new User()
                        {
                            Email = "cocinero@cocinero.com",
                            Password = SecurityUtils.HashPassword("cocinero")
                        }
                    },
                    new Employee()
                    {
                        FirstName = "Gerente",
                        LastName = "Gerente",
                        Phone = "555555555",
                        RoleId = 5,
                        User = new User()
                        {
                            Email = "gerente@gerente.com",
                            Password = SecurityUtils.HashPassword("gerente")
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
