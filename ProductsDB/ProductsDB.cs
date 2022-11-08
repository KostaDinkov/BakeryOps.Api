namespace Orders.ProductsDB;

public record Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}

public class ProductsDB
{
    private static List<Product> products = new()
    {
        new Product { ID = 1, Name = "Torta", Price = 10 },
        new Product { ID = 2, Name = "Ekler", Price = 3 },
        new Product { ID = 3, Name = "Sladki", Price = 2.5m }
    };

    public static List<Product> GetProducts()
    {
        return products;
    }

    public static Product?  GetProduct(int id)
    {
        return products.SingleOrDefault(p => p.ID == id);
    }

    public static Product CreateProduct(Product product)
    {
        products.Add(product);
        return product;
    }

    public static Product UpdateProduct(Product update)
    {
        products = products.Select(p =>
        {
            if (p.ID == update.ID)
            {
                p.Name = update.Name;
                p.Price = update.Price;
            }

            return p;

        }).ToList();
        return update;
    }

    public static void DeleteProduct(int id)
    {
        products = products.FindAll(p=>p.ID != id).ToList();
    }
}

