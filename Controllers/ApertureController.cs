using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/aperture")]
    public class ApertureController : Controller
    {

        private readonly IAperture apertureServices;

        public ApertureController(IAperture apertureServices)
        {

            this.apertureServices = apertureServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApertureGet>>> getAll()
        {
            List<ApertureGet> lista = await apertureServices.getAll();

            return Ok(lista);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApertureGet>> getFindById([FromRoute] int id)
        {

            Aperture aperture = await apertureServices.getApertureById(id);

            if (aperture == null)
            {
                return NotFound();
            }

            ApertureGet apertureGet = aperture.Adapt<ApertureGet>();

            return Ok(apertureGet);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ApertureCreate aperture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newAperture = aperture.Adapt<Aperture>();


            int result = await apertureServices.saveAperture(newAperture);

            if (result == -1) return BadRequest();



            return StatusCode(201, Json(new { name = "hola", }));
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<ApertureGet>> Update([FromRoute] int id, ApertureCreate apertureUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            Aperture apertura = await apertureServices.getApertureById(id);

            if (apertura == null)
            {
                return BadRequest();
            }

            apertura.FecAperture = apertureUpdate.FecAperture;
            apertura.FecClose = apertureUpdate.FecClose;
            apertura.SaleToDay = apertureUpdate.SaleToDay;


            int result = await apertureServices.updateAperture(apertura);

            if (result == -1) return BadRequest();

            ApertureGet aperturaGet = apertura.Adapt<ApertureGet>();

            return Ok(aperturaGet);


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            int result = await apertureServices.deleteAperture(id);

            if (result == -1) return BadRequest();

            return Ok();

        }





    }
}
