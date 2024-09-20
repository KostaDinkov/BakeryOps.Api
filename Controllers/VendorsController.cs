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
    public class VendorsController(ICrudService<VendorDTO> vendorsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            var vendors = await vendorsService.GetAll();
            return Ok(vendors);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor(Guid id)
        {
            var vendor = await vendorsService.GetById(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor(VendorDTO newVendor)
        {
            var vendor = await vendorsService.Create(newVendor);
            return Ok(vendor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVendor(VendorDTO update)
        {
            var vendor = await vendorsService.Update(update);
            if (vendor == null)
            {
                return NotFound();
            }


            return Ok(vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            if (await vendorsService.Delete(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}