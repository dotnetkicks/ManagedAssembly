using System.Web.Mvc;
using ManagedAssembly.Data;
using ManagedAssembly.Services;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class AdminController : Controller
	{
		private readonly PostService _postService;
		protected PostService PostService {
			get {
				return _postService;
			}
		}

		private readonly UserService _userService;
		protected UserService UserService {
			get {
				return _userService;
			}
		}

		private readonly VoteService _voteService;
		protected VoteService VoteService {
			get {
				return _voteService;
			}
		}

		public AdminController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}

		public EmptyResult Recalculate(int? id)
		{
			if (id.HasValue)
			{
				Post post = PostService.GetById(id.Value);
				PostService.RecalculateCommentCount(post);
				PostService.RecalculatePoints(post);
			}
			else
			{
				var repo = new PostRepository();
				foreach (var post in repo.ListAll())
				{
					PostService.RecalculateCommentCount(post);
					PostService.RecalculatePoints(post);
				}
			}

			return new EmptyResult();
		}

		public EmptyResult GenerateSlugs() {
			var repo = new PostRepository();

			var posts = repo.List(null, 1, int.MaxValue, true);
			foreach (var post in posts) {
				if (!string.IsNullOrEmpty(post.Title)) {
					post.Slug = PostService.Tokenize(post.Title);
					repo.Save(post);
				}
			}

			return new EmptyResult();
		}

		public EmptyResult GenerateDomains() {
			var repo = new PostRepository();
			var posts = repo.List(null, 1, int.MaxValue, true);
			foreach (var post in posts) {
				if (!string.IsNullOrEmpty(post.Url)) {
					post.DomainName = PostService.ExtractDomain(post.Url);
					repo.Save(post);
				}
			}

			return new EmptyResult();
		}
	}
}
