using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence;
using AniRate.Infrastructure.Services;
using AniRate.WebApi.Models;
using AniRate.WebApi.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AniRate.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly ApplicationDbContext _dbContext;
        private readonly HashService _hashService;

        public AuthController(
            IOptions<AuthOptions> authOptions, 
            ApplicationDbContext dbContext,
            HashService hashService)
        {
            _authOptions = authOptions;
            _dbContext = dbContext;
            _hashService = hashService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(loginViewModel);
            }

            var passworsHash = _hashService.CalculateMD5Hash(loginViewModel.Password);

            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.UserName == loginViewModel.Username && u.Password == passworsHash);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return BadRequest(loginViewModel);
            }

            if (user != null)
            {
                var token = GenerateJWT(user);

                return Ok(token);
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

            var passworsHash = _hashService.CalculateMD5Hash(registerViewModel.Password);

            var user = new Account
            {
                Password = passworsHash,
                UserName = registerViewModel.Username,
                Id = Guid.NewGuid(),
            };

            var sameUser = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (sameUser != null)
            {
                return Conflict("Имя занято");
            }

            await _dbContext.Accounts.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var token = GenerateJWT(user);

            return Ok(token);
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
