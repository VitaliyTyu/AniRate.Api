using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence;
using AniRate.WebApi.Models;
using AniRate.WebApi.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public AuthController(
            IOptions<AuthOptions> authOptions, 
            ApplicationDbContext dbContext)
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

            var passworsHash = CalculateMD5Hash(loginViewModel.Password);

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

            var passworsHash = CalculateMD5Hash(registerViewModel.Password);

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

        private string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
