
namespace Orders.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }= String.Empty;
    public string Barcode { get; set; }= String.Empty;
    public string Category { get; set; } = String.Empty;
    public decimal PriceDrebno { get; set; } = 0;
    public decimal PriceEdro { get; set; } = 0;

    public string? Code { get; set; }

    public DateTime DateCreated { get; set; }
}


