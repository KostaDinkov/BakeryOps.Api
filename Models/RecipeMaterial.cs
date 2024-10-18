namespace BakeryOps.API.Models;

public class RecipeMaterial
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public Guid IngredientId { get; set; }
    public required Material Material{ get; set; }
    public decimal Quantity { get; set; }
        
}