using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using project_backend.Services;
using project_backend.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace project_backend.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
  
        private readonly IDish _dishService;
        private readonly ICategoryDish _categorydishService;


        public DishController(IDish disService, ICategoryDish categoryDishService)
        {
            _dishService = disService;
            _categorydishService = categoryDishService;

        }

        [HttpGet]
        public async Task<IEnumerable<DishGet>> GetDishs()
        {

            List<DishGet> dish = (await _dishService.GetDishs()).Adapt<List<DishGet>>();


            return dish;
              
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<DishGet>> getFindById(string Id)
        {
            var dish = await _dishService.GetDish(Id);
            
            if (dish == null)
            {
                return NotFound();
            }
         

            DishGet dishGet = dish.Adapt<DishGet>();
            
            return dishGet;
        }

        [HttpPut("{Id}")]

        public async Task<IActionResult> Update(string Id, [FromBody] DishPut value )
        {

        

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updateDish = await _dishService.GetDish(Id);
            if (updateDish == null)
            {
                return NotFound("no se encontre ningun plato");
            }
          
           
            if (updateDish.CategoryDishId != value.CategoryDishId)
            {
                //Validar categoria de plato
                var newCategoryDish = await _categorydishService.GetCategoryDish(value.CategoryDishId);
                if (newCategoryDish == null)
                {
                    return BadRequest("No existe el tipo  de plato");
                }
            }


            updateDish.NameDish = value.NameDish;
            updateDish.PriceDish = value.PriceDish;
            updateDish.CategoryDishId = value.CategoryDishId;
            updateDish.ImgDish = value.ImgDish;
            await _dishService.UpdateDish(updateDish);
            var getDish = (await _dishService.GetDish(Id)).Adapt<DishGet>();

            return StatusCode(200, getDish);
               

        }

        

        [HttpPost]
        public async Task<ActionResult<DishPrincipal>> PostDish([FromBody] DishCreate dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Validar categoria
            var cate = await _categorydishService.GetCategoryDish(dish.CategoryDishId);
            if (cate == null)
            {
                return BadRequest("No existe la categoria");
            }

            //CREANDO PLATO
            var newDish = dish.Adapt<Dish>();
            
            await _dishService.CreateDish(newDish);

            var getDish = (await _dishService.GetDish(newDish.Id)).Adapt<DishGet>();
            return StatusCode(201, getDish);
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var dish = await _dishService.GetDish(Id);
            if (dish == null) {
                return NotFound();
               
            }
            await _dishService.DeteleDish(dish);
            return NoContent();          

        }


    }
}
        