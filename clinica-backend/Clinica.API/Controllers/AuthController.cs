using Clinica.Application.DTOs;
using Clinica.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var resultado = await authService.LoginAsync(dto);

            if (resultado == null)
                return Unauthorized(new { erro = "Usuário ou senha inválidos" });

            return Ok(resultado);
        }

        // O endpoint /registrar foi COMENTADO. A injeção de administrador agora é via EF Core Seed.
        // POST: api/auth/registrar
        // (Endpoint auxiliar apenas para criarmos o primeiro usuário de teste)
        //[HttpPost("registrar")]
        //public async Task<IActionResult> Registrar([FromBody] LoginDTO dto)
        //{
        //    var usuario = new Usuario
        //    {
        //        NomeUsuario = dto.NomeUsuario,
        //        Senha = dto.Senha
        //    };

        //    _clinicaDbContext.Usuarios.Add(usuario);
        //    await _clinicaDbContext.SaveChangesAsync();

        //    return Ok("Usuário criado com sucesso!");
        //}
    }
}