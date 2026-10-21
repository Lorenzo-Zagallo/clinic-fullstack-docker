using Clinica.Application.Interfaces;
using Clinica.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Clinica.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IAuthService, AuthService>();

            // Regista todos os validadores que estão neste projeto
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}