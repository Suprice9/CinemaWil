using CinemaWilWeb.ViewModel;
using Domain.Dtos;
using Domain.Models;

namespace CinemaWilWeb1.Interfase
{

        public interface IApiConsume
        {
        Task<HttpClient> GetAutorization(HttpContext httpContext);

        Task<List<ActorViewModel>> GetActors(Uri baseAddress, HttpClient autorization);

        Task<Actor> GetActorsById(Uri baseAddress, HttpClient autorization, int userId);

        Task<HttpResponseMessage> CreateActor(Uri baseAddress, HttpClient autorization, ActorDto actor);

        Task<HttpResponseMessage> DeleteActor(Uri baseAddress, HttpClient autorization);
        }
    
}
