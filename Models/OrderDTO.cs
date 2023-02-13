namespace Orders.Models
{
    public class OrderDTO
    {
       public int Id { get; set; }
        public int OperatorId { get; set; }
        
        public DateTime PickupDate { get; set; }

        
        public string PickupTime { get; set; }

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }

        public bool IsPaid { get; set; }

        public decimal AdvancePaiment { get; set; }

        public Status Status { get; set; } = Status.Incomplete;

        

        // OrderItems
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
