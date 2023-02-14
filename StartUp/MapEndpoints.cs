using Orders.Models;
using Microsoft.EntityFrameworkCore;
using Orders.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Orders.StartUp;

public static class MapEndpoints
{
    

    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/api/orders", async (OrdersDB db) =>
        {
            var orders = await db.Orders.Include(o => o.OrderItems).GroupBy(o=>o.PickupDate).ToListAsync();
            return orders;
        }).RequireAuthorization();

        

        return app;
    }
    public static WebApplication MapSecurityEndpoints(this WebApplication app, ConfigurationManager config)
    {
        //app.MapPost("api/security/getToken", [AllowAnonymous] (User user) =>
        //{
        //    if (user.UserName == "kodin" && user.Password == "kodin")
        //    {
        //        var issuer = config["Jwt:Issuer"];
        //        var audience = config["Jwt:Audience"];
        //        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //                new Claim("Id", Guid.NewGuid().ToString()),
        //                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            }),
        //            Expires = DateTime.UtcNow.AddMinutes(10),
        //            Issuer = issuer,
        //            Audience = audience,
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        //        };

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var jstToken = tokenHandler.WriteToken(token);
        //        var stringToken = tokenHandler.WriteToken(token);
        //        return Results.Ok(stringToken);
        //    }
        //    return Results.Unauthorized();
        //});
        return app;
    }




}
