
using Orders.StartUp;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Orders.Hubs;

namespace Orders
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureServices(builder);
            builder.Services.AddAuthorization();
            builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddSignalR();


            var app = builder.Build();
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
