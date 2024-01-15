using Domain.Dtos.JWT;
using Domain.Models.JWT;


namespace Domain.Interface.JWT
{
    public interface IAuthServices
    {
        Auth AutenticateUser(Auth user);

        string GenerateToken(Auth user);

        Task createUser(AuthDto user);

        string IsAdminOrNot(Auth user);
        Task<Auth> loginUser(AuthDto user);
    }
}
