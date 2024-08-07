using System.Text;
using System.Text.Json.Serialization;
using BakeryOps.API.Data;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BakeryOps.API.StartUp
{
    public static class DependencyInjectionSetup
    {
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            

            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            //services.AddSqlite<AppDb>(connectionString);
            builder.Services.AddSqlServer<AppDb>(connectionString);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BakeryOps", Description = "Application for bakery management", Version = "v1" });
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IClientsService, ClientsService>();
            builder.Services.AddScoped<IUsersService, UserService>();

            builder.Services.AddAuthentication(options =>
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

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                //logging.RequestHeaders.Add("sec-ch-ua");
                //logging.ResponseHeaders.Add("MyResponseHeader");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;

            });

            builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.SetIsOriginAllowed((origin) =>
                            {
                                if (origin.StartsWith("http://192.168.1.") || origin.StartsWith("https://192.168.1."))
                                {
                                    return true;
                                }
                                else if (origin.Contains("www.bakeryops.home") || origin.StartsWith("http://bakeryops.home") || origin.StartsWith("https://bakeryops.home"))
                                {
                                    return true;
                                }
                                else if (origin.Contains("localhost"))
                                {
                                    return true;
                                }
                                return false;
                            })
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                            
                        });
                }
            );
            builder.Services.AddAuthorization();
            builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddSignalR();


        }
    }
}
