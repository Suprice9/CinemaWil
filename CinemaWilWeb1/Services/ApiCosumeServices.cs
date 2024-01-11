using CinemaWilWeb.ViewModel;
using CinemaWilWeb1.Interfase;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CinemaWilWeb1.Services
{
    public class ApiCosumeServices : IApiConsume
    {
        public async Task<HttpClient> GetAutorization(HttpContext httpContext)
        {
            var accessToken = httpContext.Session.GetString("JWToken").Remove(0, 10);
            if (accessToken != null)
            {

                var cleanToken = RemoveEnd(accessToken, 2);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", cleanToken);

                return client;
            }
            else
            {
                return null;
            }
        }
        private static String RemoveEnd(String str, int len)
                {
                    if (str.Length < len)
                    {
                        return string.Empty;
                    }

                    return str[..^len];
                }

        public async Task<List<ActorViewModel>> GetActors(Uri baseAddress,HttpClient autorization)
        {
            var url = baseAddress;
            if (autorization is not null)
            {

                string jsonStr = await autorization.GetStringAsync(url);

                var res = JsonConvert.DeserializeObject<List<ActorViewModel>>(jsonStr).ToList();

                return res;
            }else
            {
                return null;
            }
        }
      
    }
}
