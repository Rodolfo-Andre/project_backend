
using Mapster;


using Microsoft.AspNetCore.Mvc;

using project_backend.Interfaces;
using project_backend.Models;

using project_backend.Schemas.project_backend.Schemas;
using project_backend.Services;
using project_backend.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace project_backend.Controllers
{
    [Route("api/categorydish")]
    [ApiController]
    public class CategoryDishController : ControllerBase
    {
        private readonly ICategoryDish _categoryDishService;

        public CategoryDishController(ICategoryDish categoryDishService)
        {
            _categoryDishService = categoryDishService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryDishGet>>> GetCategoryDishs()
        {
            List<CategoryDishGet> categoryDishes = (await _categoryDishService.GetCategoryDishs()).Adapt<List<CategoryDishGet>>();

            return categoryDishes;
            
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<CategoryDishGet>> GetFindById(string Id)
        {
            var catdish = await _categoryDishService.GetCategoryDish(Id);

            if (catdish == null)
            {
                return NotFound("No se encontro ninguna categoria");
            }


            CategoryDishGet catDishGet = catdish.Adapt<CategoryDishGet>();

            return catDishGet;
        }


        [HttpPut("{Id}")]

        public async Task<ActionResult<CategoryDishGet>> Update(string Id, [FromBody]  CategoryDishPut value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updateCatDish = await _categoryDishService.GetCategoryDish(Id);
            if (updateCatDish == null)
            {
                return NotFound("No se encontro ninguna categoria");
            }
           

            updateCatDish.NameCatDish = value.NameCatDish;
            await _categoryDishService.UpdateCategoryDish(updateCatDish);
            var getCatDish = (await _categoryDishService.GetCategoryDish(Id)).Adapt<CategoryDishGet>();


            return StatusCode(200, getCatDish);

        }

        [HttpPost]

        public async Task<ActionResult<CategoryDishGet>> Create([FromBody] CategoryDishCreate categoryDish)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //CREANDO CATEGORIA PLATO
            var newCatDish = categoryDish.Adapt<CategoryDish>();

            await _categoryDishService.CreateCategoryDish(newCatDish);

            var getCatDish = (await _categoryDishService.GetCategoryDish(newCatDish.Id)).Adapt<CategoryDishGet>();


            return StatusCode(201, getCatDish);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id)
        {

            var categoryDish = await _categoryDishService.GetCategoryDish(Id);
         
            if (categoryDish == null)
            {
                return BadRequest("No se encontro ninguna categoria");
            }

            await _categoryDishService.DeteleCategoryDish(categoryDish);
            return NoContent();



        }

    }
}
