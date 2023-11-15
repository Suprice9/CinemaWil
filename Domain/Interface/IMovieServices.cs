using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IMovieServices
    {
        Task<List<Movie>> GetMovies();
        Task MapMovieObject(MovieDto movieD);
        Task<string> DeleteMovie(int id);
    }
}
