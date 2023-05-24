using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin login)
        {
            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            return NotFound("User not found");
        }

        private string GenerateJSONWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        _configuration["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserLogin login)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

    }
}
