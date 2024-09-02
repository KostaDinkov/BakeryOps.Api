namespace BakeryOps.API.Models.DTOs
{
    public class MaterialDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Unit { get; set; }

        
    }
}
