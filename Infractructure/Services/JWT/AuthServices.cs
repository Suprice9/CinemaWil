using Domain.Dtos.JWT;
using Domain.Interface.JWT;
using Domain.Models.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.Services.JWT
{
    public class AuthServices : IAuthServices
    {
        private IConfiguration _configuration;

        public AuthServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Auth AutenticateUser(AuthDto user)
        {
            Auth _user = null;
            if (user.Name=="admin" && user.PassCode=="admin")
            {
               _user=new Auth { Admin = true };
            }

            return _user; 
        }

        public string GenerateToken(Auth user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jw:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
