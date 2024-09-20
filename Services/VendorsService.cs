using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class VendorsService(AppDb appDb, IMapper mapper) : ICrudService<VendorDTO>
    {

        public async Task<VendorDTO?> Create(VendorDTO vendor)

        {
            var v = mapper.Map<VendorDTO, Vendor>(vendor);
            appDb.Vendors.Add(v);
            await appDb.SaveChangesAsync();
            return vendor;
        }


        public async Task<VendorDTO?> GetById(Guid id)
        {
            var vendor = await appDb.Vendors.FindAsync(id);
            return vendor == null ? null : mapper.Map<VendorDTO>(vendor);
        }

        public async Task<List<VendorDTO>> GetAll()
        {
            var vendors = await appDb.Vendors.Where(v=>v.IsDeleted != true).ToListAsync();
            return vendors.Select(mapper.Map<VendorDTO>).ToList();
        }

        public async Task<VendorDTO?> Update(VendorDTO vendor)
        {
            var existingVendor = await appDb.Vendors.FindAsync(vendor.Id);
            
            if (existingVendor == null)
            {
                return null;
            }
            appDb.Entry(existingVendor).CurrentValues.SetValues(vendor);
            await appDb.SaveChangesAsync();
            return mapper.Map<VendorDTO>(existingVendor);
        }

        public async Task<bool> Delete(Guid id)
        {
            var vendor = await appDb.Vendors.Include(v=>v.Materials).FirstOrDefaultAsync(v=>v.Id == id);
            if (vendor == null)
            {
                return false;

            }
            if(vendor.Materials.Any())
            {
                vendor.IsDeleted = true;
            }
            else
            {
                appDb.Vendors.Remove(vendor);
            }
            await appDb.SaveChangesAsync();
            return true;
        }
    }
}
