using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOps.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoriesService categoriesService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoriesService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await categoriesService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO newCategory)
        {
            var category = await categoriesService.CreateCategory(newCategory);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryDTO category)
        {
            try
            {
                var updatedCategory = await categoriesService.UpdateCategory(id, category);
                if (updatedCategory == null)
                {
                    return BadRequest();
                }
                return Ok(updatedCategory);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await categoriesService.DeleteCategory(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
