using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class MaterialsService(AppDb dbContext, IMapper mapper) : IMaterialsService
    {
        public async Task<Material[]> GetMaterialsAsync()
        {
            return await dbContext.Materials.ToArrayAsync();
        }

        public async Task<Material?> GetMaterialByIdAsync(Guid materialId)
        {
           return await dbContext.Materials.FindAsync(materialId);
        }

        public async Task<Material> CreateMaterialAsync(Material newMaterial)
        {
            var result = new Material
            {
                Name = newMaterial.Name,
                Description = newMaterial.Description,
                Unit = newMaterial.Unit,
                
            };

            dbContext.Materials.Add(result);
            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Material> UpdateMaterialAsync(Material update)
        {
            var material = await dbContext.Materials.FindAsync(update.Id);
            if(material == null)
            {
                throw new DbServiceException($"Material {update.Id} not found");
            }
           

           
            

            if(material == null)
            {
                throw new DbServiceException($"Material {update.Id} not found");
            }
            
            dbContext.Materials.Update(material);
            await dbContext.SaveChangesAsync();
            return material;
        }

        public async Task DeleteMaterialAsync(Guid materialId)
        {
            var material = await dbContext.Materials.FindAsync(materialId);
            if(material == null)
            {
                throw new DbServiceException($"Material {materialId} not found");
            }
            dbContext.Materials.Remove(material);
            await dbContext.SaveChangesAsync();
        }
    }
}
