using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Orders.Data;
using Orders.Models;
using Orders.StartUp;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Services.ConfigureServices(builder);
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
        builder.Services.AddAuthorization();

        

        var app = builder.Build();
        app.ConfigureSwagger();
        
        app.MapProductEndpoints();
        app.MapOrderEndpoints();
        app.MapPost("api/security/createToken", [AllowAnonymous] (User user) =>
        {
            if (user.UserName == "kodin" && user.Password =="kodin")
            {
                var issuer = builder.Configuration["Jwt:Issuer"];
                var audience = builder.Configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jstToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(stringToken); 
            }
            return Results.Unauthorized();
        });
        // Load Products
        app.UseAuthentication();
        app.UseAuthorization();
        app.Run();
    }
}