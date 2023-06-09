﻿using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models
{
	public class Post
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string? Title { get; set; }

		[Required]
		public string? Description { get; set; }

		[Required]
		public string? ImgUrl { get; set; }

		[Required]
		public Guid UserId { get; set; }

		[Required]
		public DateTime CreateDate { get; set; }

		[Required]
		public DateTime UpdateDate { get; set; }
	}
}
