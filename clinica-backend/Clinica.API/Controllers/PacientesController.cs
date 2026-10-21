using Clinica.API.Middlewares;
using Clinica.Application.DTOs;
using Clinica.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.API.Controllers
{
    // O controller pede o Service, que pede o Repository, que pede o Banco.
    // Tudo conectado automaticamente no Program.cs pelo .NET via Injeção de Dependência do ASP.NET Core DI Container
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController(IPacienteService pacienteService) : ControllerBase
    {
        // POST: api/pacientes
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter<CreatePacienteDTO>))] // <-- Filtro de validação injetado
        public async Task<IActionResult> CadastrarPaciente([FromBody] CreatePacienteDTO dto)
        {
            // Se o JSON vier vazio ou errado, o [ApiController] já valida automaticamente
            var pacienteCriado = await pacienteService.CadastrarPacienteAsync(dto);

            // Retorna status 201 (Created) com: o paciente criado no corpo da resposta e o local para acessar esse paciente via GET
            return CreatedAtAction(nameof(CadastrarPaciente), new { id = pacienteCriado.Id }, pacienteCriado);
        }

        // GET: api/pacientes
        [HttpGet]
        public async Task<IActionResult> ListarPacientes()
        {
            var pacientes = await pacienteService.ListarPacientesAsync();
            return Ok(pacientes);
        }

        // PUT: api/pacientes/{pacienteId}
        [HttpPut("{pacienteId}")]
        public async Task<IActionResult> AtualizarPaciente(int pacienteId, [FromBody] CreatePacienteDTO dto)
        {
            var sucesso = await pacienteService.AtualizarPacienteAsync(pacienteId, dto);

            if (!sucesso) return NotFound(); // Retorna 404 se o paciente não existir

            return NoContent(); // Retorna 204 se a atualização foi bem-sucedida
        }

        // DELETE: api/pacientes/{pacienteId}
        [HttpDelete("{pacienteId}")]
        public async Task<IActionResult> DeletarPaciente(int pacienteId)
        {
            var sucesso = await pacienteService.DeletarPacienteAsync(pacienteId);

            if (!sucesso) return NotFound(); // Retorna 404 se o paciente não existir

            return NoContent(); // Retorna 204 se a deleção foi bem-sucedida
        }

        // GET: api/pacientes/dashboard
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var dados = await pacienteService.ObterDadosDashboardAsync();
            return Ok(dados);
        }
    }
}