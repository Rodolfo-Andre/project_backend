using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Dto;
using project_backend.Enums;
using project_backend.Interfaces;
using project_backend.Models;

namespace project_backend.Services
{
    public class TableService : ITableRestaurant
    {
        private readonly CommandsContext _context;

        public TableService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTable(TableRestaurant table)
        {
            bool result = false;

            try
            {
                _context.TableRestaurant.Add(table);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<bool> DeleteTable(TableRestaurant table)
        {
            bool result = false;

            try
            {
                _context.TableRestaurant.Remove(table);
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<TableRestaurant> GetById(int id)
        {
            var table = await _context.TableRestaurant.FirstOrDefaultAsync(t => t.NumTable == id);

            return table;
        }

        public async Task<List<TableRestaurant>> GetAll()
        {
            var tables = await _context.TableRestaurant.ToListAsync();

            return tables;
        }

        public async Task<bool> UpdateTable(TableRestaurant tableUpdate)
        {
            bool result = false;

            try
            {
                _context.Entry(tableUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        public async Task<int> GetNumberCommandsInTable(int idTable)
        {
            var table = await _context.TableRestaurant
            .Include(c => c.Commands)
            .Where(c => c.NumTable == idTable)
            .FirstOrDefaultAsync();

            return table.Commands.Count;
        }


        public async Task<List<TableComands>> getTablesWithCommands()
        {

            List<TableComands> response = new List<TableComands>();

            List<TableRestaurant> tables = await _context.TableRestaurant.
            Include(c => c.Commands).ThenInclude(c => c.Employee)
           .Include(c => c.Commands).ThenInclude(c => c.StatesCommand)
            .ToListAsync();


            foreach (var table in tables)
            {
                TableComands tableComands = new TableComands();

                tableComands.NumTable = table.NumTable;
                tableComands.NumSeats = table.NumSeats;
                tableComands.StateTable = table.StateTable;


                if (table.Commands.Any() && table.Commands != null)
                {

                    tableComands.hasCommands = true;

                    List<CommandsCustom> commands = new List<CommandsCustom>();

                    foreach (var command in table.Commands)
                    {
                        CommandsCustom commandsCustom = new CommandsCustom();

                        commandsCustom.Id = command.Id;
                        commandsCustom.CantSeats = command.CantSeats;
                        commandsCustom.PrecTotOrder = command.PrecTotOrder;
                        commandsCustom.CreatedAt = command.CreatedAt.ToString("dd/MM/yyyy");
                        commandsCustom.EmployeeId = command.EmployeeId;
                        commandsCustom.EmployeeName = command.Employee.FirstName + " " + command.Employee.LastName;
                        commandsCustom.StatesCommandId = command.StatesCommandId;
                        commandsCustom.StatesCommandName = command.StatesCommand.State;


                        List<DetailsComand> detailsComands = _context.DetailsComands.
                        Include(c => c.Dish).ThenInclude(c => c.CategoryDish).
                        Where(c => c.CommandsId == command.Id).ToList();

                        List<DetailCommandCustom> details = new List<DetailCommandCustom>();
                        if (detailsComands.Any() && detailsComands != null)
                        {
                            foreach (var detail in detailsComands)
                            {
                                DetailCommandCustom detailsComandCustom = new DetailCommandCustom();
                                detailsComandCustom.Id = detail.Id;
                                detailsComandCustom.CantDish = detail.CantDish;
                                detailsComandCustom.PrecDish = detail.PrecDish;
                                detailsComandCustom.Dish = new DishCustom()
                                {
                                    Id = detail.Dish.Id,
                                    CategoryDishId = detail.Dish.CategoryDishId,
                                    CategoryDishName = detail.Dish.CategoryDish.NameCatDish,
                                    ImgDish = detail.Dish.ImgDish,
                                    NameDish = detail.Dish.NameDish,
                                    PriceDish = detail.Dish.PriceDish
                                };

                                detailsComandCustom.Observation = detail.Observation;
                                detailsComandCustom.PrecOrder = detail.PrecOrder;

                                details.Add(detailsComandCustom);
                            }


                            commandsCustom.DetailsComand = details;

                        }
                        else
                        {
                            commandsCustom.DetailsComand = new List<DetailCommandCustom> { };
                        }


                        if (command.StatesCommand.Id != 3)
                        {
                            tableComands.commandActive = commandsCustom;
                        }


                       commands.Add(commandsCustom);

                    }

                    tableComands.Commands = commands;
                }
                else
                {
                    tableComands.hasCommands = false;

                    tableComands.Commands = new List<CommandsCustom> { };
                }

                response.Add(tableComands);

            }



            return response;


        }



        
        public async Task<TableComands> getTablesWithCommandsByTableId(int id)
        {

            TableComands response = new TableComands();

             TableRestaurant table = await _context.TableRestaurant.
             Include(c => c.Commands).ThenInclude(c => c.Employee)
            .Include(c => c.Commands).ThenInclude(c => c.StatesCommand)
            .Where(c => c.NumTable == id) .FirstOrDefaultAsync();


            response.NumTable = table.NumTable;
            response.NumSeats = table.NumSeats;
            response.StateTable = table.StateTable;

           




            return response;


        }


    }
}
