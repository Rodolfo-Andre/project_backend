using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApertureController : Controller
    {
        private readonly IAperture _apertureService;

        public ApertureController(IAperture apertureService)
        {
            _apertureService = apertureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApertureGet>>> GetAperture()
        {
            List<ApertureGet> list = (await _apertureService.GetAll()).Adapt<List<ApertureGet>>();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApertureGet>> GetAperture(int id)
        {
            Aperture aperture = await _apertureService.GetById(id);

            if (aperture == null)
            {
                return NotFound("Apertura no encontrada");
            }

            ApertureGet apertureGet = aperture.Adapt<ApertureGet>();

            return Ok(apertureGet);
        }

        [HttpPost]
        public async Task<ActionResult<ApertureGet>> CreateAperture([FromBody] ApertureCreate aperture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAperture = aperture.Adapt<Aperture>();

            await _apertureService.CreateAperture(newAperture);

            var getAperture = (await _apertureService.GetById(newAperture.Id)).Adapt<ApertureGet>();

            return CreatedAtAction(nameof(GetAperture), new { id = getAperture.Id }, getAperture);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApertureGet>> UpdateAperture(int id, [FromBody] ApertureCreate apertureUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Aperture apertura = await _apertureService.GetById(id);

            if (apertura == null)
            {
                return NotFound("Apertura no encontrada");
            }

            apertura.FecAperture = apertureUpdate.FecAperture;
            apertura.FecClose = apertureUpdate.FecClose;
            apertura.SaleToDay = apertureUpdate.SaleToDay;

            await _apertureService.UpdateAperture(apertura);

            var getAperture = (await _apertureService.GetById(id)).Adapt<ApertureGet>();

            return Ok(getAperture);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aperture = await _apertureService.GetById(id);

            if (aperture == null)
            {
                return NotFound("Apertura no encontrada");
            }

            await _apertureService.DeleteAperture(aperture);

            return NoContent();
        }
    }
}
