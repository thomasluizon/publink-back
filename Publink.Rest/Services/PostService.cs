﻿using Publink.Rest.Helpers;
using Publink.Rest.Interfaces;
using Publink.Rest.Models;
using Publink.Rest.Models.Dto;

namespace Publink.Rest.Services
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postsRepository;

		public PostService(IPostRepository postsRepository)
		{
			_postsRepository = postsRepository;
		}

		public async Task<Post> Create(PostDto post)
		{
			var res = await _postsRepository.Create(post);

			return res;
		}

		public async Task<IList<Post>> GetAllRandom()
		{
			var posts = await _postsRepository.GetAll();

			if (!posts.Any())
			{
				return new List<Post>();
			}

			var lastPost = posts.OrderByDescending(x => x.Id).ToList().First();

			var randomPosts = new List<Post>
			{
				lastPost
			};

			posts.Shuffle();

			var listToAdd = posts.ToList();

			listToAdd.Remove(lastPost);

			randomPosts.AddRange(listToAdd);

			return randomPosts;
		}

		public async Task<Post> GetById(int id)
		{
			var post = await _postsRepository.GetById(id);

			return post;
		}

		public async Task<IList<Post>> GetByIdAndRandom(int id, int randomLength)
		{
			var post = await GetById(id);

			if (post == null)
			{
				return new List<Post>();
			}

			var posts = new List<Post>
			{
				post
			};

			var randomPosts = await GetAllRandom();

			var postToRemove = randomPosts.First(x => x.Id == id);

			randomPosts.Remove(postToRemove);

			var i = 0;

			foreach (var p in randomPosts)
			{
				if (i == randomLength)
				{
					break;
				}

				posts.Add(p);
				i++;
			}

			return posts;
		}
	}
}
