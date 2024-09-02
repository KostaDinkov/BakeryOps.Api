using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services;
using BakeryOps.API.Models;
public interface IMaterialsService
{
    Task<Material[]> GetMaterialsAsync();
    Task<Material?> GetMaterialByIdAsync(Guid materialId);
    Task<Material> CreateMaterialAsync(Material MaterialDTO);
    Task<Material> UpdateMaterialAsync(Material update);
    Task DeleteMaterialAsync(Guid materialId);
}

