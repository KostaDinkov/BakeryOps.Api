namespace BakeryOps.API.Models.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public DateOnly LastUpdated { get; set; }
        public required Product Product { get; set; }
        public Guid ProductId { get; set; }
        public string? Description { get; set; } = null;

        public List<RecipeMaterial> Ingredients { get; set; } = new List<RecipeMaterial>();
        public List<SubRecipe> SubRecipes { get; set; } = new List<SubRecipe>();
        public double WorkHours { get; set; }

        public double Yield { get; set; }
  
    }
}