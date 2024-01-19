using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Domain.Dtos.JWT;
using Domain.Interface.JWT;

namespace CinemaWilWeb1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthServices _authServices;

        public LoginController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        public ActionResult LoginCreate()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>LoginCreate(AuthDto user)
        {
            try
            { 
                var httpClient = new HttpClient();

                var UserDb = await _authServices.loginUser(user);

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(UserDb), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:7048/api/auth/Login", stringContent);

                string token = await response.Content.ReadAsStringAsync();

                var UserPosition =  _authServices.IsAdminOrNot(UserDb);


                if (UserPosition== "Es admin")
                {  
                    HttpContext.Session.SetString("JWToken", token);

                    return Redirect("~/Actor/Index");
                }
                else if (UserPosition == "No es Admin")
                {
                    return Redirect("~/Dashboard/Index");
                }
                else
                {
                        ViewBag.Message = "Incorrect UserName or Password";
                        return Redirect("~/Login/LoginCreate");
                }
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();//Clean token
            return Redirect("~/Login/LoginCreate");
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public async Task<ActionResult> CreateUserPost(AuthDto user)
        {
           await _authServices.createUser(user);
            return Redirect("~/Login/LoginCreate");     
        }


    }
}
