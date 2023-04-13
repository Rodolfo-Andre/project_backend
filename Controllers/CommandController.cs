using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_backend.Controllers
{
    [Route("api/command")]
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


        // GET: api/<CommandController>
        [HttpGet]
        public async Task<IEnumerable<CommandGet>> GetCommands()
        {
            List<CommandGet> commands = (await _commandService.getCommands()).Adapt<List<CommandGet>>();
           
            return commands;
        }

        // GET api/<CommandController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandGet>> GetCommand(int id)
        {
            var command = await _commandService.getComand(id);
            if(command == null) { return NotFound(); }
            CommandGet commandGet = command.Adapt<CommandGet>();

            return commandGet;
        }

        // POST api/<CommandController>
        [HttpPost]
        public async Task<ActionResult<Commands>> PostCommand([FromBody] CommandCreate command)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            //Validar mesa
            var table = await _tableService.GetTableById(command.TableRestaurantId);
            if (table == null)
            {
                return BadRequest("No existe la mesa");
            }

            if(table.StateTable == "Ocupado")
            {
                return BadRequest("Mesa ocupada, eliga otra");
            }
            //Creando comanda
            var newCommand = command.Adapt<Commands>();

            

            List<DetailsComand> detalles = new List<DetailsComand>();
            foreach (var item in command.ListDetails)
            {
                var dish = await _dishService.GetDish(item.Dish.Id);
                DishGet dishGet = dish.Adapt<DishGet>();
                if(dish == null)
                {
                    return NotFound($"El plato {item.Dish.Id} no existe");
                }
                newCommand.PrecTotOrder += item.cantDish * dishGet.PriceDish;
                DetailsComand newDetail = new()
                {
                    CantDish = item.cantDish,
                    PrecDish = dish.PriceDish,
                    PrecOrder = item.cantDish * dishGet.PriceDish,
                    Observation = item.Observation,
                    DishId = item.Dish.Id
                };
                detalles.Add(newDetail);
            }
            //Valores adicionales
            newCommand.StatesCommandId = 1;

            //Rodolfo aquí me carreas pls 
            newCommand.UserId = 1;


            await _commandService.createCommand(newCommand);
            var getCommand = await _commandService.getComand(newCommand.Id);
            //Actualizar mesa
            table.StateTable = "Ocupado";
            await _tableService.UpdateStateTable(table);

            //Agregamos detalles
            foreach(var item in detalles)
            {
                item.CommandsId = newCommand.Id;
                await _detailsService.createDetailCommand(item);
            }

            return StatusCode(201, getCommand);
        }

        // PUT api/<CommandController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCommand(int id, [FromBody] CommandSchema value)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var updateCommand = await _commandService.getComand(id);
            if(updateCommand == null) { return NotFound(); }

            if (updateCommand.TableRestaurantId != value.TableRestaurantId)
            {
                //Validar mesa
                var newTable = await _tableService.GetTableById(value.TableRestaurantId);
                if (newTable == null)
                {
                    return BadRequest("No existe la mesa");
                }

                if (newTable.StateTable == "Ocupado")
                {
                    return BadRequest("Mesa ocupada, eliga otra");
                }
                newTable.StateTable = "Ocupado";
                await _tableService.UpdateStateTable(newTable);
                var tableOld = await _tableService.GetTableById(updateCommand.TableRestaurantId);
                tableOld.StateTable = "Libre";
                await _tableService.UpdateStateTable(tableOld);

            }

            updateCommand.TableRestaurantId = value.TableRestaurantId;
            updateCommand.CantSeats = value.CantSeats;
            await _commandService.updateCommand(updateCommand);
            var getCommand = await _commandService.getComand(id);
            return StatusCode(200, getCommand);

        }

        // DELETE api/<CommandController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(int id)
        {
            var command = await _commandService.getComand(id);
            if(command == null) { return NotFound(); }
            if(command.StatesCommandId == 2) { return BadRequest("La comanda ya fue facturada, no es posible eliminar"); }
            //Eliminar detalles primero
            List<DetailsComand> details = await _detailsService.GetDetailsComandsByNumCommand(command.Id);
            foreach (var item in details)
            {
                await _detailsService.deleteDetailCommand(item);
            }
            //Actualizar mesa
            var tableUpdate = await _tableService.GetTableById(command.TableRestaurantId);
            tableUpdate.StateTable = "Libre";
            await _tableService.UpdateStateTable(tableUpdate);

            await _commandService.deleteCommand(command);
            return NoContent();

        }
    }
}
