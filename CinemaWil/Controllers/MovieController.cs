using Domain.Dtos;
using Domain.Interface;
using Infractructure.Data;
using Infractructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWil.Controllers
{
    [Authorize]
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

        [Authorize]
        [HttpGet]
        //Documentar
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _movieServices.GetMovies());
        }

        [Authorize]
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

        [Authorize]
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
