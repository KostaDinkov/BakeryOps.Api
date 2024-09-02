using System.ComponentModel.DataAnnotations;

namespace BakeryOps.API.Models
{
    public class Material
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Unit { get; set; }

        public Guid? VendorId { get; set; }
        
    }
}
