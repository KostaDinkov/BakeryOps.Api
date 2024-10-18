using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryOps.API.Models;

public class SubRecipe
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public Recipe Parent { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public decimal Quantity { get; set; }
}