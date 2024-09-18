using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface ICategoriesService
    {
        public Task<Category> CreateCategory(CategoryDTO category);
        public Task<Category> GetCategory(Guid id);
        public Task<List<Category>> GetCategories();
        public Task<Category> UpdateCategory(Guid id, CategoryDTO category);
        public Task DeleteCategory(Guid id);
    }
}
