using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class CategoriesService(AppDb db, IMapper mapper) : ICrudService<CategoryDTO>
    {
        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            var newCategory = mapper.Map<Category>(categoryDTO);

            db.Categories.Add(newCategory);
            await db.SaveChangesAsync();
            return mapper.Map<CategoryDTO>(newCategory);
        }

        public async Task<CategoryDTO?> GetById(Guid id)
        {
            var result = await db.Categories.FindAsync(id);
            if (result == null || result.IsDeleted)
            {
                return null;
            }

            return mapper.Map<CategoryDTO>(result);
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var result = await db.Categories.Where(c => c.IsDeleted != true).ToListAsync();
            return result.Select(mapper.Map<CategoryDTO>).ToList();
        }

        public async Task<CategoryDTO?> Update(CategoryDTO category)
        {
            var existingCategory = await db.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }

            mapper.Map(category, existingCategory);
            await db.SaveChangesAsync();
            return mapper.Map<CategoryDTO>(existingCategory);
        }

        public async Task<bool> Delete(Guid id)
        {
            var category = await db.Categories.Include(c=>c.Materials).FirstOrDefaultAsync(c=>c.Id == id);
            if (category == null)
            {
                return false;
            }
            else if (category.Materials.Any())
            {
                category.IsDeleted = true;
            }
            else
            {
                db.Categories.Remove(category);
            }
            await db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}