using Biblioteca_digital.Database.Repositorios;
using Biblioteca_digital.Interfaces;
using Biblioteca_digital.Servicios;
using Microsoft.AspNetCore.Authentication;

namespace Biblioteca_digital.Database.config
{
    public static class AddRepositorio
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReporitorioBase<>), typeof(RepositorioBase<>));

            return services;
        }

        public static IServiceCollection AddAutneticatorServices(this IServiceCollection services)
        {

            services.AddScoped<IServicioToken, ServicioToken>();
            services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();


            return services;
        }
    }
}
