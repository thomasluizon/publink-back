using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Publink.Rest.Models
{
	public class Post
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string ImgUrl { get; set; }
	}
}
