using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class MaterialsService(AppDb appDb, IMapper mapper) : IMaterialsService
    {
        public async Task<MaterialDTO[]> GetMaterialsAsync()
        {
            var materials = await appDb.Materials.Include(m => m.Category).Include(m => m.Vendor).ToArrayAsync();
            //convert from Material array to MaterialDTO array
            var materialsDTO = materials.Select(m => mapper.Map<MaterialDTO>(m)).ToArray();
            return materialsDTO;
        }

        public async Task<MaterialDTO?> GetMaterialByIdAsync(Guid materialId)
        {
            var material = await appDb.Materials.FindAsync(materialId);
            return mapper.Map<Material, MaterialDTO>(material);
        }

        public async Task<MaterialDTO> CreateMaterialAsync(MaterialDTO newMaterial)
        {
            var result = new Material
            {
                Name = newMaterial.Name,
                Description = newMaterial.Description,
                Unit = newMaterial.Unit,
                CategoryId = newMaterial.CategoryId,
                Category = appDb.Categories.FirstOrDefault(c=>c.Id == newMaterial.CategoryId),
                VendorId = newMaterial.VendorId,
                Vendor = appDb.Vendors.FirstOrDefault(v=>v.Id == newMaterial.VendorId)
            };
            

            appDb.Materials.Add(result);
            await appDb.SaveChangesAsync();
            return mapper.Map<MaterialDTO>(result);
        }

        public async Task<MaterialDTO> UpdateMaterialAsync(MaterialDTO update)
        {
            var material = await appDb.Materials.FindAsync(update.Id);
            if(material == null)
            {
                throw new DbServiceException($"Material {update.Id} not found");
            }
            
            material.Name = update.Name;
            material.Description = update.Description;
            material.Unit = update.Unit;
            material.CategoryId = update.CategoryId;
            material.Category = appDb.Categories.FirstOrDefault(c=>c.Id == update.CategoryId);
            material.VendorId = update.VendorId;
            material.Vendor = appDb.Vendors.FirstOrDefault(v=>v.Id == update.VendorId);

            appDb.Materials.Update(material);
            await appDb.SaveChangesAsync();
            return mapper.Map<MaterialDTO>(material); ;
        }

        public async Task DeleteMaterialAsync(Guid materialId)
        {
            var material = await appDb.Materials.FindAsync(materialId);
            if(material == null)
            {
                throw new DbServiceException($"Material {materialId} not found");
            }
            appDb.Materials.Remove(material);
            await appDb.SaveChangesAsync();
        }
    }
}
