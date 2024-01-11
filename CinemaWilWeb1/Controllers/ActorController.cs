using CinemaWilWeb.ViewModel;
using CinemaWilWeb1.Interfase;
using Microsoft.AspNetCore.Mvc;


namespace CinemaWilWeb1.Controllers
{
    public class ActorController : Controller
    {
        private readonly IApiConsume _ApiConsume;

        public ActorController(IApiConsume ApiConsume)
        {
            _ApiConsume = ApiConsume;
        }

        Uri baseAddress = new Uri("https://localhost:7048/api/Actor/GetActors");

        public async Task<ActionResult> Index()
        {
            var actors = await GetActors();
            return View(actors);
        }


        public async Task<List<ActorViewModel>>GetActors()
        {
            var Apiconsume =await _ApiConsume.GetAutorization(HttpContext);

            var actors = await _ApiConsume.GetActors(baseAddress,Apiconsume);

            return actors;
           
        }


        // GET: ActorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
