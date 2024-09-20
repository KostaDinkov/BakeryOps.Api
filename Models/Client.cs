using System.ComponentModel.DataAnnotations;

namespace BakeryOps.API.Models
{
    public class Client
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsCompany { get; set; } = false;
        public bool IsSpecialPrice { get; set; } = false;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        
    }
}
