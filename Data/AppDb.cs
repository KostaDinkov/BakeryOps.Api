using BakeryOps.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Data
{
    public class AppDb(DbContextOptions<AppDb> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public  DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Delivery> Deliveries { get; set; } = null!;
        public DbSet<DeliveryItem> DeliveryItems { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;


        public DbSet<Permission> Permissions { get; set; } = null!;




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Navigation(e => e.OrderItems).AutoInclude();
            modelBuilder.Entity<OrderItem>().Navigation(item => item.Product).AutoInclude();

           
        }

    }
}
