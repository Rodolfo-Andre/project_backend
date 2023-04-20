using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherTypeController : Controller
    {
        private readonly IVoucherType _voucherTypeService;

        public VoucherTypeController(IVoucherType voucherTypeService)
        {
            _voucherTypeService = voucherTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VoucherTypeGet>>> GetVoucherType()
        {
            List<VoucherTypeGet> listVoucherType = (await _voucherTypeService.GetAll()).Adapt<List<VoucherTypeGet>>();

            return Ok(listVoucherType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherTypeGet>> GetVoucherType(int id)
        {
            VoucherType voucherType = await _voucherTypeService.GetById(id);

            if (voucherType == null)
            {
                return NotFound("Tipo de Comprobante no encontrado");
            }

            VoucherTypeGet voucherTypeGet = voucherType.Adapt<VoucherTypeGet>();

            return Ok(voucherTypeGet);
        }

        [HttpPost]
        public async Task<ActionResult<VoucherTypeGet>> CreateVoucherType([FromBody] VoucherTypePrincipal voucherType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VoucherType newVoucherType = voucherType.Adapt<VoucherType>();

            await _voucherTypeService.CreateVoucherType(newVoucherType);

            var voucherTypeGet = (await _voucherTypeService.GetById(newVoucherType.Id)).Adapt<VoucherTypeGet>();

            return CreatedAtAction(nameof(GetVoucherType), new { id = voucherTypeGet.Id }, voucherTypeGet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherTypeGet>> UpdateVoucherType(int id, [FromBody] VoucherTypePrincipal voucherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VoucherType voucherType = await _voucherTypeService.GetById(id);

            if (voucherType == null)
            {
                return NotFound("Tipo de Comprobante no encontrado");
            }

            voucherType.Name = voucherUpdate.Name;

            await _voucherTypeService.UpdateVoucherType(voucherType);

            var voucherTypeGet = (await _voucherTypeService.GetById(id)).Adapt<VoucherTypeGet>();

            return Ok(voucherTypeGet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVoucherType(int id)
        {
            var voucherType = await _voucherTypeService.GetById(id);

            if (voucherType == null)
            {
                return NotFound("Tipo de Comprobante no encontrado");
            }
            await _voucherTypeService.DeleteVoucherType(voucherType);

            return NoContent();
        }
    }
}
