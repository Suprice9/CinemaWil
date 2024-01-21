using CinemaWilWeb.ViewModel;
using CinemaWilWeb1.Interfase;
using Domain.Dtos;
using Domain.Interface;
using Domain.Models;
using Infractructure.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace CinemaWilWeb1.Services
{
    public class ApiCosumeServices : IApiConsume
    {

        private readonly IActorServices _ActorServices;

        public ApiCosumeServices(IActorServices actorServices) {
            _ActorServices = actorServices; 
        }

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

        public async Task<Actor> GetActorsById(Uri baseAddress, HttpClient autorization, int userId)
        {
            var url = baseAddress;
            if (autorization is not null)
            {
                var jsonStr =  await autorization.GetStringAsync(url);

                var res = JsonConvert.DeserializeObject<Actor>(jsonStr);
                //Arreglar el delete para que funcione
                return res;

            }
            else
            {
                return null;
            }
        }


        public async Task<HttpResponseMessage> CreateActor(Uri baseAddress, HttpClient autorization, ActorDto newActor)
        {
            var url = baseAddress;

            var stringActor = newActor.ToString();
            if (autorization is not null)
            {

                var jsonStr = await autorization.PostAsJsonAsync(url, newActor);
                return jsonStr;
            }
            else
            {
                return null;
            }

        }

        public async Task<HttpResponseMessage> UpdateActor(string baseAdress, HttpClient autorization,ActorDto actor, int id)
        {
            var url = baseAdress;

            if (autorization is not null)
            {
                var response =await autorization.PutAsJsonAsync(url+id,actor);
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteActor(Uri baseAddress, HttpClient autorization)
        {
            var url = baseAddress;

            if (autorization is not null)
            {
                //Crear Metodo para eliminar 
                var jsonStr = await autorization.DeleteAsync(url);
                return jsonStr;
            }
            else
            {
                return null;
            }
           
        }



    }
}
