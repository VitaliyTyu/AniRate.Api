using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AuthModels
{
    public class UserInfo
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }


        public UserInfo(string token, string userName, string errorMessage)
        {
            Token = token;
            UserName = userName;
            ErrorMessage = errorMessage;
        }
    }
}
