using Microsoft.OpenApi.Models;
using Orders.ProductsDB;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
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
        app.MapGet("/products/{id}", (int id) => ProductsDB.GetProduct(id));
        app.MapGet("products", () => ProductsDB.GetProducts());
        app.MapPost("products", (Product product) => ProductsDB.CreateProduct(product));
        app.MapPut("products", (Product product) => ProductsDB.UpdateProduct(product));
        app.MapDelete("products/{id}", (int id) => ProductsDB.DeleteProduct(id));

        app.Run();
    }
}