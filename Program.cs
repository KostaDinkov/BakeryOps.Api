
using Orders.StartUp;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Orders.Hubs;
using Orders.Data;

namespace Orders
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            builder.Services.ConfigureServices(builder);
            
            builder.Services.AddAuthorization();
            builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddSignalR();


            var app = builder.Build();
            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<OrdersDB>();
                context.Database.EnsureCreated();
            }
            app.ConfigureSwagger();
            app.UseHttpLogging();
            app.UseCors(DependencyInjectionSetup.MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<EventHub>("/eventHub");
            app.MapControllers();

            app.Run();
        }
    }
}
