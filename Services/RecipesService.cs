using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class RecipesService(AppDb appDb, IMapper mapper):ICrudService<RecipeDTO>
    {
        public async Task<RecipeDTO?> Create(RecipeDTO recipeDto)
        {
            var recipe = mapper.Map<RecipeDTO, Recipe>(recipeDto);
            await appDb.Recipes.AddAsync(recipe);
            await appDb.SaveChangesAsync();
            return mapper.Map<RecipeDTO>(recipe);
        }

        public async Task<RecipeDTO?> GetById(Guid id)
        {
            var recipe = await appDb.Recipes.FindAsync(id);
            return mapper.Map<RecipeDTO>(recipe);
        }

        public async Task<List<RecipeDTO>> GetAll()
        {
            var recipes = await appDb.Recipes.ToListAsync();
            return recipes.Select(mapper.Map<RecipeDTO>).ToList();    
        }

        public async Task<RecipeDTO?> Update(RecipeDTO RecipeDTO)
        {
            var recipe = await appDb.Recipes.FindAsync(RecipeDTO.Id);
            
            if (recipe == null) return null;
            
            appDb.Entry(recipe).CurrentValues.SetValues(RecipeDTO);
            await appDb.SaveChangesAsync();
            return mapper.Map<RecipeDTO>(recipe);
        }

        public async Task<bool> Delete(Guid id)
        {
            var recipe = await appDb.Recipes.FindAsync(id);
            if(recipe == null) return false;
            appDb.Recipes.Remove(recipe);
            await appDb.SaveChangesAsync();
            return true;
        }
    }
}
