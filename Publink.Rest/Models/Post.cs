using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models
{
	public class Post
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string ImgUrl { get; set; }
	}
}
