using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;
using System.Drawing;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayMethodController : ControllerBase
    {

        private readonly IPayMethod _context;
        public PayMethodController(IPayMethod context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayMethodGet>>> getAll()
        {
            List<PayMethodGet> listaPaymethod = await _context.getPayMethods();

            return listaPaymethod;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PayMethod>> getPaymethod(int id)
        {
            var Paymethodservice = await _context.getPayMethod(id);

            if (Paymethodservice == null)
            {

                return NotFound("No se encuentra el metodo de pago");

            }


            return Paymethodservice;

        }

        [HttpPost]

        public async Task<ActionResult> Create(PayMethodDefault payMethodCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPayMethod = payMethodCreate.Adapt<PayMethod>();

            await _context.createPaymethod(newPayMethod);

            return Ok(payMethodCreate)

;
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<PayMethodGet>> Update(int id, PayMethodDefault payMethodUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var payMethod = await _context.getPayMethod(id);

            if (payMethod == null)
            {
                return BadRequest("El metodo de pago no existe");
            }

            payMethod.Paymethod = payMethodUpdate.Paymethod;

            await _context.updatePaymethod(payMethod);

            PayMethodGet payMethodGet = payMethod.Adapt<PayMethodGet>();



            return Ok(payMethod);



        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            var payMethod = await _context.getPayMethod(id);
            if (payMethod == null)
            {
                return NotFound();
            }
            await _context.deletePaymethod(payMethod);

            return Ok();
        }
    }


}
