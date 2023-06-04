using Microsoft.AspNetCore.Mvc;
using project_backend.Schemas;
using project_backend.Models;
using Mapster;
using project_backend.Interfaces;
using project_backend.Enums;
using project_backend.Utils;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TableController : ControllerBase
    {
        private readonly ITableRestaurant _tableService;

        public TableController(ITableRestaurant tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableRestaurantGet>>> GetTable()
        {
            return Ok((await _tableService.GetAll()).Adapt<List<TableRestaurantGet>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableRestaurantGet>> GetTable(int id)
        {
            var table = await _tableService.GetById(id);

            if (table == null)
            {
                return NotFound("Mesa no encontrada");
            }

            var TableRestaurantGet = table.Adapt<TableRestaurantGet>();

            return Ok(TableRestaurantGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<TableRestaurantGet>> CreateTable([FromBody] TableRestaurantPrincipal table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTable = table.Adapt<TableRestaurant>();
            newTable.StateTable = TypeTableState.Free.GetEnumMemberValue();

            await _tableService.CreateTable(newTable);

            var getTable = (await _tableService.GetById(newTable.NumTable)).Adapt<TableRestaurantGet>();

            return CreatedAtAction(nameof(GetTable), new { id = getTable.NumTable }, getTable);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TableRestaurantGet>> UpdateTable(int id, [FromBody] TableUpdate table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateTable = await _tableService.GetById(id);

            if (updateTable == null)
            {
                return NotFound("Mesa no encontrada");
            }

            updateTable.NumSeats = table.NumSeats;
            updateTable.StateTable = table.StateTable.GetEnumMemberValue();

            await _tableService.UpdateTable(updateTable);

            var getTable = (await _tableService.GetById(id)).Adapt<TableRestaurantGet>();

            return Ok(getTable);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var table = await _tableService.GetById(id);

            if (table == null)
            {
                return NotFound("Mesa no encontrada");
            }

            await _tableService.DeleteTable(table);

            return NoContent();
        }

        [HttpGet("{id}/number-commands")]
        public async Task<ActionResult<int>> GetNumberCommandsInTable(int id)
        {
            var table = await _tableService.GetById(id);

            if (table == null)
            {
                return NotFound("Mesa no encontrada");
            }

            var count = await _tableService.GetNumberCommandsInTable(table.NumTable);

            return Ok(count);
        }
    }
}
