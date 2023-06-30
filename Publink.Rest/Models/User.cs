using System.ComponentModel.DataAnnotations;
using Publink.Rest.Enums;
using Publink.Rest.Models.Responses;

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
		public string? PasswordHash { get; set; }

		[Required]
		public Roles Role { get; set; }

		[Required]
		public string? Salt { get; set; }

		public UserResponse ToUserResponse()
		{
			return new UserResponse
			{
				Id = Id,
				Username = Username
			};
		}
	}
}
