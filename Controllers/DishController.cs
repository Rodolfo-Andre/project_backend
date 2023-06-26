using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_backend.Dto;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

        [HttpGet("{id}/number-details-commands")]
        public async Task<ActionResult<int>> GetNumberDetailsCommandsInDish(string id)
        {
            var dish = await _dishService.GetById(id);

            if (dish == null)
            {
                return NotFound("Plato no encontrado");
            }

            var count = await _dishService.GetNumberDetailsCommandsInDish(dish.Id);

            return Ok(count);
        }

        [HttpGet("dish-order-statistics")]
        public async Task<ActionResult<IEnumerable<DishOrderStatistics>>> GetDishOrderStatistics()
        {
            return Ok(await _dishService.GetDishOrderStatistics());
        }


        [HttpGet("category-dish")]
        public async Task<ActionResult<IEnumerable<CategoryDishGet>>> GetCategoryDish()
        {
            return Ok((await _categoryDishService.GetAll()).Adapt<List<CategoryDishGet>>());
        }

        [HttpGet("get-dishByIdCategory/{id}")]
        public async Task<ActionResult<IEnumerable<DishGet>>> GetDishByIdCategory(string id)
        {
            var categoryDish = await _categoryDishService.GetById(id);
            if (categoryDish == null)
            {
                return NotFound("Categoría de Plato no encontrada");
            }

            List<Dish> list = await _dishService.GetAll();

            var dish = list.Where(x => x.CategoryDishId == id).ToList();

            return Ok(dish.Adapt<List<DishGet>>());
        }

        [HttpGet("verify-name/{nameDish}/{idDish?}")]
        public async Task<ActionResult> VerifyName(string nameDish, string idDish = null)
        {
            var IsNotNameDishUnique = !await _dishService.IsNameDishUnique(nameDish);

            if (!string.IsNullOrEmpty(idDish))
            {
                IsNotNameDishUnique = !await _dishService.IsNameDishUnique(nameDish, idDish);
            }

            if (IsNotNameDishUnique)
            {
                return Conflict("El nombre de plato ya está en uso");
            }

            return Ok(new { isFound = IsNotNameDishUnique });
        }
    }
}
