using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Dto;
using project_backend.Enums;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Dto.inputs;

namespace project_backend.Services
{
    public class CommandService : ICommands
    {
        private readonly CommandsContext _context;

        public CommandService(CommandsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCommand(CommandInput input)
        {
            try
            {
                if (input.id != 0)
                {
                    Commands command = await _context.Commands
                   .Include(c => c.TableRestaurant)
                   .Include(c => c.Employee)
                   .Include(c => c.StatesCommand)
                   .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                   .FirstOrDefaultAsync(c => c.Id == input.id);

                    if (command.StatesCommandId.Equals(3))
                    {
                        return false;
                    }



                    if (command is null)
                    {
                        return false;
                    }


                    TableRestaurant tableRestaurant = _context.TableRestaurant.FirstOrDefault(t => t.NumTable == input.numTable);
                    if (tableRestaurant == null)
                    {
                        return false;
                    }

                    Employee employee = _context.Employee.FirstOrDefault(e => e.Id == input.employeeId);
                    if (employee == null)
                    {
                        return false;
                    }

                    command.CantSeats = input.cantPerson;
                    command.StatesCommandId = (int)TypeCommandState.Generated;
                    command.TableRestaurantId = tableRestaurant.NumTable;
                    command.EmployeeId = employee.Id;
                    command.CreatedAt = DateTime.Now;

                    //command.TableRestaurant.NumTable = input.numTable;
                    //command.TableRestaurant.NumSeats = input.cantPerson;
                    //command.TableRestaurant.StateTable = "Ocupado";

                    List<DetailsComand> details = new List<DetailsComand>();

                    command.DetailsComand.Clear();

                    foreach (var item in input.listDish)
                    {
                        DetailsComand detail = new DetailsComand();
                        Dish dish = await _context.Dish.FirstOrDefaultAsync(d => d.Id == item.dishId);
                        if (dish == null) continue;

                        detail.DishId = dish.Id;
                        detail.CantDish = item.quantity;
                        detail.Observation = item.observation;
                        detail.PrecDish = dish.PriceDish;
                        detail.CommandsId = command.Id;
                        detail.PrecOrder = detail.PrecDish * detail.CantDish;
                        details.Add(detail);
                        _context.DetailsComands.Add(detail);

                    }

                    command.PrecTotOrder = details.Sum(d => d.PrecOrder);

                    _context.Commands.Update(command);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    Commands commandActiveExist =  _context.Commands.FirstOrDefault(c => c.StatesCommandId == 1 && c.TableRestaurantId == input.numTable);

                    if (commandActiveExist != null)
                    {
                        return false;
                    }


                    TableRestaurant tableRestaurant = _context.TableRestaurant.FirstOrDefault(t => t.NumTable == input.numTable);
                    if (tableRestaurant == null)
                    {
                        return false;
                    }

                    Employee  employee = _context.Employee.FirstOrDefault(e => e.Id == input.employeeId);
                    if (employee == null)
                    {
                        return false;
                    }


                    Commands command = new Commands();
                    command.CantSeats = input.cantPerson;
                    command.EmployeeId = input.employeeId;
                    command.StatesCommandId = (int)TypeCommandState.Generated;
                    command.TableRestaurantId = tableRestaurant.NumTable;
                    command.EmployeeId = employee.Id;
                    command.CreatedAt = DateTime.Now;
                    tableRestaurant.StateTable = "Ocupado";
                    _context.Commands.Add(command);
                    await _context.SaveChangesAsync();

                    //command.TableRestaurant.NumTable = input.numTable;
                    //command.TableRestaurant.NumSeats = input.cantPerson;
                    //command.TableRestaurant.StateTable = "Ocupado";
                    List<DetailsComand> details = new List<DetailsComand>();

                    foreach (var item in input.listDish)
                    {
                        DetailsComand detail = new DetailsComand();
                        Dish dish = await _context.Dish.FirstOrDefaultAsync(d => d.Id == item.dishId);
                        if (dish == null) continue;

                        detail.DishId = dish.Id;
                        detail.CantDish = item.quantity;
                        detail.Observation = item.observation;
                        detail.PrecDish = dish.PriceDish;
                        detail.CommandsId = command.Id;
                        detail.PrecOrder = detail.PrecDish * detail.CantDish;

                        details.Add(detail);
                        _context.DetailsComands.Add(detail);
                    }

                    //command.DetailsComand = details;
                    command.PrecTotOrder = details.Sum(d => d.PrecOrder);
                    _context.Commands.Update(command);
                    _context.TableRestaurant.Update(tableRestaurant);
                    await _context.SaveChangesAsync();

                    return true;


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return false;
            }


        }

        public async Task<bool> DeleteCommand(int id)
        {
            bool result = false;

            try
            {

                

              
                Commands command = await _context.Commands
                    .Include(c => c.TableRestaurant)
                    .Include(c => c.Employee)
                    .Include(c => c.StatesCommand)
                    .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                    .FirstOrDefaultAsync(c => c.Id == id);


                if (command is null)

                {
                    return false;
                }

                if(command.StatesCommandId.Equals(2) || command.StatesCommandId.Equals(3))
                {
                    return false;
                }

                TableRestaurant tableRestaurant = await _context.TableRestaurant.FirstOrDefaultAsync(t => t.NumTable == command.TableRestaurantId);

                if (tableRestaurant is null)
                {
                    return false;
                }

                tableRestaurant.StateTable = "Libre";


                if(command.DetailsComand.Any()){
                    _context.DetailsComands.RemoveRange(command.DetailsComand);
                }

                _context.Commands.Remove(command);

                await _context.SaveChangesAsync();


                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }


         public async Task<bool> updateStateCommand (int id)
        {
            bool result = false;

            try
            {

                Commands commands = await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);

                if (commands is null)
                {
                    return false;
                }


                if (commands.StatesCommandId.Equals(2) || commands.StatesCommandId.Equals(3))
                {
                    return false;
                }

                commands.StatesCommandId = 2;
                _context.Commands.Update(commands);
                await _context.SaveChangesAsync();
                result = true;

            }
            catch (Exception)
            {
                result = false;
            }





            return result;
        }

        public async Task<Commands> GetById(int id)
        {
            var command = await _context.Commands
                .Include(c => c.TableRestaurant)
                .Include(c => c.Employee)
                .Include(c => c.StatesCommand)
                .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                .FirstOrDefaultAsync(c => c.Id == id);


            return command;
        }

        public async Task<List<Commands>> GetAll()
        {
            List<Commands> command = await _context.Commands
                .Include(c => c.TableRestaurant)
                .Include(c => c.Employee)
                .Include(c => c.StatesCommand)
                .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
                .ToListAsync();

            return command;
        }

        public async Task<bool> UpdateCommand(Commands command)
        {
            bool result = false;

            try
            {
                _context.Entry(command).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }


        public async Task<GetCommandWithTable> getCommandByTableId(int id)
        {
            GetCommandWithTable command = new GetCommandWithTable();

            Commands commandS = await _context.Commands
           .Include(c => c.TableRestaurant)
           .Include(c => c.Employee)
           .Include(c => c.StatesCommand)
           .Include(c => c.DetailsComand).ThenInclude(d => d.Dish).ThenInclude(ca => ca.CategoryDish)
           .FirstOrDefaultAsync(c => c.TableRestaurant.NumTable == id && c.StatesCommand.Id != 3 && c.TableRestaurant.StateTable.Equals("Ocupado"));

            if (commandS is null)
            {
                TableRestaurant  table = await _context.TableRestaurant.FirstOrDefaultAsync(t => t.NumTable == id && t.StateTable.Equals("Libre"));

                int idd =  _context.Commands.Any() ? _context.Commands.Max(c => c.Id) + 1 : 1;
                if (table != null)
                {
                    command = new GetCommandWithTable()
                    {
                        Id = 0,
                        CantSeats = 0,
                        CreatedAt = "",
                        EmployeeId = 0,
                        EmployeeName = "",
                        NumSeats = table.NumSeats,
                        NumTable = table.NumTable,
                        PrecTotOrder = 0,
                        StateTable = table.StateTable,
                        StatesCommandId = 0,
                        StatesCommandName = TypeCommandState.Generated.ToString(),
                        isCommandActive = false,
                        DetailsComand = new List<DetailCommandCustom>()
                    };

                    return command;
                }
                else
                {
                    return null;
                }

                
                 
            };

            command.Id = commandS.Id;
            command.NumTable = commandS.TableRestaurant.NumTable;
            command.NumSeats = commandS.TableRestaurant.NumSeats;
            command.CreatedAt = commandS.CreatedAt.ToString("dd/MM/yyyy");
            command.StatesCommandId = commandS.StatesCommand.Id;
            command.StatesCommandName = commandS.StatesCommand.State;
            command.CantSeats = commandS.CantSeats;
            command.EmployeeId = commandS.Employee.Id;
            command.EmployeeName = commandS.Employee.FirstName + " " + commandS.Employee.LastName;
            command.PrecTotOrder = commandS.PrecTotOrder;
            command.isCommandActive = true;
            command.StateTable = commandS.TableRestaurant.StateTable;


            if (commandS.DetailsComand.Any())
            {
                command.DetailsComand = commandS.DetailsComand.Select(d => new DetailCommandCustom
                {
                    Id = d.Id,
                    CantDish = d.CantDish,
                    PrecDish = d.PrecDish,
                    Observation = d.Observation,
                    Dish = new DishCustom
                    {
                        Id = d.Dish.Id,
                        ImgDish = d.Dish.ImgDish,
                        PriceDish = d.Dish.PriceDish,
                        CategoryDishId = d.Dish.CategoryDish.Id,
                        CategoryDishName = d.Dish.CategoryDish.NameCatDish,
                        NameDish = d.Dish.NameDish
                    },
                    PrecOrder = d.PrecOrder,

                }).ToList();
            }
            else
            {
                command.DetailsComand = new List<DetailCommandCustom>();
            }

            return command;

        }
    }
}
