using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Orders.StartUp;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Services.ConfigureServices(builder);
        builder.Services.AddAuthorization();
        builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
               

        var app = builder.Build();
        app.ConfigureSwagger();
        app.UseCors(DependencyInjectionSetup.MyAllowSpecificOrigins);
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapProductEndpoints();
        app.MapOrderEndpoints();
        app.MapSecurityEndpoints(builder.Configuration);
  
        app.Run();
    }
}