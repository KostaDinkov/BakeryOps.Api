using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BakeryOps.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController(IConfiguration config, AppDb dbContext) : Controller
    {
        [HttpPost]
        public  async Task<IActionResult> GetToken(UserCredentialsDTO credentials)
        {
            //TODO Fix for production!!!
            var user = new User();
            var pHasher = new PasswordHasher<User>();
            
            if (credentials.UserName == "admin" && credentials.Password == "admin")
            {
                user = new User
                {
                    UserName = "admin",
                    HashedPassword = pHasher.HashPassword(user, "admin"),
                    Permissions = SecurityUtils.GetApiPermissions().Select(s=> new Permission(s)).ToList()
                };
            }
            else
            {
                user = dbContext.Users.Include(u => u.Permissions).FirstOrDefault(u => u.UserName == credentials.UserName);
            }
            
            if (user == null)
            {
                return Unauthorized();
            }
            
            
            if (pHasher.VerifyHashedPassword(user, user.HashedPassword, credentials.Password) == PasswordVerificationResult.Success)
            {
                var issuer = config["Jwt:Issuer"];
                var audience = config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, credentials.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, credentials.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        
                    }),
                    
                    
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                tokenDescriptor.Subject.AddClaims(user.Permissions.Select(p => new Claim("Permission", p.Name)));

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                //var jstToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();

        }



    }
}
