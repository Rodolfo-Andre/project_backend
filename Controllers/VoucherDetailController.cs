using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherDetailController : Controller
    {

        private readonly IVoucherDetail _voucherDetailServices;

        public VoucherDetailController(IVoucherDetail voucherDetailServices)
        {

            this._voucherDetailServices = voucherDetailServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<VoucherDetailGet>>> getAll()
        {
            List<VoucherDetailGet> lista = await _voucherDetailServices.getAll();

            return Ok(lista);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherDetailGet>> getFindById([FromRoute] int id)
        {

            VoucherDetail voucherDetail = await _voucherDetailServices.getVoucherById(id);

            if (voucherDetail == null)
            {
                return NotFound();
            }

            VoucherDetailGet voucherDetailGet = voucherDetail.Adapt<VoucherDetailGet>();

            return Ok(voucherDetailGet);
        }


        [HttpPost]
        public async Task<ActionResult> Create(VoucherDetailCreate voucherDetailCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            VoucherDetail voucherDetail = voucherDetailCreate.Adapt<VoucherDetail>();


            int result = await _voucherDetailServices.Create(voucherDetail);

            if (result == -1) return BadRequest();



            return StatusCode(201, Json(new { name = "hola", }));
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherDetailGet>> Update([FromRoute] int id, VoucherDetailUpdate voucherDetailUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            VoucherDetail voucherDetail = await _voucherDetailServices.getVoucherById(id);

            if (voucherDetail == null)
            {
                return BadRequest();
            }

            voucherDetail.PaymentAmount = voucherDetailUpdate.PaymentAmount;
            voucherDetail.PayMethodId = voucherDetailUpdate.PayMethodId;
            voucherDetail.VoucherId = voucherDetailUpdate.VoucherId;
        



            int result = await _voucherDetailServices.Update(voucherDetail);

            if (result == -1) return BadRequest();

            VoucherDetailGet voucherDetailGet = voucherDetail.Adapt<VoucherDetailGet>();

            return Ok(voucherDetailGet);


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            int result = await _voucherDetailServices.Delete(id);

            if (result == -1) return BadRequest();

            return Ok();

        }



    }
}
