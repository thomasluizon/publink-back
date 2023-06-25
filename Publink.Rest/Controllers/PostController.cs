using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publink.Rest.Interfaces.Services;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class PostController : ControllerBase
	{
		private readonly IPostService _postService;
		private readonly ITokenService _tokenService;

		public PostController(IPostService postService, ITokenService tokenService)
		{
			_postService = postService;
			_tokenService = tokenService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRandomPosts()
		{
			var posts = await _postService.GetAllRandom();

			return Ok(posts);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PostDto post)
		{
			var res = await _postService.Create(post);

			return Ok(res);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetByIdAndRandom([FromRoute] int id)
		{
			var res = await _postService.GetByIdAndRandom(id, 4);

			if (!res.Any())
			{
				return NotFound(res);
			}

			return Ok(res);
		}
	}
}
