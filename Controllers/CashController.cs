using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CashController : ControllerBase
    {
        private readonly ICash _cashService;
        private readonly IEstablishment _establishmentService;


        public CashController(ICash cashService, IEstablishment establishmentService)
        {
            _cashService = cashService;
            _establishmentService = establishmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashGet>>> GetCash()
        {
            List<CashGet> listCash = (await _cashService.GetAll()).Adapt<List<CashGet>>();

            return Ok(listCash);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CashGet>> GetCash(int id)
        {
            var cash = await _cashService.GetById(id);

            if (cash == null)
            {
                return NotFound("Caja no encontrada");
            }

            var cashGet = cash.Adapt<CashGet>();

            return Ok(cashGet);
        }

        [HttpPost]
        public async Task<ActionResult<CashGet>> CreateCash([FromBody] CashPrincipal cash)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var establishment = await _establishmentService.GetById(cash.EstablishmentId);

            if (establishment == null)
            {
                return NotFound("Establecimiento no encontrado");
            }

            var newCash = cash.Adapt<Cash>();
            await _cashService.CreateCash(newCash);

            var getCash = (await _cashService.GetById(newCash.Id)).Adapt<CashGet>();

            return CreatedAtAction(nameof(GetCash), new { id = getCash.Id }, getCash);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CashGet>> UpdateCash(int id, [FromBody] CashPrincipal cashUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cash = await _cashService.GetById(id);

            if (cash == null)
            {
                return NotFound("Caja no encontrada");
            }

            if (cash.EstablishmentId != cashUpdate.EstablishmentId)
            {
                var establishment = await _establishmentService.GetById(cashUpdate.EstablishmentId);

                if (establishment == null)
                {
                    return NotFound("Establecimiento no encontrado");
                }

                cash.EstablishmentId = cashUpdate.EstablishmentId;
            }

            var getCash = (await _cashService.GetById(id)).Adapt<CashGet>();

            return Ok(getCash);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCash(int id)
        {
            var cash = await _cashService.GetById(id);

            if (cash == null)
            {
                return NotFound("Caja no encontrada");
            }

            await _cashService.DeleteCash(cash);

            return NoContent();
        }
    }
}

