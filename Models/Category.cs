namespace BakeryOps.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}
