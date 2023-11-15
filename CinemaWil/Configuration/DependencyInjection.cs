using Domain.Interface;
using Infractructure.Services;

namespace CinemaWil.Configuration
{
    public static class DependencyInjection
    {
        public static void GetDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<IBillboardServices, BillboardServices>();
            services.AddScoped<IMovieServices, MovieServices>();
        }
    }
}
