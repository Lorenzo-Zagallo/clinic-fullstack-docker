using Clinica.Application.DTOs;

namespace Clinica.Application.Interfaces
{
	public interface IAuthService
	{
		Task<LoginResponseDTO?> LoginAsync(LoginDTO dto);
	}
}