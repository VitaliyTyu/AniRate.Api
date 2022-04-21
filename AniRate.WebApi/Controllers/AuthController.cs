using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence;
using AniRate.WebApi.Models;
using AniRate.WebApi.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly ApplicationDbContext _dbContext;

        public AuthController(IOptions<AuthOptions> authOptions, ApplicationDbContext dbContext)
        {
            _authOptions = authOptions;
            _dbContext = dbContext;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(loginViewModel);
            }

            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.UserName == loginViewModel.Username && u.Password == loginViewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return BadRequest(loginViewModel);
            }

            if (user != null)
            {
                var token = GenerateJWT(user);

                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerViewModel);
            }

            var user = new Account
            {
                Password = registerViewModel.Password,
                UserName = registerViewModel.Username,
                Id = Guid.NewGuid(),
            };

            var sameUser = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (sameUser != null)
            {
                return BadRequest("Имя занято");
            }

            await _dbContext.Accounts.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var token = GenerateJWT(user);

            return Ok(new { access_token = token });
        }


        private string GenerateJWT(Account user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
