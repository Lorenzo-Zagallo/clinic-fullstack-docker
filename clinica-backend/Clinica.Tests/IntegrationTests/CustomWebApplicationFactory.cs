using Clinica.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Clinica.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("Kaxoxa_Testes_123!")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext original registado no Program.cs
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ClinicaDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Adiciona o DbContext apontando para o contentor descartável
                services.AddDbContext<ClinicaDbContext>(options =>
                {
                    options.UseSqlServer(_msSqlContainer.GetConnectionString());
                });
            });
        }

        public async Task InitializeAsync()
        {
            // Inicia o contentor antes de os testes correrem
            await _msSqlContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            // Destrói o contentor no final
            await _msSqlContainer.DisposeAsync();
        }
    }
}