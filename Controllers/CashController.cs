using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Interfaces;
using project_backend.Services;
using project_backend.Models;
using project_backend.Schemas;
using Mapster;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashController : ControllerBase
    {
        private readonly ICash _context;

        public CashController(ICash context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashSchema>>> getAll() {

            List<CashSchema> listaCash = await _context.getAllCash();
            return listaCash;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cash>> GetCash(int id)
        {
            var Cashservice = await _context.getCash(id);

            if (Cashservice == null)
            {

                return NotFound("No se encuentra la caja");
            }
            return Cashservice;
        }

        [HttpPost]

        public async Task<ActionResult> Create(CashSchema cash)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            var newCash = cash.Adapt<Cash>();
            await _context.createCash(newCash);
            return Ok(newCash);




        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cash>> Update (int id, CashSchema cashema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var cash =await _context.getCash(id);

            if (cash ==null)
            {
                return BadRequest("La caja no existe");
            }

            cash.EstablishmentId = cashema.EstablishmentId;
            await _context.updateCash(cash);


            return Ok(cash);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            var cash = await _context.getCash(id);

            if (cash == null)
            {
                return NotFound();
            }
            await _context.deleteCash(cash);
            return Ok(cash);

        }
        }
    }

