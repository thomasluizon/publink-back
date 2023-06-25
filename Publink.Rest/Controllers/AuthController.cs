using Microsoft.AspNetCore.Mvc;
using Publink.Rest.Enums;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models.Dto.Users;

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
			var loginResponse = await _authService.Login(model);

			if (loginResponse == null)
			{
				return NotFound(new { message = "Invalid email or password" });
			}

			return Ok(new
			{
				token = loginResponse.Item1,
				user = loginResponse.Item2
			});
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
		{
			var response = await _authService.Register(model);

			if (response.Item1)
				return Ok("User registered");

			return response.Item2 == ErrorTypes.DuplicateEntry ?
				BadRequest("User already registered")
				: StatusCode(500, "Error while registering user");
		}
	}
}
