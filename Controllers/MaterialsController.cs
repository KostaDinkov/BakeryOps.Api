using BakeryOps.API.Exceptions;
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
    public class MaterialsController(ICrudService<MaterialDTO> materialsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            var materials = await materialsService.GetAll();
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(Guid id)
        {
            var material = await materialsService.GetById(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> AddMaterial(MaterialDTO newMaterial)
        {
            var material = await materialsService.Create(newMaterial);
            return Created("",material);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMaterial(MaterialDTO material)
        {
            try
            {
                var updatedMaterial = await materialsService.Update(material);
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
            if (await materialsService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
