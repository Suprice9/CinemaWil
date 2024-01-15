using Domain.Interface.JWT;
using Infractructure.Services.JWT;
using CinemaWilWeb1.Interfase;
using CinemaWilWeb1.Services;

namespace CinemaWilWeb1.Configuration
{
    public static class DependencyInjectionWeb
    {
        public static void GetDependencyInjectionsWeb(this IServiceCollection services)
        {
            services.AddScoped<IApiConsume, ApiCosumeServices>();
            services.AddScoped<IAuthServices, AuthServices>();
        }
    }
}
