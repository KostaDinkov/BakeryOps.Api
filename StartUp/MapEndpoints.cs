using Orders.Models;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using System.Globalization;

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

        app.MapGet("/syncDatabase", async (OrdersDB db) =>
        {
            await ProductsLoader.SyncProductsData(db);
            return Results.Ok();
        });
        return app;
    }

    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/api/orders/", async (OrdersDB db) =>
        {
            var orders = await db.Orders.ToListAsync();
            return orders;
        });
        app.MapGet("/api/orders/forDate({date})", async (OrdersDB db, string date) =>
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "dd-MM-yyyy";
            var dateParsed = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);

            var orders = await db.Orders.Where(order => order.PickupDate.Day == dateParsed.Day && order.PickupDate.Month == dateParsed.Month && order.PickupDate.Year == dateParsed.Year).ToListAsync();

            return orders;
        });

        app.MapGet("/api/orders/between({date1})and({date2})", async (OrdersDB db, string date1, string date2) =>
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "dd-MM-yyyy";
            var date1Parsed = DateTime.ParseExact(date1, format, CultureInfo.InvariantCulture);
            var date2Parsed = DateTime.ParseExact(date2, format, CultureInfo.InvariantCulture);

            var orders = await db.Orders.Where(order =>order.PickupDate<=date2Parsed && order.PickupDate>=date1Parsed).ToListAsync();

            return orders;
        });

        return app;
    }




}
