using Microsoft.AspNetCore.Mvc;
using project_backend.Data;
using project_backend.Schemas;
using project_backend.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;
using project_backend.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_backend.Controllers
{
    [Route("api/tables")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly CommandsContext _contex;
        private readonly ITableRestaurant _tableService;

        public TablesController(CommandsContext contex, ITableRestaurant ITable)
        {
            _contex = contex;
            _tableService = ITable;
        }

        // GET: api/<TablesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableGet>>> GetTable()
        {
            List<TableGet> tables = (await _tableService.GetTables()).Adapt<List<TableGet>>();
            return tables;
        }

        // GET api/<TablesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TableGet>> GetTable(int id)
        {
            var table = await _tableService.GetTableById(id);
            if (table == null)
            {
                return NotFound();
            }
            TableGet tableGet = table.Adapt<TableGet>();
            return tableGet;
        }

        // POST api/<TablesController>
        [HttpPost]
        public async Task<ActionResult<TableRestaurant>> PostTable([FromBody] TableCreate tableCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTable = tableCreate.Adapt<TableRestaurant>();
            newTable.StateTable = "Libre";
            newTable.NumSeats = tableCreate.numSeats;
            await _tableService.createTable(newTable);
            var getTable = await _tableService.GetTableById(newTable.NumTable);
            return StatusCode(201, getTable);

        }

        // PUT api/<TablesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTable(int id, [FromBody] TableUpdate request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var updateTable = await _tableService.GetTableById(id);
            if (updateTable == null) { return NotFound(); }

            updateTable.NumSeats = request.numSeats;
            updateTable.StateTable = request.StateTable;
            await _tableService.UpdateStateTable(updateTable);
            var getTable = await _tableService.GetTableById(updateTable.NumTable);
            return StatusCode(200, getTable);
        }

        // DELETE api/<TablesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var table = await _tableService.GetTableById(id);
            if (table == null) { return NotFound(); }
            await _tableService.DeleteStateTable(table);
            
            return NoContent();
        }
    }
}
