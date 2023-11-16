using Domain.Dtos.JWT;
using Domain.Models.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.JWT
{
    public interface IAuthServices
    {
        Auth AutenticateUser(AuthDto user);

        string GenerateToken(Auth user);
    }
}
