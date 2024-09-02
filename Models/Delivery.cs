namespace BakeryOps.API.Models;

public  class Delivery
    {
        public Guid Id { get; set; }
       
        public DateOnly DeliveryDate { get; set; }
        public Guid VendorId { get; set; }
        public required Vendor Vendor { get; set; }
        public List<DeliveryItem> Items { get; set; }

        public string? InvoiceNumber { get; set; }
        public string? Notes { get; set; }
    }


