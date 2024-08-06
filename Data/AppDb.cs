using BakeryOps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions options) : base(options) { }
        public AppDb() : base() { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Navigation(e => e.OrderItems).AutoInclude();
            modelBuilder.Entity<OrderItem>().Navigation(item => item.Product).AutoInclude();
        }

    }
}
