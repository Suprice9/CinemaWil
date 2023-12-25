using Domain.Dtos;
using Domain.Interface;
using Infractructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace CinemaWil.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
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
        /// <summary>
        /// Retorna todos los actores.
        /// </summary>
        /// <response code="200">OK. Se retornaron correctamente los actores.</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="524">DatabaseEmpty. La Base de datos esta vacia.</response>
        [HttpGet]
        public async Task<IActionResult> GetActors()
        {

            return Ok(await _actorServices.GetActors());
        }


        /// <summary>
        /// Crear un actor.
        /// </summary>
        /// <response code="200">OK. Se Creo correctamente el actor.</response>        
        /// <response code="400">BadRequest. Se han encontrado datos incorrectos.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
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

        /// <summary>
        /// Modificar actor
        /// </summary>
        /// <param name="code">id de cartelera.</param>
        /// <param name="actor">Datos requeridos para la modificacion de una cartelera</param>
        /// <response code="200">OK. Se modifico correctamente el actor</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound. El id del actor no existe.</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateActor(int code,ActorDto actor)
        {
            try
            {
               var result= await _actorServices.UpdateActor(code, actor);

                if (result != "Actor no encontrado")
                {
                    return Ok(result);
                }
                else
                {
                    {
                        return BadRequest(result);
                    }
                }

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }
        }


        /// <summary>
        /// Eliminar actor.
        /// </summary>
        /// <param name="Code">Datos requeridos para la eliminacion de un actor.</param>
        /// <response code="200">OK. Se elimino correctamente la cartelera.</response> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound.El actor no existe</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
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
                        return BadRequest(404);
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

