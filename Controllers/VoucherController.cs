using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using project_backend.Data;
using project_backend.Enums;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{

    [ApiController]
    [Route("api/comprobante")]
    public class VoucherController : Controller
    {

        private readonly IVoucher _context;
        private readonly ICommands _commandContext;
        private readonly ITableRestaurant _commandTable;


        public VoucherController(IVoucher context, ICommands commandsServices, ITableRestaurant _commandTable)
        {
            this._context = context;
            this._commandContext = commandsServices;
            this._commandTable = _commandTable;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherGet>>> getAll()
        {
            List<VoucherGet> listVoucher = await _context.getAll();


            return Ok(listVoucher);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<VoucherGet>> getById([FromRoute] int id)
        {

            Voucher voucher = await _context.getVoucherById(id);

            if (voucher == null)
            {
                return NotFound("No se encontro el comprobante ");
            }


            VoucherGet voucherGet = voucher.Adapt<VoucherGet>();


            return Ok(voucherGet);
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VoucherCreate voucherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            Commands command = await _commandContext.getComand(voucherCreate.CommandId);

            if (command == null)
            {
                return BadRequest("El Comando no exite");
            }


            if (command.StatesCommandId == (int)TypeCommandState.Paid)
            {
                return base.BadRequest("El Comando ya fue pagado");
            }


            TableRestaurant tableRestaurant = await _commandTable.GetTableById(voucherCreate.TableRestaurantId);

            if (tableRestaurant is null)
            {
                return BadRequest("La mesa  no exite");
            }


            tableRestaurant.StateTable = TypeTableState.Occupied.ToString();
            command.StatesCommandId = 1;

            await _commandContext.updateCommand(command);
            await _commandTable.UpdateStateTable(tableRestaurant);



            var newVoucher = voucherCreate.Adapt<Voucher>();



            int result = await _context.saveVoucher(newVoucher);

            if (result == -1) return BadRequest("No se pudo crear el comprobante");

            return StatusCode(201);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<VoucherGet>> Update([FromRoute] int id, [FromBody] VoucherCreate voucherUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Voucher voucher = await _context.getVoucherById(id);

            if (voucher == null)
            {
                return BadRequest("El Comprobante no exite");
            }


            voucher.EstablishmentId = voucherUpdate.EstablishmentId;
            voucher.ApertureId = voucherUpdate.ApertureId;
            voucher.VoucherTypeId = voucherUpdate.VoucherTypeId;
            voucher.UserId = voucherUpdate.UserId;
            voucher.CustomerName = voucherUpdate.CustomerName;
            voucher.DateIssued = voucherUpdate.DateIssued;
            voucher.NumCom = voucherUpdate.NumCom;
            voucher.TotalPrice = voucherUpdate.TotalPrice;

            int result = await _context.updateVoucher(voucher);
            if (result == -1) return BadRequest("No se pudo actualizar el comprobante");


            VoucherGet voucherGet = voucher.Adapt<VoucherGet>();

            return Ok(voucherGet);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int result = await _context.deleteVoucherById(id);

            if (result == -1) return BadRequest("No se pudo eliminar el comprobante");

            return Ok();

        }

    }

}
