using UserMovie.Services;

namespace UserMovie.Service.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
