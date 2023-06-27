using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryDishController : ControllerBase
    {
        private readonly ICategoryDish _categoryDishService;

        public CategoryDishController(ICategoryDish categoryDishService)
        {
            _categoryDishService = categoryDishService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDishGet>>> GetCategoryDish()
        {
            return Ok((await _categoryDishService.GetAll()).Adapt<List<CategoryDishGet>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDishGet>> GetCategoryDish(string id)
        {
            var categoryDish = await _categoryDishService.GetById(id);

            if (categoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            var categoryDishGet = categoryDish.Adapt<CategoryDishGet>();

            return Ok(categoryDishGet);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoryDishGet>> UpdateCategoryDish(string id, [FromBody] CategoryDishPrincipal categoryDish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateCategoryDish = await _categoryDishService.GetById(id);

            if (updateCategoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            var isNotNameCatDishUnique = !await _categoryDishService.IsNameCatDishUnique(categoryDish.NameCatDish, updateCategoryDish.Id);

            if (isNotNameCatDishUnique)
            {
                return Conflict("El nombre de categoría ya está en uso");
            }

            updateCategoryDish.NameCatDish = categoryDish.NameCatDish;
            await _categoryDishService.UpdateCategoryDish(updateCategoryDish);

            var getCategoryDish = (await _categoryDishService.GetById(id)).Adapt<CategoryDishGet>();

            return Ok(getCategoryDish);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoryDishGet>> CreateCategoryDish([FromBody] CategoryDishPrincipal categoryDish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isNotNameCatDishUnique = !await _categoryDishService.IsNameCatDishUnique(categoryDish.NameCatDish);

            if (isNotNameCatDishUnique)
            {
                return Conflict("El nombre de categoría ya está en uso");
            }

            var newCategoryDish = categoryDish.Adapt<CategoryDish>();

            await _categoryDishService.CreateCategoryDish(newCategoryDish);

            var getCategoryDish = (await _categoryDishService.GetById(newCategoryDish.Id)).Adapt<CategoryDishGet>();

            return CreatedAtAction(nameof(GetCategoryDish), new { id = getCategoryDish.Id }, getCategoryDish);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteCategoryDish(string id)
        {
            var categoryDish = await _categoryDishService.GetById(id);

            if (categoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            await _categoryDishService.DeteleCategoryDish(categoryDish);

            return NoContent();
        }

        [HttpGet("{id}/number-dish")]
        public async Task<ActionResult<int>> GetNumberDishInCategoryDish(string id)
        {
            var categoryDish = await _categoryDishService.GetById(id);

            if (categoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            var count = await _categoryDishService.GetNumberDishInCategoryDish(categoryDish.Id);

            return Ok(count);
        }
    }
}
