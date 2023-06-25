using Publink.Rest.Models;

namespace Publink.Rest.Interfaces.Services
{
	public interface ITokenService
	{
		string GenerateToken(User user);
	}
}
