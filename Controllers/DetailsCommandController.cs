using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_backend.Controllers
{
    [Route("api/detailscommand")]
    [ApiController]
    public class DetailsCommandController : ControllerBase
    {
        private readonly CommandsContext _context;
        private readonly ICommands _ICommand;
        private readonly IDetailsCommand _IDetailsCommand;

        public DetailsCommandController(CommandsContext context, ICommands ICommand, IDetailsCommand detailsCommand)
        {
            _context = context;
            _ICommand = ICommand;
            _IDetailsCommand = detailsCommand;
        }

        // GET: api/<DetailsCommandController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailsComand>>> GetDetails()
        {
            List<DetailsComand> details = await _IDetailsCommand.getAllDetailsCommands();
            return details;
        }

        // GET api/<DetailsCommandController>/5
        [HttpGet("{idDish}/{idCommand}")]
        public async Task<ActionResult<DetailCommandGET>> GetDetail(string idDish, int idCommand)
        {
            var detail = await _IDetailsCommand.GetDetailsCommand(idCommand, idDish);
            if(detail == null) { return NotFound(); }
            DetailCommandGET detailGet = detail.Adapt<DetailCommandGET>();  
            return detailGet;
        }

        // POST api/<DetailsCommandController>
        [HttpPost]
        public async Task<ActionResult<DetailCommandGET>> PostDetail([FromBody] DetailCommandCreate detail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Validar que exista la comanda
            var command = await _ICommand.getComand(detail.IdCommand);
            if(command == null) 
            {
                return BadRequest("No existe la comanda");
            }
            if(command.StatesCommandId == 2)
            {
                return BadRequest("No puedes actualizar una comanda ya pagada");
            }
            //AQUÍ TENGO QUE VALIDAR LOS PLATOS <- Que no se repitan los mismos platos, solo que aumente su cantidad. PERO FALTA LO DE JORE
            List<DetailsComand> listValid = await _IDetailsCommand.GetDetailsComandsByNumCommand(command.Id);
            foreach(var detailsComand in listValid)
            {
                if(detailsComand.DishId == detail.Dish.Id)
                {
                    return BadRequest("El plato ya está solicitado, tendrías que actualizar la cantidad");
                }
            }



            var newDetail = detail.Adapt<DetailsComand>();
            await _IDetailsCommand.createDetailCommand(newDetail);
            var getDetail = await _IDetailsCommand.GetDetailsCommand(newDetail.CommandsId, newDetail.DishId);
            return StatusCode(201, getDetail);
        }

        // PUT api/<DetailsCommandController>/5
        [HttpPut("{idDish}/{idCommand}")]
        public async Task<IActionResult> EditDetailCommand(string idDish, int idCommand, [FromBody] DetailCommandEdit value)
        {
            //Validar que exista la comanda
            var command = await _ICommand.getComand(idCommand);
            if (command == null)
            {
                return BadRequest("No existe la comanda");
            }
            if (command.StatesCommandId == 2)
            {
                return BadRequest("No puedes actualizar una comanda ya pagada");
            }
            //NECESITO LO DE JORE PARA OBTENER LOS JODIDOS PLATOS, SU PRECIO, ETC. 
            var detail = await _IDetailsCommand.GetDetailsCommand(idCommand, idDish);
            detail.CantDish = value.cantDish;
            await _IDetailsCommand.updateDetailCommand(detail);
            return StatusCode(200, command);

        }

        // DELETE api/<DetailsCommandController>/5
        [HttpDelete("{idDish}/{idCommand}")]
        public async Task<IActionResult> DeleteCommand(string ididDish, int idCommand)
        {
            var details = await _IDetailsCommand.GetDetailsCommand(idCommand, ididDish);
            if(details == null) { return NotFound(); }
            //No creo que sea necesario validar que la comanda esté sin pagar, porque en el front-end no debería poderse actualizar los detalles de una comanda pagada
            //Solo en el swagger saldría error
            await _IDetailsCommand.deleteDetailCommand(details);
            return NoContent();

        }
    }
}
