using Microsoft.AspNetCore.Mvc;
using Publink.Rest.Interfaces;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
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
				return BadRequest(res);
			}

			return Ok(res);
		}
	}
}
