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

            Commands command = await _commandService.GetById(voucherCreate.CommandsId);

            if (command == null)
            {
                return NotFound("Comprobante de Pago no encontrado");
            }

            if (command.StatesCommandId == (int)TypeCommandState.Paid)
            {
                return BadRequest("La Comanda ya fue pagada");
            }

            TableRestaurant tableRestaurant = await _tableService.GetById(voucherCreate.TableRestaurantId);

            if (tableRestaurant == null)
            {
                return NotFound("Mesa no encontrada");
            }

            tableRestaurant.StateTable = TypeTableState.Occupied.ToString();
            command.StatesCommandId = 1;

            await _commandService.UpdateCommand(command);
            await _tableService.UpdateTable(tableRestaurant);

            var newVoucher = voucherCreate.Adapt<Voucher>();

            await _voucherService.CreateVoucher(newVoucher);

            var getVoucher = (await _voucherService.GetById(newVoucher.Id)).Adapt<VoucherGet>();

            return CreatedAtAction(nameof(GetVoucher), new { id = getVoucher.Id }, getVoucher);
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

            voucher.CommandsId = voucherUpdate.CustomerId;
            voucher.VoucherTypeId = voucherUpdate.VoucherTypeId;
            voucher.EmployeeId = voucherUpdate.EmployeeId;
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
