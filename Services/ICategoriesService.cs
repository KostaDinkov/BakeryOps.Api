using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface ICategoriesService
    {
        public Task<CategoryDTO> CreateCategory(CategoryDTO category);
        public Task<CategoryDTO?> GetCategory(Guid id);
        public Task<List<CategoryDTO>> GetCategories();
        public Task<CategoryDTO?> UpdateCategory( CategoryDTO category);
        public Task<bool> DeleteCategory(Guid id);
    }
}
