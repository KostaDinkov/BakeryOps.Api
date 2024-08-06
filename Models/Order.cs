namespace BakeryOps.API.Models
{
    public enum Status
    {
        Complete,
        Incomplete,

    }

    public class Order
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PickupDate { get; set; }
        //public string PickupTime { get; set; }

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }

        public Client? Client { get; set; }
        public int? ClientId { get; set; } 

        public bool IsPaid { get; set; }

        public decimal AdvancePaiment { get; set; }

        public Status Status { get; set; } = Status.Incomplete;

        // OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
