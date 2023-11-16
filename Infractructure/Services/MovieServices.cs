using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractructure.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly DataBaseContext _dbContext;

        public MovieServices(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
       

        public async Task<List<Movie>> GetMovies()
        {
            var movies = await _dbContext.Movie.ToListAsync();

            return movies;
        }

        public async Task MapMovieObject(MovieDto movieD)
        {
            var result = movieD.Adapt<Movie>();
            await _dbContext.Movie.AddAsync(result);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> UpdateMovie(int e, MovieDto movie)
        {

            var movieDb = await _dbContext.Movie.FindAsync(e);
            if (movieDb != null)
            {
                movieDb.Title = movie.Title;
                movieDb.Genre = movie.Genre;

                _dbContext.Update(movieDb);

                await _dbContext.SaveChangesAsync();

                return "Actualizado exitosamente";
            }
            else
            {
                return "Pelicula no encontrada";
            }
        }

        public async Task<string> DeleteMovie(int id)
        {
            var movie = await _dbContext.Movie.FindAsync(id);

            if (movie != null)
            {
                _dbContext.Remove(movie);
                await _dbContext.SaveChangesAsync();
                return "Se elimino correctamente";
            }
            else
            {
                return "Hubo un error";
            }
        }

       
    }
}
