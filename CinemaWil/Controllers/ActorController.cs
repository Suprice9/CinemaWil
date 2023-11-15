using Domain.Dtos;
using Domain.Interface;
using Infractructure.Data;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CinemaWil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        private readonly IActorServices _actorServices;

        public ActorController(DataBaseContext dbContext, IActorServices actorServices)
        {
            _dbContext = dbContext;
            _actorServices = actorServices;
        }

        [HttpGet]
        //Documentar
        public async Task<IActionResult> GetActors()
        {

            return Ok(await _actorServices.GetActors());
        }

        [HttpPost]
        public async Task<IActionResult> AddActor(ActorDto addActor)
        {
            try
            {
                
                if (addActor is null)
                {
                    return BadRequest(400);
                }
                else
                {
                    await _actorServices.MapActorObject(addActor);
                }
                return Ok("Se ha creado correctamente");

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActor(int Code)
        {
            try
            {
                var result = await _actorServices.DeleteActor(Code);

                if (result != "Hubo un error")
                {
                    return Ok(result);
                }
                else
                {
                    {
                        return BadRequest();
                    }
                }

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }
    }

}

