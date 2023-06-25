using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models.Dto
{
	public class PostDto
	{
		[Required]
		public string? Title { get; set; }

		[Required]
		public string? Description { get; set; }

		[Required]
		public string? ImgUrl { get; set; }
	}
}
