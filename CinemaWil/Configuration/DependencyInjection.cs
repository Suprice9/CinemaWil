using Domain.Interface;
using Domain.Interface.JWT;
using Infractructure.Services;
using Infractructure.Services.JWT;

namespace CinemaWil.Configuration
{
    public static class DependencyInjection
    {
        public static void GetDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IBillboardServices, BillboardServices>();
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IAuthServices, AuthServices>();
        }
    }
}
