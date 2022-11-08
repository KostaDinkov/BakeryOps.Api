namespace Orders.ProductsDB;

public record Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}

public class ProductsDB
{
    private static List<Product> products = new List<Product>()
    {
        new Product { ID = 1, Name = "Torta", Price = 10},
        new Product { ID = 2, Name = "Ekler", Price = 3},
        new Product { ID = 3, Name = "Sladki", Price = 2.5m}
    };
}
