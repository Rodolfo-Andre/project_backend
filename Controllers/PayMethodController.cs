using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayMethodController : ControllerBase
    {
        private readonly IPayMethod _payMethodService;

        public PayMethodController(IPayMethod payMethodService)
        {
            _payMethodService = payMethodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayMethodGet>>> GetPaymethod()
        {
            return Ok((await _payMethodService.GetAll()).Adapt<List<PayMethodGet>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PayMethodGet>> GetPaymethod(int id)
        {
            var payMethod = await _payMethodService.GetById(id);

            if (payMethod == null)
            {
                return NotFound("Método de Pago no encontrado");
            }

            var payMethodGet = payMethod.Adapt<PayMethodGet>();

            return Ok(payMethodGet);
        }

        [HttpPost]
        public async Task<ActionResult<PayMethodGet>> CreatePaymethod([FromBody] PayMethodPrincipal payMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPayMethod = payMethod.Adapt<PayMethod>();

            await _payMethodService.CreatePaymethod(newPayMethod);

            var getPayMethod = (await _payMethodService.GetById(newPayMethod.Id)).Adapt<PayMethodGet>();

            return CreatedAtAction(nameof(GetPaymethod), new { id = getPayMethod.Id }, getPayMethod);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PayMethodGet>> UpdatePaymethod(int id, [FromBody] PayMethodPrincipal payMethodUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payMethod = await _payMethodService.GetById(id);

            if (payMethod == null)
            {
                return NotFound("Método de Pago no encontrado");
            }

            payMethod.Paymethod = payMethodUpdate.Paymethod;

            await _payMethodService.UpdatePaymethod(payMethod);

            var getPaymethod = (await _payMethodService.GetById(id)).Adapt<PayMethodGet>();

            return Ok(getPaymethod);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymethod(int id)
        {
            var payMethod = await _payMethodService.GetById(id);

            if (payMethod == null)
            {
                return NotFound("Método de Pago no encontrado");
            }

            await _payMethodService.DeletePaymethod(payMethod);

            return NoContent();
        }
    }
}
