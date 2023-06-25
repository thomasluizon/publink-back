using System.Security.Cryptography;
using System.Text;

namespace Publink.Rest.Extensions
{
	public static class Auth
	{
		public static string ToHash(this string password)
		{
			var passwordBytes = Encoding.Default.GetBytes(password);

			var hashedPassword = SHA256.HashData(passwordBytes);

			return Convert.ToHexString(hashedPassword);
		}

		public static string WithSalt(this string password, string salt)
				=> password + salt;
	}
}
