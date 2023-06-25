using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models.Dto.Users
{
	public class UserRegisterDto
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		public string? Username { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}
