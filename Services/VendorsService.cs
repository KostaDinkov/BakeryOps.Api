using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class VendorsService(AppDb appDb, IMapper mapper) : IVendorsService
    {


        public async Task<VendorDTO> CreateVendor(VendorDTO vendor)

        {
            var v = mapper.Map<VendorDTO, Vendor>(vendor);
            appDb.Vendors.Add(v);
            await appDb.SaveChangesAsync();
            return vendor;
        }



        public async Task<VendorDTO> GetVendor(Guid id)
        {
            var vendor = await appDb.Vendors.FindAsync(id);
            if (vendor == null)
            {
                throw new Exception("Vendor not found");
            }
            return mapper.Map<VendorDTO>(vendor);
        }

        public async Task<List<VendorDTO>> GetVendors()
        {
            var vendors = await appDb.Vendors.ToListAsync();
            return vendors.Select(mapper.Map<VendorDTO>).ToList();
        }

        public async Task<VendorDTO> UpdateVendor(VendorDTO vendor)
        {
            var existingVendor = await appDb.Vendors.FindAsync(vendor.Id);
            if (existingVendor == null)
            {
                throw new Exception("Vendor not found");
            }
            existingVendor.Name = vendor.Name;
            await appDb.SaveChangesAsync();
            return mapper.Map<VendorDTO>(existingVendor);
        }

        public async Task DeleteVendor(Guid id)
        {
            var vendor = await appDb.Vendors.FindAsync(id);
            if (vendor == null)
            {
                throw new Exception("Vendor not found");
            }
            appDb.Vendors.Remove(vendor);
            await appDb.SaveChangesAsync();
        }
    }
}
