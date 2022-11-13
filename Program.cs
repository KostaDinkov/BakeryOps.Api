using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Orders.StartUp;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Services.ConfigureServices(builder);
        builder.Services.AddAuthorization();
               

        var app = builder.Build();
        app.ConfigureSwagger();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapProductEndpoints();
        app.MapOrderEndpoints();
        app.MapSecurityEndpoints(builder.Configuration);
  
        app.Run();
    }
}