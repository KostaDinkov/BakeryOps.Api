using Orders.Models;
using Microsoft.EntityFrameworkCore;
using Orders.Data;

namespace Orders.StartUp;

public static class MapEndpoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
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
            product.PriceDrebno = update.PriceDrebno;
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

        app.MapGet("/syncDatabase", async(OrdersDB db) => { 
            await ProductsLoader.SyncProductsData(db); 
            return Results.Ok();
        });
        return app;
    }
}
