using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialsController(IMaterialsService materialsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            var materials = await materialsService.GetMaterialsAsync();
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(Guid id)
        {
            var material = await materialsService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaterial(Material newMaterial)
        {
            var material = await materialsService.CreateMaterialAsync(newMaterial);
            return Ok(material);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(Guid id, Material material)
        {
            try
            {
                var updatedMaterial = await materialsService.UpdateMaterialAsync(material);
                if (updatedMaterial == null)
                {
                    return BadRequest();
                }
                return Ok(updatedMaterial);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            try
            {
                await materialsService.DeleteMaterialAsync(id);
                return Ok();
            }
            catch (DbServiceException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
    }
}
