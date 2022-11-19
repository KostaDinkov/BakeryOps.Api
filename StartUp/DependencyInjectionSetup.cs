using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orders.Models;
using System.Text;

namespace Orders.StartUp
{
    public static class DependencyInjectionSetup
    {
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("Orders") ?? "Data Source=Orders.db";

            services.AddSqlite<OrdersDB>(connectionString);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Description = "Application for customer orders", Version = "v1" });

            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            

            services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.WithOrigins("http://localhost", "https://localhost:3000", "http://localhost:3000")
                            .AllowAnyHeader();
                        });
                }
            );

            return services;
        }
    }
}
