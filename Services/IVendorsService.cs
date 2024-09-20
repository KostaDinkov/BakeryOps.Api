using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface IVendorsService
    {
        public Task<VendorDTO> CreateVendor(VendorDTO vendor);
        public Task<VendorDTO> GetVendor(Guid id);
        public Task<List<VendorDTO>> GetVendors();
        public Task<VendorDTO?> UpdateVendor( VendorDTO vendor);
        public Task<bool> DeleteVendor(Guid id);
    }
}
