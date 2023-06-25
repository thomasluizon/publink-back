using Dapper;
using Publink.Rest.Context;
using Publink.Rest.Interfaces.Repository;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Repository
{
	public class PostRepository : IPostRepository
	{
		private readonly DapperContext _dbContext;
		private readonly ILogger _logger;
		private readonly string _baseLogErrorMessage;

		public PostRepository(ILogger<PostRepository> logger, DapperContext dbContext)
		{
			_dbContext = dbContext;
			_logger = logger;
			_baseLogErrorMessage = "Message => {0} - Stack Trace => {1}";
		}

		public async Task<IList<Post>> GetAll()
		{
			try
			{
				_logger.LogDebug("Getting all posts from database");

				const string query = "SELECT * FROM Post";

				using var connection = _dbContext.CreateConnection();

				var posts = await connection.QueryAsync<Post>(query);

				return posts.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting all posts from database: {Message}", string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}

		public async Task<Post> Create(PostDto post)
		{
			try
			{
				_logger.LogDebug("Creating post with title: {Title} in the database", post.Title);

				const string query = @"INSERT INTO Post (
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
			catch (Exception ex)
			{
				_logger.LogError("Error while creating new post on the database: {Message}", string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}

		public async Task<Post?> GetById(int id)
		{
			try
			{
				_logger.LogDebug("Getting post with id {Id} in the database", id);

				const string query = @"SELECT *
                         FROM Post
                        WHERE id = @id";

				using var connection = _dbContext.CreateConnection();

				var post = await connection.QueryFirstOrDefaultAsync<Post>(query, new { id });

				if (post != null) return post;

				_logger.LogError("Post with id {Id} not found", id);
				return null;

			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting post with id {Id} on the database: {Message}", id, string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}
	}
}
