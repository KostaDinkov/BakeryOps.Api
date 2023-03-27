
using System.ComponentModel.DataAnnotations;

namespace Orders.Models;

public class Product
{
    [Key]
    public Guid Id{ get; set; }
    public string Name { get; set; }= String.Empty;
   
    public string Category { get; set; } = String.Empty;
    public decimal PriceDrebno { get; set; } = 0;
    public decimal PriceEdro { get; set; } = 0;
    public bool HasDiscount { get; set; } = false;
    public bool KeepPriceDrebno { get; set; } = false;

    public bool inPriceList { get; set; } = true;
    public string Unit { get; set; } = String.Empty;

    public string? Code { get; set; }

    public DateTime DateCreated { get; set; }
}


