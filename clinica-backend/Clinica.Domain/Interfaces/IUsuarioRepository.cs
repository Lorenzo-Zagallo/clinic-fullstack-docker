using Clinica.Domain.Entities;

namespace Clinica.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorNomeUsuarioAsync(string nomeUsuario);
    }
}