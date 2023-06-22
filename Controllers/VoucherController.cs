using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Dto;
using project_backend.Enums;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherController : Controller
    {
        private readonly IVoucher _voucherService;
        private readonly ICommands _commandService;
        private readonly ITableRestaurant _tableService;

        public VoucherController(IVoucher voucherService, ICommands commandService, ITableRestaurant tableService)
        {
            _voucherService = voucherService;
            _commandService = commandService;
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherGet>>> GetVoucher()
        {
            List<VoucherGet> listVoucher = (await _voucherService.GetAll()).Adapt<List<VoucherGet>>();

            return Ok(listVoucher);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherGet>> GetVoucher(int id)
        {
            Voucher voucher = await _voucherService.GetById(id);

            if (voucher == null)
            {
                return NotFound("Comprobante de Pago no encontrado");
            }

            VoucherGet voucherGet = voucher.Adapt<VoucherGet>();

            return Ok(voucherGet);
        }

        [HttpPost]
        public async Task<ActionResult<VoucherGet>> CreateVoucher([FromBody] VoucherCreate voucherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 

            bool created = await _voucherService.CreateVoucher(voucherCreate);


            if (!created)
            {
                return BadRequest("No se pudo crear el comprobante de pago");
            }

            return Ok("nuevo");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherGet>> UpdateVoucher(int id, [FromBody] VoucherCreate voucherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Voucher voucher = await _voucherService.GetById(id);

            if (voucher == null)
            {
                return NotFound("Comprobante de Pago no encontrado");
            }

            voucher.CommandsId = voucherUpdate.idCommand;
            voucher.VoucherTypeId = voucherUpdate.idTypeVoucher;
            voucher.EmployeeId = voucherUpdate.idEmployee;
            voucher.DateIssued = voucherUpdate.DateIssued;
            voucher.CashId = voucherUpdate.CashId;
            voucher.TotalPrice = voucherUpdate.TotalPrice;

            await _voucherService.UpdateVoucher(voucher);

            var getVoucher = (await _voucherService.GetById(id)).Adapt<VoucherGet>();

            return Ok(getVoucher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVoucher(int id)
        {
            var voucher = await _voucherService.GetById(id);

            if (voucher == null)
            {
                return NotFound("Comprobante de pago no encontrado");
            }

            await _voucherService.DeleteVoucher(voucher);

            return NoContent();
        }

        [HttpGet("sales-data-per-date")]
        public async Task<ActionResult<IEnumerable<SalesDataPerDate>>> GetSalesDataPerDate()
        {
            return Ok(await _voucherService.GetSalesDataPerDate());
        }
    }
}
