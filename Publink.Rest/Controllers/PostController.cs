using Microsoft.AspNetCore.Mvc;
using Publink.Rest.Interfaces;

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
			var posts = await _postService.GetAllRandomPosts();

			return Ok(posts);
		}
	}
}
