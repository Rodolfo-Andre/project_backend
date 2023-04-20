using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsCommandController : ControllerBase
    {
        private readonly ICommands _commandService;
        private readonly IDetailsCommand _detailsCommand;

        public DetailsCommandController(ICommands commandService, IDetailsCommand detailsCommand)
        {
            _commandService = commandService;
            _detailsCommand = detailsCommand;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailsComand>>> GetDetailsCommand()
        {
            List<DetailsComand> details = await _detailsCommand.GetAll();
            return Ok(details);
        }

        [HttpGet("{idDish}/{idCommand}")]
        public async Task<ActionResult<DetailCommandGet>> GetDetailsCommand(string idDish, int idCommand)
        {
            var detail = await _detailsCommand.GetByCommandIdAndDishId(idCommand, idDish);

            if (detail == null)
            {
                return NotFound("Detalle de Comanda no encontrada");
            }

            DetailCommandGet detailGet = detail.Adapt<DetailCommandGet>();
            return Ok(detailGet);
        }

        [HttpPost]
        public async Task<ActionResult<DetailCommandGet>> CreateDetailsCommand([FromBody] DetailCommandCreate detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Validar que exista la comanda
            var command = await _commandService.GetById(detail.IdCommand);

            if (command == null)
            {
                return NotFound("Comanda no encontrada");
            }

            if (command.StatesCommandId == 2)
            {
                return BadRequest("No puedes actualizar una comanda ya pagada");
            }

            //AQUÍ TENGO QUE VALIDAR LOS PLATOS <- Que no se repitan los mismos platos, solo que aumente su cantidad. PERO FALTA LO DE JORE
            List<DetailsComand> listValid = await _detailsCommand.GetByCommandId(command.Id);
            foreach (var detailsComand in listValid)
            {
                if (detailsComand.DishId == detail.DishId)
                {
                    return BadRequest("El plato ya está solicitado, tendrías que actualizar la cantidad");
                }
            }

            var newDetail = detail.Adapt<DetailsComand>();
            await _detailsCommand.CreateDetailCommand(newDetail);
            var getDetail = await _detailsCommand.GetByCommandIdAndDishId(newDetail.CommandsId, newDetail.DishId);

            return CreatedAtAction(nameof(GetDetailsCommand), new { id = getDetail.Id }, getDetail);
        }

        [HttpPut("{idDish}/{idCommand}")]
        public async Task<ActionResult<DetailCommandGet>> UpdateDetailCommand(string idDish, int idCommand, [FromBody] DetailCommandUpdate value)
        {
            //Validar que exista la comanda
            var command = await _commandService.GetById(idCommand);

            if (command == null)
            {
                return NotFound("Comanda no encontrada");
            }

            if (command.StatesCommandId == 2)
            {
                return BadRequest("No puedes actualizar una comanda ya pagada");
            }

            //NECESITO LO DE JORE PARA OBTENER LOS JODIDOS PLATOS, SU PRECIO, ETC. 
            var detail = await _detailsCommand.GetByCommandIdAndDishId(idCommand, idDish);
            detail.CantDish = value.cantDish;
            await _detailsCommand.UpdateDetailCommand(detail);
            return Ok(command);
        }

        [HttpDelete("{idDish}/{idCommand}")]
        public async Task<IActionResult> DeleteCommand(string ididDish, int idCommand)
        {
            var details = await _detailsCommand.GetByCommandIdAndDishId(idCommand, ididDish);

            if (details == null) { return NotFound(""); }

            //No creo que sea necesario validar que la comanda esté sin pagar, porque en el front-end no debería poderse actualizar los detalles de una comanda pagada
            //Solo en el swagger saldría error
            await _detailsCommand.DeleteDetailCommand(details);
            return NoContent();
        }
    }
}
