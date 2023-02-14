using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Orders.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SecurityController : Controller
    {

        ILogger logger;
        IConfiguration config;
        public SecurityController( IConfiguration config )
        {
            
            this.config= config;
        }

        [HttpPost]
        public  async Task<IActionResult> GetToken(UserCredentialsDTO credentials)
        {
            if (credentials.UserName == "kodin" && credentials.Password == "kodin")
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jstToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(stringToken);
            }
            return Unauthorized();

        }



    }
}
