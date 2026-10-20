using Clinica.Domain.Entities;
using Clinica.Domain.Interfaces;
using Clinica.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Repositories
{
    public class UsuarioRepository(ClinicaDbContext dbContext) : IUsuarioRepository
    {
        public async Task<Usuario?> ObterPorNomeUsuarioAsync(string nomeUsuario)
        {
            return await dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == nomeUsuario);
        }
    }
}