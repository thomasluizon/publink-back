using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models.Dto.Users
{
	public class UserResponseDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string? Username { get; set; }
	}
}
