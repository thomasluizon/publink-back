using Microsoft.EntityFrameworkCore;

namespace Publink.Rest.Models
{
	public class ApplicationDbContext : DbContext
	{
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) {}
	}
}
