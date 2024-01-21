using CinemaWilWeb.ViewModel;
using CinemaWilWeb1.Interfase;
using Domain.Dtos;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CinemaWilWeb1.Controllers
{
    public class ActorController : Controller
    {
        private readonly IApiConsume _ApiConsume;


        public ActorController(IApiConsume ApiConsume)
        {
            _ApiConsume = ApiConsume;

        }

        Uri UrlGetUser = new Uri("https://localhost:7048/api/Actor/GetActors");
        Uri urlGetUserId = new Uri("https://localhost:7048/api/Actor/GetActorsById/");
        Uri UrlPostUser = new Uri("https://localhost:7048/api/Actor/AddActor");
        string urlPutUser = "https://localhost:7048/api/Actor/UpdateActor/";
        Uri urlDeleteUser = new Uri("https://localhost:7048/api/Actor/DeleteActor/");


        public async Task<ActionResult> Index()
        {
            var actors = await GetActors();
            return View(actors);
        }


        public async Task<List<ActorViewModel>> GetActors()
        {
            var Apiconsume = await _ApiConsume.GetAutorization(HttpContext);

            var actors = await _ApiConsume.GetActors(UrlGetUser, Apiconsume);

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
        public async Task<ActionResult> Create(ActorDto actor)
        {
            try
            {
                var Apiconsume = await _ApiConsume.GetAutorization(HttpContext);
                var actorAdd = await _ApiConsume.CreateActor(UrlPostUser, Apiconsume, actor);

                if (actorAdd.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Actor Created.";
                    return Redirect("~/Actor/Index");

                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }

        // GET: ActorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                ActorDto actor = new ActorDto();

                var apiConsume = await _ApiConsume.GetAutorization(HttpContext);

                var response = apiConsume.GetAsync("https://localhost:7048/api/Actor/GetActorsById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    actor = JsonConvert.DeserializeObject<ActorDto>(data);
                }
                return View(actor);

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: ActorController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Edit(int id,ActorDto actor)
        {
            try
            {
                var apiConsume = await _ApiConsume.GetAutorization(HttpContext);

                var actorUpdated = await _ApiConsume.UpdateActor(urlPutUser,apiConsume,actor, id);

                if (actorUpdated.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Actor Updated.";
                    return Redirect("~/Actor/Index");

                }
                else
                {
                    return StatusCode(400);
                }


            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: ActorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ActorDto actor = new ActorDto();

                var apiConsume = await _ApiConsume.GetAutorization(HttpContext);

                var response = apiConsume.GetAsync("https://localhost:7048/api/Actor/GetActorsById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    actor = JsonConvert.DeserializeObject<ActorDto>(data);
                }
                return View(actor);

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        // POST: ActorController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            { 
                var apiConsume = await _ApiConsume.GetAutorization(HttpContext);

                var response = apiConsume.DeleteAsync("https://localhost:7048/api/Actor/DeleteActor/" + id).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Actor deleted.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View(Delete);
            }
            return View(Delete);
        }
    }
}
