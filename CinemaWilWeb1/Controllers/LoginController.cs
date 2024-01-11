using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.JWT;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace CinemaWilWeb1.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult LoginCreate()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>LoginCreate(Auth user)
        {
            try
            {
                var httpClient = new HttpClient();
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://localhost:7048/api/auth/Login", stringContent);

                string token = await response.Content.ReadAsStringAsync();
                if (token=="Invalid credentials")
                {
                    ViewBag.Message = "Incorrect UserId or Password";
                    return Redirect("~/Login/LoginCreate");
                }
                HttpContext.Session.SetString("JWToken", token);

                return Redirect("~/Dashboard/Index");
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
    }
}
