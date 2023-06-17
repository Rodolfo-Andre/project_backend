using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Dto;
using project_backend.Dto.inputs;
using project_backend.Schemas;
using System.Security.Claims;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommands _commandService;
        private readonly IDetailsCommand _detailsService;
        private readonly ITableRestaurant _tableService;
        private readonly IDish _dishService;
        //Agregar lo de platos y usuarios

        public CommandController(ICommands commandService, IDetailsCommand detailsService, ITableRestaurant tableService, IDish dishService)
        {
            _commandService = commandService;
            _detailsService = detailsService;
            _tableService = tableService;
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandGet>>> GetCommand()
        {
            List<CommandGet> commands = (await _commandService.GetAll()).Adapt<List<CommandGet>>();

            return Ok(commands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandGet>> GetCommand(int id)
        {
            var command = await _commandService.GetById(id);

            if (command == null)
            {
                return NotFound("Comanda no encontrada");
            }

            CommandGet commandGet = command.Adapt<CommandGet>();

            return Ok(commandGet);
        }


        [HttpPost]
        public async Task<ActionResult> CreateCommand ([FromBody ] CommandInput input){
            try
            {

                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

               bool result = await _commandService.CreateCommand(input);

                if(!result){
                    return BadRequest("Error al crear la comanda");
                }

                return Ok("Comanda creada con exito");


                
            }
            catch (System.Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }
       
       
       
        // [HttpPost]
        // public async Task<ActionResult<Commands>> CreateCommand([FromBody] CommandCreate command)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     //Validar mesa
        //     var table = await _tableService.GetById(command.TableRestaurantId);

        //     if (table == null)
        //     {
        //         return NotFound("Mesa no encontrada");
        //     }

        //     if (table.StateTable == "Ocupado")
        //     {
        //         return BadRequest("Mesa ocupada, eliga otra");
        //     }

        //     //Creando comanda
        //     var newCommand = command.Adapt<Commands>();

        //     List<DetailsComand> details = new();

        //     foreach (var item in command.ListDetails)
        //     {
        //         var dish = await _dishService.GetById(item.DishId);

        //         DishGet dishGet = dish.Adapt<DishGet>();

        //         if (dish == null)
        //         {
        //             return NotFound($"El plato {item.DishId} no existe");
        //         }

        //         newCommand.PrecTotOrder += item.CantDish * dishGet.PriceDish;

        //         DetailsComand newDetail = new()
        //         {
        //             CantDish = item.CantDish,
        //             PrecDish = dish.PriceDish,
        //             PrecOrder = item.CantDish * dishGet.PriceDish,
        //             Observation = item.Observation,
        //             DishId = item.DishId
        //         };

        //         details.Add(newDetail);
        //     }

        //     var identity = User.Identity as ClaimsIdentity;
        //     var id = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

        //     //Valores adicionales
        //     newCommand.StatesCommandId = 1;
        //     newCommand.CantSeats = command.CantSeats;

        //     newCommand.EmployeeId = id;
        //     await _commandService.CreateCommand(newCommand);
        //     var getCommand = (await _commandService.GetById(newCommand.Id)).Adapt<CommandGet>();

        //     //Actualizar mesa
        //     table.StateTable = "Ocupado";
        //     await _tableService.UpdateTable(table);

        //     //Agregamos detalles
        //     foreach (var item in details)
        //     {
        //         item.CommandsId = newCommand.Id;
        //         await _detailsService.CreateDetailCommand(item);
        //     }

        //     return CreatedAtAction(nameof(GetCommand), new { id = getCommand.Id }, getCommand);
        // }

        [HttpPut("{id}")]
        public async Task<ActionResult<Commands>> UpdateCommand(int id, [FromBody] CommandPrincipal value)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var updateCommand = await _commandService.GetById(id);
            if (updateCommand == null) { return NotFound("Comanda no encontrada"); }

            if (updateCommand.TableRestaurantId != value.TableRestaurantId)
            {
                //Validar mesa
                var newTable = await _tableService.GetById(value.TableRestaurantId);
                if (newTable == null)
                {
                    return BadRequest("No existe la mesa");
                }

                if (newTable.StateTable == "Ocupado")
                {
                    return BadRequest("Mesa ocupada, eliga otra");
                }
                newTable.StateTable = "Ocupado";
                await _tableService.UpdateTable(newTable);
                var tableOld = await _tableService.GetById(updateCommand.TableRestaurantId);
                tableOld.StateTable = "Libre";
                await _tableService.UpdateTable(tableOld);

            }

            updateCommand.TableRestaurantId = value.TableRestaurantId;
            updateCommand.CantSeats = value.CantSeats;
            await _commandService.UpdateCommand(updateCommand);
            var getCommand = await _commandService.GetById(id);
            return Ok(getCommand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(int id)
        {
            // var command = await _commandService.GetById(id);

            // if (command == null) { return NotFound("Comanda no encontrada"); }
            // if (command.StatesCommandId == 2) { return BadRequest("La comanda ya fue facturada, no es posible eliminar"); }
            // //Eliminar detalles primero
            // List<DetailsComand> details = await _detailsService.GetByCommandId(command.Id);
            // foreach (var item in details)
            // {
            //     await _detailsService.DeleteDetailCommand(item);
            // }
            // //Actualizar mesa
            // var tableUpdate = await _tableService.GetById(command.TableRestaurantId);
            // tableUpdate.StateTable = "Libre";
            // await _tableService.UpdateTable(tableUpdate);

            await _commandService.DeleteCommand(id);
            return NoContent();
        }
    

        [HttpGet("getCommandByTableId/{id}")]
        public async Task<ActionResult<GetCommandWithTable>> getCommandByTableId(int id)
        {
            var command = await _commandService.getCommandByTableId(id);

            if (command == null)
            {
                return NotFound("Comanda no encontrada");
            }

            return Ok(command);
        }


        
    }
}
