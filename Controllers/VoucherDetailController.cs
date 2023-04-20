using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherDetailController : Controller
    {
        private readonly IVoucherDetail _voucherDetailService;

        public VoucherDetailController(IVoucherDetail voucherDetailService)
        {
            _voucherDetailService = voucherDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VoucherDetailGet>>> GetVoucherDetail()
        {
            List<VoucherDetailGet> listVoucherDetail = (await _voucherDetailService.GetAll()).Adapt<List<VoucherDetailGet>>();

            return Ok(listVoucherDetail);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherDetailGet>> GetVoucherDetail(int id)
        {
            VoucherDetail voucherDetail = await _voucherDetailService.GetById(id);

            if (voucherDetail == null)
            {
                return NotFound("Detalle de Comprobante no encontrado");
            }

            VoucherDetailGet voucherDetailGet = voucherDetail.Adapt<VoucherDetailGet>();

            return Ok(voucherDetailGet);
        }

        [HttpPost]
        public async Task<ActionResult<VoucherDetailGet>> CreateVoucherDetail([FromBody] VoucherDetailCreate voucherDetailCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VoucherDetail newvoucherDetail = voucherDetailCreate.Adapt<VoucherDetail>();

            await _voucherDetailService.CreateVoucherDetail(newvoucherDetail);

            var getVoucherDetailGet = (await _voucherDetailService.GetById(newvoucherDetail.Id)).Adapt<VoucherGet>();

            return CreatedAtAction(nameof(GetVoucherDetail), new { id = getVoucherDetailGet.Id }, getVoucherDetailGet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherDetailGet>> UpdateVoucherDetail(int id, [FromBody] VoucherDetailUpdate voucherDetailUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VoucherDetail voucherDetail = await _voucherDetailService.GetById(id);

            if (voucherDetail == null)
            {
                return NotFound("Detalle de Comprobante no encontrado");
            }

            voucherDetail.PaymentAmount = voucherDetailUpdate.PaymentAmount;
            voucherDetail.PayMethodId = voucherDetailUpdate.PayMethodId;
            voucherDetail.VoucherId = voucherDetailUpdate.VoucherId;

            await _voucherDetailService.UpdateVoucherDetail(voucherDetail);

            var getVoucherDetailGet = (await _voucherDetailService.GetById(id)).Adapt<VoucherGet>();

            return Ok(getVoucherDetailGet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVoucherDetail(int id)
        {
            var voucherDetail = await _voucherDetailService.GetById(id);

            if (voucherDetail == null)
            {
                return NotFound("Detalle de Comprobante no encontrado");
            }
            await _voucherDetailService.DeleteVoucherDetail(voucherDetail);

            return NoContent();
        }
    }
}
