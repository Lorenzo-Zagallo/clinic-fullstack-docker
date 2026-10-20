using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
	public interface ITokenService
	{
		string GerarToken(Usuario usuario);
	}
}