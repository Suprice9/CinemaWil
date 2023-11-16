using Domain.Dtos.JWT;
using Domain.Interface.JWT;
using Domain.Models.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWil.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthServices _authServices;
        public AuthController (IConfiguration configuration, IAuthServices authServices)
        {
            _authServices = authServices;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AuthDto auth)
        {
            try
            {
                IActionResult response = Unauthorized();

                var userAutentication = _authServices.AutenticateUser(auth);

                if (userAutentication != null && userAutentication.Admin==true)
                {
                    var token = _authServices.GenerateToken(userAutentication);
                    response = Ok(new { token = token } );
                }
                return response ;
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
            
        }
    }
}
