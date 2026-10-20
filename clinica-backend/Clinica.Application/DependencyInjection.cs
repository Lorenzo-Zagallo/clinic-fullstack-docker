using Clinica.Application.Interfaces;
using Clinica.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clinica.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}