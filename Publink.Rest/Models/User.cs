using System.ComponentModel.DataAnnotations;
using Publink.Rest.Enums;

namespace Publink.Rest.Models
{
	public class User
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		public string? Username { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		public Roles Role { get; set; }
	}
}
