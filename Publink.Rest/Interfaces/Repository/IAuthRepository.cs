using Publink.Rest.Models;

namespace Publink.Rest.Interfaces.Repository
{
    public interface IAuthRepository
    {
        User? GetUser(string email, string password);
    }
}
