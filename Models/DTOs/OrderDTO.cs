using System.ComponentModel.DataAnnotations;

namespace Orders.Models.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        
        public int? OperatorId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? PickupDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public int? ClientId { get; set; }

        public bool? IsPaid { get; set; }

        public decimal? AdvancePaiment { get; set; }

        public Status? Status { get; set; } 

        // OrderItems
        public ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
