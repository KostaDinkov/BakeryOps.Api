using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services;
using BakeryOps.API.Models;
public interface IMaterialsService
{
    Task<MaterialDTO[]> GetMaterialsAsync();
    Task<MaterialDTO?> GetMaterialByIdAsync(Guid materialId);
    Task<MaterialDTO> CreateMaterialAsync(MaterialDTO MaterialDTO);
    Task<MaterialDTO> UpdateMaterialAsync(MaterialDTO update);
    Task DeleteMaterialAsync(Guid materialId);
}

