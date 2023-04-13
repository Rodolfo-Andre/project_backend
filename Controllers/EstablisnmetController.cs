using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablisnmetController : ControllerBase
    {

        private readonly IEstablishment _context;

        public EstablisnmetController(IEstablishment contextAccessor)
        {
            _context = contextAccessor;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Establishment>>> getAll()
        {
            List<Establishment> listEstablishment = await _context.GetEstablishments();
            return Ok(listEstablishment);


        }


        [HttpPut]
        public async Task<ActionResult<Establishment>> Update(int id, EstablishmentPrincipal establishmentput)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var establisment = await _context.GetEstablishment(id);

            if (establisment == null)
            {
                return BadRequest("No se encontro Establecimiento");
            }


            establisment.Name = establishmentput.Name;
            establisment.Address = establishmentput.Address;
            establisment.Phone = establishmentput.Phone;
            establisment.Ruc = establisment.Ruc;
            await _context.updateEstblisment(establisment);
            return Ok(establisment);



        }
    }
}
