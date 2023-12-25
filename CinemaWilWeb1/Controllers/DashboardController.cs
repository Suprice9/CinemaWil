using Microsoft.AspNetCore.Mvc;

namespace CinemaWilWeb1.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
