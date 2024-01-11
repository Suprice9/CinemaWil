using CinemaWilWeb.ViewModel;

namespace CinemaWilWeb1.Interfase
{

        public interface IApiConsume
        {
        Task<HttpClient> GetAutorization(HttpContext httpContext);

        Task<List<ActorViewModel>> GetActors(Uri baseAddress, HttpClient autorization);
        }
    
}
