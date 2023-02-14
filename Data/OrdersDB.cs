using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders.Data
{
    public class OrdersDB : DbContext
    {
        public OrdersDB(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Navigation(e => e.OrderItems).AutoInclude();
            modelBuilder.Entity<OrderItem>().Navigation(item => item.Product).AutoInclude();
        }

    }
}
