using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models
{
	public class Post
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string? Title { get; set; }

		[Required]
		public string? Description { get; set; }

		[Required]
		public string? ImgUrl { get; set; }
	}
}
