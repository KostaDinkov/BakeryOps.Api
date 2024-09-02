namespace BakeryOps.API.Models;

public class DeliveryItem
{
    public Guid Id { get; set; }
    public Guid DeliveryId { get; set; }
    public Delivery Delivery { get; set; }
    public Guid MaterialId { get; set; }
    public required Material Material{ get; set; }
    public double Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal VAT { get; set; } = 0.2m;

    public DateOnly? ExpirationDate { get; set; }
    public string? LotNumber { get; set; }

    public string? Notes { get; set; }
    
}