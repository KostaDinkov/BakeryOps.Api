using Microsoft.OpenApi.Models;
using Orders.ProductsDB;
using Microsoft.EntityFrameworkCore;
using Orders.Models;
using Product = Orders.Models.Product;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<OrdersDB>(options => options.UseInMemoryDatabase("items"));
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Description = "Application for customer orders", Version = "v1" });
        });
        

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
        });


        app.MapGet("/", () => "Hello World!");
        
        app.MapGet("/products", async (OrdersDB db) => await db.Products.ToListAsync());
        app.MapPost("/products", async (OrdersDB db, Product product) =>
        {
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return Results.Created($"/products/{product.Id}", product);
        });
        app.MapGet("/products/{id}", async (OrdersDB db, int id) => await db.Products.FindAsync(id));

        app.MapPut("/products/{id}", async (OrdersDB db, Product update, int id) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null) return Results.NotFound();
            product.Name = update.Name;
            product.Price = update.Price;
            product.Code = update.Code;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/products/{id}", async (OrdersDB db, int id) =>
        {
            var product = await db.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NoContent();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
        

        app.Run();
    }
}