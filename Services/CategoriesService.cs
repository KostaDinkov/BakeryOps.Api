using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class CategoriesService(AppDb db): ICategoriesService
    {
        public async Task<Category> CreateCategory(CategoryDTO category)
        {
            var newCategory = new Category
            {
                Name = category.Name
            };
            db.Categories.Add(newCategory);
            await db.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(Guid id, CategoryDTO category)
        {
            var existingCategory = await db.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }

            existingCategory.Name = category.Name;
            await db.SaveChangesAsync();
            return existingCategory;
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();
        }
    }
}
