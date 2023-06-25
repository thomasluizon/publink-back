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
				_logger.LogInformation("Getting all posts from database");

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

		public async Task<Post> Create(PostDto post, Guid userId)
		{
			try
			{
				_logger.LogInformation("Creating post with title: {Title} in the database", post.Title);

				const string query = @"INSERT INTO Post (
										    Id,
											 Title,
											 Description,
											 ImgUrl,
                                  User_Id,
											 Create_Date,
											 Update_Date
									  ) VALUES (
                                  @Id,
											 @Title,
											 @Description,
											 @ImgUrl,
                                  @UserId,
											 @CreateDate,
											 @UpdateDate
									  );

									  SELECT *
									    FROM Post
									ORDER BY Create_Date DESC
									   LIMIT 1;";

				using var connection = _dbContext.CreateConnection();

				var date = DateTime.UtcNow.AddHours(-3);

				var insertedPost = await connection.QuerySingleAsync<Post>(query, new
				{
					Id = Guid.NewGuid(),
					post.Title,
					post.Description,
					post.ImgUrl,
					UserId = userId,
					CreateDate = date,
					UpdateDate = date
				});

				return insertedPost;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while creating new post on the database: {Message}", string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}

		public async Task<Post?> GetById(Guid id)
		{
			try
			{
				_logger.LogInformation("Getting post with id {Id} in the database", id);

				const string query = @"SELECT *
				                         FROM Post
				                        WHERE id = @id";

				using var connection = _dbContext.CreateConnection();

				var post = await connection.QueryFirstOrDefaultAsync<Post>(query, new { id });

				if (post != null)
					return post;

				_logger.LogError("Post with id {Id} not found", id);
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting post with id {Id} on the database: {Message}", id, string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}

		public async Task<IList<Post>> GetAllPostsByUserId(Guid userId)
		{
			try
			{
				_logger.LogInformation("Getting all posts with id {userId} in the database", userId);

				const string query = @"SELECT *
											    FROM Post AS p
											   WHERE p.User_Id = @UserId
											ORDER BY Create_Date DESC;";

				using var connection = _dbContext.CreateConnection();

				var posts = await connection.QueryAsync<Post>(query, new
				{
					UserId = userId
				});

				return posts.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError("Error while getting all posts from user with id {userId} on the database: {Message}", userId, string.Format(_baseLogErrorMessage, ex.Message, ex.StackTrace));
				throw;
			}
		}
	}
}
