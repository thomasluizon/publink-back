using Microsoft.AspNetCore.Mvc;

namespace Publink.Rest.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PingController : ControllerBase
	{
		private readonly ILogger<PingController> _logger;

		public PingController(ILogger<PingController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Ping()
		{
			_logger.LogInformation("Calling ping controller");

			return Content("ok");
		}
	}
}