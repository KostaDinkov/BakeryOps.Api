using Orders.Models;

namespace Orders.API.Models.DTOs
{
    public class ClientDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public bool IsCompany { get; set; } = false;
        public bool IsSpecialPrice { get; set; } = false;
        public  List<Order>? Orders { get; set; } 
    }
}
