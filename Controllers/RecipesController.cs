using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOps.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class RecipesController(ICrudService<RecipeDTO> recipesService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await recipesService.GetAll();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(Guid id)
        {
            var recipeDto = await recipesService.GetById(id);
            if (recipeDto == null)
            {
                return NotFound();
            }
            return Ok(recipeDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeDTO newRecipe)
        {
            var recipeDto = await recipesService.Create(newRecipe);
            return Ok(recipeDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(RecipeDTO update)
        {
            try
            {
                var updatedRecipe = await recipesService.Update(update);
                if (updatedRecipe == null)
                {
                    return NotFound();
                }
                return Ok(updatedRecipe);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            try
            {
                await recipesService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
