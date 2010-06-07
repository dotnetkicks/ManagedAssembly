using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;
using ManagedAssembly.Web;

namespace ManagedAssembly.Services
{
	public class VoteService
	{
		private readonly UserRepository _userRepo;
		private UserRepository UserRepository {
			get {
				return _userRepo;
			}
		}

		private readonly VoteRepository _voteRepo;
		private VoteRepository VoteRepository {
			get {
				return _voteRepo;
			}
		}

		private readonly PostRepository _postRepo;
		private PostRepository PostRepository {
			get {
				return _postRepo;
			}
		}

		public VoteService() {
			_userRepo = new UserRepository();
			_postRepo = new PostRepository();
			_voteRepo = new VoteRepository();
		}

		public void RegisterVote(int userId, int postId, int voteDirectionId)
		{
			bool isDownVote = voteDirectionId == IDs.VoteDirection.Down;

			// prevent banned users from voting
			User user = UserRepository.GetById(userId);
			if (user.IsBanned)
				return;

			// disallow duplicate votes
			Vote vote = VoteRepository.GetExisting(postId, userId, voteDirectionId);
			if (vote != null)
				return;

			// disallow vote on own submissions
			Post post = PostRepository.GetById(postId);
			if (post.UserId == userId)
				return;

			// disallow downvote on top-level items
			if (isDownVote && !post.IsComment)
				return;

			// disallow downvotes on comments 36 hours after posting
			if (isDownVote && post.IsComment && post.CreateDate < DateTime.Now.AddHours(-Settings.Thresholds.DownvoteCommentHours))
				return;

			// disallow downvotes from user with less than 25 rep
			if (isDownVote && user.Points < Settings.Thresholds.MinimumDownvoteRep)
				return;

			// max out downvotes at 10
			if (isDownVote && post.Points <= Settings.Thresholds.CommentDownvoteCap)
				return;

			// disallow downvotes on first-level children of own post
			if (isDownVote && !post.Parent.IsComment && post.Parent.UserId == userId)
				return;

			// vote is valid
			vote = new Vote();
			vote.UserId = userId;
			vote.PostId = postId;
			vote.VoteDirectionId = voteDirectionId;
			vote.CreateDate = DateTime.Now;
			VoteRepository.Save(vote);

			var userService = new UserService();
			userService.RecalculatePoints(post.User);
			userService.RecalculatePoints(user);

			var postService = new PostService();
			postService.RecalculatePoints(post);
		}

		public List<int> ListUserUpVotePostIds(int userId) {
			return VoteRepository.ListUserVotePostIds(userId, IDs.VoteDirection.Up);
		}

		public List<int> ListUserDownVotePostIds(int userId) {
			return VoteRepository.ListUserVotePostIds(userId, IDs.VoteDirection.Down);
		}

		public List<Vote> ListLatest() {
			return ListLatest(1, 40);
		}

		public List<Vote> ListLatest(int pageNo, int pageSize) {
			return VoteRepository.ListLatest(pageNo, pageSize);
		}

		public List<Vote> ListByUser(User user) {
			return VoteRepository.ListByUser(user.UserId);
		}

		public void ChangeUser(Vote vote, int newUserId) {
			vote.UserId = newUserId;
			VoteRepository.Save(vote);

			var userService = new UserService();
			var user = userService.GetById(newUserId);
			userService.RecalculatePoints(user);
		}
	}
}
