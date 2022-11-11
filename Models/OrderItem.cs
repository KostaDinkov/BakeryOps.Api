namespace Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public double ProductAmount { get; set; }

        public string Description { get; set; }

        public string CakeFoto { get; set; }
        public string CakeTitle { get; set; }

        public bool IsInProgress { get; set; }

        public bool IsComplete { get; set; }

        public virtual Order Order { get; set; }
        public virtual int OrderId { get; set; }
    }
}
