using Microsoft.AspNetCore.Mvc;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto.Users;
using Publink.Rest.Services;

namespace Publink.Rest.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly ITokenService _tokenService;

		public AuthController(IAuthService authService, ITokenService tokenService)
		{
			_authService = authService;
			_tokenService = tokenService;
		}

		[HttpPost]
		public async Task<IActionResult> Authenticate([FromBody] UserLoginDto model)
		{
			var user = _authService.GetUser(model.Email, model.Password);

			if (user == null)
			{
				return NotFound(new { message = "Invalid email or password" });
			}

			var token = _tokenService.GenerateToken(user);

			return Ok(new
			{
				user = new UserResponseDto
				{
					Id = user.Id,
					Username = user.Username
				},
				token
			});
		}
	}
}
