using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;

namespace Publink.Rest.Services
{
	public class TokenService : ITokenService
	{
		private readonly Jwt _jwt;

		public TokenService(IOptions<Jwt> jwt)
		{
			_jwt = jwt.Value;
		}

		public string GenerateToken(User user)
		{
			var key = Encoding.ASCII.GetBytes(_jwt.Secret);

			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Expires = DateTime.UtcNow.AddHours(8),

				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature
				),

				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role.ToString()),
				})
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
