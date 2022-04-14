using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class AuthOptions
    {
        public string Issuer { get; set; } //тот кто сгенерировал токен
        public string Audience { get; set; } //тот для кого предназначен токен
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
