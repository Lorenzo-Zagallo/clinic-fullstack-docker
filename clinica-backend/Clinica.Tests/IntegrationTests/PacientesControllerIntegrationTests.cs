using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Clinica.Application.DTOs;
using FluentAssertions;

namespace Clinica.Tests.IntegrationTests
{
    public class PacientesControllerIntegrationTests(
        CustomWebApplicationFactory factory) 
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        private async Task<string> AutenticarComoAdminAsync()
        {
            var loginDto = new LoginDTO { NomeUsuario = "admin", Senha = "Kaxoxa_123!" };
            var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            return result!.Token;
        }

        [Fact]
        public async Task CadastrarPaciente_DeveRetornarCreated_QuandoAutenticadoEDadosValidos()
        {
            // Arrange: Autenticação
            var token = await AutenticarComoAdminAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var novoPaciente = new CreatePacienteDTO
            {
                Nome = "Paciente de Integração",
                CPF = "123.456.789-00",
                ValorConsulta = 250.00m
            };

            // Act: Chama o Endpoint real
            var response = await _client.PostAsJsonAsync("/api/pacientes", novoPaciente);

            // Assert: Valida a pipeline completa (Controller -> Validação -> Serviço -> DB)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Contain("Paciente de Integração");
        }

        [Fact]
        public async Task CadastrarPaciente_DeveRetornarUnauthorized_QuandoNaoTemToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = null;
            var novoPaciente = new CreatePacienteDTO { Nome = "Teste", CPF = "123", ValorConsulta = 100 };

            // Act
            var response = await _client.PostAsJsonAsync("/api/pacientes", novoPaciente);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}