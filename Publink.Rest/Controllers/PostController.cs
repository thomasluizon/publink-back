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
			var token = Request.Headers.Authorization.First();

			var userId = _tokenService.GetUserIdByToken(token);

			var res = await _postService.Create(post, userId);

			return Ok(res);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAndRandom([FromRoute] Guid id)
		{
			var res = await _postService.GetByIdAndRandom(id, 4);

			if (!res.Any())
			{
				return NotFound("Post not found");
			}

			return Ok(res);
		}

		[HttpGet("{userId:guid}")]
		public async Task<IActionResult> GetAllPostsByUserId([FromRoute] Guid userId)
		{
			var posts = await _postService.GetAllPostsByUserId(userId);
			
			return Ok(posts);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPostsByUserId()
		{
			var token = Request.Headers.Authorization.First();

			var userId = _tokenService.GetUserIdByToken(token);

			var posts = await _postService.GetAllPostsByUserId(userId);

			return Ok(posts);
		}
	}
}
