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
    public class VendorsController(IVendorsService vendorsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            var vendors = await vendorsService.GetVendors();
            return Ok(vendors);
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor(Guid id)
        {
            var vendor = await vendorsService.GetVendor(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor(VendorDTO newVendor)
        {
            var vendor = await vendorsService.CreateVendor(newVendor);
            return Ok(vendor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVendor(VendorDTO update)
        {
            var vendor = await vendorsService.UpdateVendor(update);
            if (vendor == null)
            {
                return NotFound();
            }


            return Ok(vendor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            if (await vendorsService.DeleteVendor(id))
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