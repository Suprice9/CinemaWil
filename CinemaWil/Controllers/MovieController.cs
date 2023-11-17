using Domain.Dtos;
using Domain.Interface;
using Infractructure.Data;
using Infractructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWil.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        private readonly IMovieServices _movieServices;

        public MovieController(DataBaseContext dbContext, IMovieServices movieServices)
        {
            _dbContext = dbContext;
            _movieServices = movieServices;
        }

        /// <summary>
        /// Retorna todas las peliculas.
        /// </summary>
        /// <response code="200">OK. Se retornaron correctamente las peliculas.</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="524">DatabaseEmpty. La Base de datos esta vacia.</response>
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _movieServices.GetMovies());
        }


        /// <summary>
        /// Crear una pelicula.
        /// </summary>
        /// <response code="200">OK. Se Creo correctamente la pelicula.</response>        
        /// <response code="400">BadRequest. Se han encontrado datos incorrectos.</response>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDto addMovie)
        {
            try
            {

                if (addMovie is null)
                {
                    return BadRequest(400);
                }
                else
                {
                    await _movieServices.MapMovieObject(addMovie);
                }
                return Ok("Se ha creado correctamente");

            }
            catch (Exception e)
            {

                return StatusCode(500, e);
            }

        }

        /// <summary>
        /// Modificar cartelera
        /// </summary>
        /// <param name="code">id de la pelicula.</param>
        /// <param name="movie">Datos requeridos para la modificacion de una pelicula</param>
        /// <response code="200">OK. Se modifico correctamente la pelicula</response>        
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound. El id de la pelicula no existe.</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(int code, MovieDto movie)
        {
            try
            {
                var result = await _movieServices.UpdateMovie(code, movie);

                if (result != "Pelicula no encontrada")
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
        /// Eliminar pelicula.
        /// </summary>
        /// <param name="Code">Datos requeridos para la eliminacion de una pelicula.</param>
        /// <response code="200">OK. Se elimino correctamente la cartelera.</response> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="404">NotFound. La pelicula no existe</response>        
        /// <response code="500">Internal Server Error. Ocurrio un error imprevisto.</response>
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int Code)
        {
            try
            {
                var result = await _movieServices.DeleteMovie(Code);

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
