using Mapster;
using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDish _dishService;
        private readonly ICategoryDish _categoryDishService;

        public DishController(IDish disService, ICategoryDish categoryDishService)
        {
            _dishService = disService;
            _categoryDishService = categoryDishService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishGet>>> GetDish()
        {
            return Ok((await _dishService.GetAll()).Adapt<List<DishGet>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishGet>> GetDish(string id)
        {
            var dish = await _dishService.GetById(id);

            if (dish == null)
            {
                return NotFound("Plato no encontrado");
            }

            var dishGet = dish.Adapt<DishGet>();

            return Ok(dishGet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DishGet>> UpdateDish(string id, [FromBody] DishCreateOrUpdate value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateDish = await _dishService.GetById(id);

            if (updateDish == null)
            {
                return NotFound("Plato no encontrado");
            }

            if (updateDish.CategoryDishId != value.CategoryDishId)
            {
                var newCategoryDish = await _categoryDishService.GetById(value.CategoryDishId);

                if (newCategoryDish == null)
                {
                    return NotFound("Categoría de Plato no encontrada");
                }

                updateDish.CategoryDishId = value.CategoryDishId;
            }

            updateDish.NameDish = value.NameDish;
            updateDish.PriceDish = value.PriceDish;
            updateDish.ImgDish = value.ImgDish;
            await _dishService.UpdateDish(updateDish);

            var getDish = (await _dishService.GetById(id)).Adapt<DishGet>();

            return Ok(getDish);
        }

        [HttpPost]
        public async Task<ActionResult<DishGet>> CreateDish([FromBody] DishCreateOrUpdate dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDish = await _categoryDishService.GetById(dish.CategoryDishId);

            if (categoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            var newDish = dish.Adapt<Dish>();
            await _dishService.CreateDish(newDish);

            var getDish = (await _dishService.GetById(newDish.Id)).Adapt<DishGet>();

            return CreatedAtAction(nameof(GetDish), new { id = getDish.Id }, getDish);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(string id)
        {
            var dish = await _dishService.GetById(id);

            if (dish == null)
            {
                return NotFound("Plato no encontrado");
            }

            await _dishService.DeteleDish(dish);

            return NoContent();
        }
    }
}
