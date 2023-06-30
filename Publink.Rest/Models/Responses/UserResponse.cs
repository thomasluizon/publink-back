using System.ComponentModel.DataAnnotations;

namespace Publink.Rest.Models.Responses
{
    public class UserResponse
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Username { get; set; }
    }
}
