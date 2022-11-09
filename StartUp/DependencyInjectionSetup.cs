using Microsoft.OpenApi.Models;
using Orders.Models;

namespace Orders.StartUp
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection ConfigureServices (this IServiceCollection services,WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Orders") ?? "Data Source=Orders.db";
            builder.Services.AddSqlite<OrdersDB>(connectionString);
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Description = "Application for customer orders", Version = "v1" });
            });

            return services;
        }
    }
}
