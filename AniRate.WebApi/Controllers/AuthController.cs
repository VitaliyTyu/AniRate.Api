using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence;
using AniRate.Infrastructure.Services;
using AniRate.WebApi.Models;
using AniRate.WebApi.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Логин
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<UserInfo>> Login([FromBody] LoginViewModel loginViewModel)
        {
            var userInfo = new UserInfo("", "", "");

            if (!ModelState.IsValid)
            {
                userInfo = new UserInfo("", "", "Ошибка валидации");
                return BadRequest(userInfo);
                //return BadRequest(loginViewModel);
            }

            var emailHash = _hashService.CalculateMD5Hash(loginViewModel.EmailAddress);
            var passworsHash = _hashService.CalculateMD5Hash(loginViewModel.Password);

            var user = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.EmailAddress == emailHash && u.Password == passworsHash);

            if (user == null)
            {
                userInfo = new UserInfo("", "", "Пользователь не найден");
                return BadRequest(userInfo);
                //ModelState.AddModelError(string.Empty, "User not found");
                //return BadRequest(loginViewModel);
            }

            userInfo = GenerateJWT(user);
            return Ok(userInfo);
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserInfo>> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var userInfo = new UserInfo("", "", "");

            if (!ModelState.IsValid)
            {
                userInfo = new UserInfo("", "", "Ошибка валидации");
                return BadRequest(userInfo);
                //return BadRequest(registerViewModel);
            }

            var emailHash = _hashService.CalculateMD5Hash(registerViewModel.EmailAddress);
            var passwordHash = _hashService.CalculateMD5Hash(registerViewModel.Password);

            var user = new Account
            {
                Password = passwordHash,
                EmailAddress = emailHash,
                Name = registerViewModel.Name,
                Id = Guid.NewGuid(),
            };

            var sameUser = await _dbContext.Accounts.SingleOrDefaultAsync(u => u.EmailAddress == user.EmailAddress);

            if (sameUser != null)
            {
                userInfo = new UserInfo("", "", "Адрес электронной почты занят");
                return Conflict(userInfo);
                //return Conflict("Адрес электронной почты занят");
            }

            await _dbContext.Accounts.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            userInfo = GenerateJWT(user);

            return Ok(userInfo);
        }


        /// <summary>
        /// Проверка на авторизованность
        /// </summary>
        [HttpGet("check")]
        [Authorize]
        public async Task<IActionResult> Check()
        {
            return Ok();
        }


        private UserInfo GenerateJWT(Account user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };


            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(authParams.TokenLifetime)),
                //expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            var userInfo = new UserInfo(encodedToken, user.Name, "");

            return userInfo;
        }
    }
}
