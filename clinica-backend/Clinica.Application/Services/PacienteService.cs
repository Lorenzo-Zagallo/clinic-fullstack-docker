using Clinica.Application.DTOs;
using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Clinica.Domain.Interfaces;

namespace Clinica.Application.Services
{
    public class PacienteService(
        IPacienteRepository pacienteRepository,
        IConsultaRepository consultaRepository) : IPacienteService
    {
        public async Task<Paciente> CadastrarPacienteAsync(CreatePacienteDTO dto)
        {
            // A construção da entidade agora garante a sua validade
            var paciente = new Paciente(dto.Nome, dto.CPF, dto.ValorConsulta);
            return await pacienteRepository.CriarAsync(paciente);
        }

        public async Task<List<Paciente>> ListarPacientesAsync()
        {
            return await pacienteRepository.ObterTodosAsync();
        }

        public async Task<bool> AtualizarPacienteAsync(int pacienteId, CreatePacienteDTO dto)
        {
            var pacienteExistente = await pacienteRepository.ObterPorIdAsync(pacienteId);
            if (pacienteExistente == null) return false;

            // Utilizamos o método de comportamento da entidade
            pacienteExistente.AtualizarDados(dto.Nome, dto.CPF, dto.ValorConsulta);

            await pacienteRepository.AtualizarAsync(pacienteExistente);
            return true;
        }

        public async Task<bool> DeletarPacienteAsync(int pacienteId)
        {
            var pacienteExistente = await pacienteRepository.ObterPorIdAsync(pacienteId);
            if (pacienteExistente == null) return false;

            await pacienteRepository.DeletarAsync(pacienteExistente);
            return true;
        }

        public async Task<DashboardDTO> ObterDadosDashboardAsync()
        {
            var total = await pacienteRepository.ContarTotalAsync();
            var faturamentoTotal = await consultaRepository.FaturamentoTotal();
            var totalConsultas = await consultaRepository.TotalConsultas();

            var ticketMedio = totalConsultas > 0 ? faturamentoTotal / totalConsultas : 0;

            return new DashboardDTO
            {
                TotalPacientes = total,
                FaturamentoTotal = faturamentoTotal,
                MediaValorConsulta = Math.Round(ticketMedio, 2)
            };
        }
    }
}