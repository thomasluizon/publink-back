using Dapper;
using Publink.Rest.Context;
using Publink.Rest.Interfaces;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Services
{
	public class PostRepository : IPostRepository
	{
		private readonly DapperContext _dbContext;

		public PostRepository(DapperContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Post>> GetAllRandomPosts()
		{
			var query = "SELECT * FROM Post";

			using var connection = _dbContext.CreateConnection();

			var posts = await connection.QueryAsync<Post>(query);

			return posts.ToList();
		}

		public async Task<Post> AddPost(PostDto post)
		{
			var query = @"INSERT INTO Post (
										 title,
										 description,
										 imgUrl
								  ) VALUES (
										 @Title,
										 @Description,
										 @ImgUrl
								  );
								  SELECT LAST_INSERT_ID();";

			using var connection = _dbContext.CreateConnection();
			
			var insertedId = await connection.ExecuteScalarAsync<int>(query, new
			{
				post.Title,
				post.Description,
				post.ImgUrl
			});

			var res = await connection.QuerySingleAsync<Post>("SELECT * FROM Post WHERE id = @Id", new
			{
				Id = insertedId
			});

			return res;
		}
	}
}
