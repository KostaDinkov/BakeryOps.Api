using Microsoft.EntityFrameworkCore;
namespace Orders.Models
{
    public class OrdersDB: DbContext
    {
        public OrdersDB(DbContextOptions options): base(options){}
        public DbSet<Product> Products { get; set; } = null!;
    }
}
