using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrustBankAPI.User;
using TrustBankApp.Models;
using TrustBankApp.Pages;
using TrustBankApp.Services;

namespace TrustBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private UserCredentials _userCredentials;
        private PasswordHasher<UserModel> _passwordHasher;

        public LoginController(IConfiguration config, UserCredentials userCredentials, PasswordHasher<UserModel> passwordHasher)
        {
            _config = config;
            _userCredentials = userCredentials;
            _passwordHasher = passwordHasher;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User Not Found");
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            
            var currentUser = _userCredentials.GetUsers()
                .First(x => x.UserName == userLogin.UserName);


            if (currentUser != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, userLogin.Password);

                if (result == PasswordVerificationResult.Success)
                    return currentUser;
                else
                    return null;
            }
            return null;
        }
        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
