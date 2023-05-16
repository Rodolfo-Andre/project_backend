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
        public async Task<ActionResult<IEnumerable<TableGet>>> GetTable()
        {
            return Ok((await _tableService.GetAll()).Adapt<List<TableGet>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableGet>> GetTable(int id)
        {
            var table = await _tableService.GetById(id);

            if (table == null)
            {
                return NotFound("Mesa no encontrada");
            }

            var tableGet = table.Adapt<TableGet>();

            return Ok(tableGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<TableGet>> CreateTable([FromBody] TablePrincipal table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTable = table.Adapt<TableRestaurant>();
            newTable.StateTable = TypeTableState.Free.GetEnumMemberValue();

            await _tableService.CreateTable(newTable);

            var getTable = (await _tableService.GetById(newTable.NumTable)).Adapt<TableGet>();

            return CreatedAtAction(nameof(GetTable), new { id = getTable.NumTable }, getTable);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TableGet>> UpdateTable(int id, [FromBody] TableUpdate table)
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

            var getTable = (await _tableService.GetById(id)).Adapt<TableGet>();

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
    }
}
