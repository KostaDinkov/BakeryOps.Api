namespace Orders.Models.DTOs
{
    public class OrderItemDTO
    {

        public int ProductId { get; set; }

        public double ProductAmount { get; set; }

        public string Description { get; set; }

        public string CakeFoto { get; set; }
        public string CakeTitle { get; set; }

        public bool IsInProgress { get; set; } = false;
        

        public bool IsComplete { get; set; } = false;
    }
}
