namespace BakeryOps.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public double ProductAmount { get; set; }

        public string Description { get; set; }

        public string CakeFoto { get; set; }
        public string CakeTitle { get; set; }

        public bool IsInProgress { get; set; } = true;

        public bool IsComplete { get; set; } = false;

        public decimal ItemUnitPrice { get; set; } 
        
        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
    }
}
