using Publink.Rest.Models;

namespace Publink.Rest.Interfaces.Services
{
	public interface IAuthService
	{
        User? GetUser(string email, string password);
	}
}
