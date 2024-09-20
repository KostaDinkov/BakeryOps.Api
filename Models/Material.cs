using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryOps.API.Models
{
    public class Material
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Unit { get; set; }
        
        public  Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}
