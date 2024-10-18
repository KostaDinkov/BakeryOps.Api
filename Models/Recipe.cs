namespace BakeryOps.API.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public DateOnly LastUpdated { get; set; }
        public required Product Product { get; set; }
        public Guid ProductId { get; set; }
        public string? Description { get; set; } = null;

        public  ICollection<RecipeMaterial> Ingredients { get; set; } = new List<RecipeMaterial>();
        public  ICollection<SubRecipe> SubRecipes { get; set; } = new List<SubRecipe>();
        public double WorkHours { get; set; }

        public double Yield { get; set; }

        public List<RecipeMaterial> GetTotalIngredients()
        {
            Dictionary<string, RecipeMaterial> totalIngredients = new Dictionary<string, RecipeMaterial>();
            foreach (var ingredient in Ingredients)
            {
                if (!totalIngredients.TryAdd(ingredient.Material.Name, ingredient))
                {
                    totalIngredients[ingredient.Material.Name].Quantity += ingredient.Quantity;
                }
            }

            if (SubRecipes.Count == 0)
            {
                return totalIngredients.Values.ToList();
            }

            foreach (var subRecipe in SubRecipes)
            {
                var totalSubIngredients = subRecipe.Recipe.GetTotalIngredients();
                foreach (var ingredient in totalSubIngredients)
                {
                    if (!totalIngredients.TryAdd(ingredient.Material.Name, ingredient))
                    {
                        totalIngredients[ingredient.Material.Name].Quantity += ingredient.Quantity * subRecipe.Quantity;
                    }
                }
            }

            return totalIngredients.Values.ToList();
        }
    }
}