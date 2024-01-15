using Domain.Dtos.JWT;
using Domain.Interface.JWT;
using Domain.Models.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using Infractructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infractructure.Services.JWT
{
    public class AuthServices : IAuthServices
    {
        private IConfiguration _configuration;

        private readonly DataBaseContext _dbContext;

        public AuthServices(IConfiguration configuration, DataBaseContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public Auth AutenticateUser(Auth user)
        {
          

            if (user.Admin == true)
            {
                return user;
            }
            else{
                return null;
            }

           
        }

        public async Task createUser(AuthDto user)
        {
            var result = user.Adapt<Auth>();
            await _dbContext.Auths.AddAsync(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Auth> loginUser(AuthDto user)
        {
            try
            {
                var UserDb = await _dbContext.Auths.FirstAsync(p=>p.Name==user.Name && p.PassCode==user.PassCode);
                if (UserDb is null)
                { 
                    return null; 
                }
                else{
                    return UserDb;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public string IsAdminOrNot(Auth User){

         var UserPosition = User.Admin;

            if (UserPosition is false)
            {
                return "No encontrado";

            }
            else
            {
                if (UserPosition is true)
                {
                    return "Es admin";
                }
                else
                {
                    return "No es Admin";
                }
            }
        }

        public string GenerateToken(Auth user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "user_name"),
                new Claim(JwtRegisteredClaimNames.Email, "user_email"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role","admin"),
                new Claim(ClaimTypes.NameIdentifier,"admin") };
         

            var token = new JwtSecurityToken(_configuration["Jw:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
