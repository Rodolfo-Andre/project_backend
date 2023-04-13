using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/voucherType")]

    public class VoucherTypeController : Controller
    {
        private readonly IVoucherType voucherTypeServices;

        public VoucherTypeController(IVoucherType _voucherTypeServices)
        {

            this.voucherTypeServices = _voucherTypeServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<VoucherTypeGet>>> getAll()
        {
            List<VoucherTypeGet> lista = await voucherTypeServices.getAll();

            return Ok(lista);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherTypeGet>> getFindById([FromRoute] int id)
        {

            VoucherType voucherType = await voucherTypeServices.getVoucherById(id);

            if (voucherType == null)
            {
                return NotFound();
            }

            VoucherTypeGet voucherTypeGet = voucherType.Adapt<VoucherTypeGet>();

            return Ok(voucherTypeGet);
        }


        [HttpPost]
        public async Task<ActionResult> Create(VoucherTypeCreate voucherType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            VoucherType v = voucherType.Adapt<VoucherType>();


            int result = await voucherTypeServices.Create(v);

            if (result == -1) return BadRequest();



            return StatusCode(201);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherTypeGet>> Update([FromRoute] int id, VoucherTypeUpdate voucherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            VoucherType voucherType = await voucherTypeServices.getVoucherById(id);

            if (voucherType == null)
            {
                return BadRequest();
            }

            voucherType.Name = voucherUpdate.Name;



            int result = await voucherTypeServices.Update(voucherType);

            if (result == -1) return BadRequest();

            VoucherTypeGet voucherTypeGet = voucherType.Adapt<VoucherTypeGet>();

            return Ok(voucherTypeGet);


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            int result = await voucherTypeServices.Delete(id);

            if (result == -1) return BadRequest();

            return Ok();

        }
    }
}
