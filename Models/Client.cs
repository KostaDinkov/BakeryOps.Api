namespace Orders.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public bool IsCompany { get; set; } = false;
        public bool IsSpecialPrice { get; set; } = false;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        

    }
}
