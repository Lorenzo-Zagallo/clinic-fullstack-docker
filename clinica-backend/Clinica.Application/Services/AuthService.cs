using Clinica.Application.DTOs;
using Clinica.Application.Interfaces;
using Clinica.Domain.Interfaces;

namespace Clinica.Application.Services
{
    public class AuthService(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService) : IAuthService
    {
        public async Task<LoginResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var usuario = await usuarioRepository.ObterPorNomeUsuarioAsync(dto.NomeUsuario);

            // Fail-fast: Se o usuário não existir ou o hash for incompatível, retorna nulo.
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
            {
                return null;
            }

            return new LoginResponseDTO
            {
                Usuario = usuario.NomeUsuario,
                Token = tokenService.GerarToken(usuario)
            };
        }
    }
}