using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ManagedAssembly.Services;
using ManagedAssembly.Data;
using System.Net;

namespace ManagedAssembly.Web.Controllers
{
	public class HomeController : ControllerBase
	{
		protected readonly PostService _postService;
		protected PostService PostService {
			get {
				return _postService;
			}
		}

		protected readonly UserService _userService;
		protected UserService UserService {
			get {
				return _userService;
			}
		}

		protected readonly VoteService _voteService;
		protected VoteService VoteService {
			get {
				return _voteService;
			}
		}

		public HomeController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}

		public ActionResult Index(int? page) {
			int pageNo = page.HasValue ? page.Value : 1;

			IndexViewModel model = new IndexViewModel();
			model.PostListViewModel = new PostListViewModel();
			model.PostListViewModel.Posts = PostService.ListPopular(pageNo, 30);
			model.PageNumber = pageNo;

			if (UserService.IsAuthenticated) {
				model.PostListViewModel.UpVoteIds = VoteService.ListUserUpVotePostIds(UserService.Current.UserId);
				model.PostListViewModel.DownVoteIds = VoteService.ListUserDownVotePostIds(UserService.Current.UserId);
			}

			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[Authenticate]
		[ValidateInput(false)]
		public ActionResult Submit(string t, string u) {
			u = HttpUtility.UrlDecode(u);
			var url = u;

			if (!string.IsNullOrEmpty(u)) {
				try {
					var request = HttpWebRequest.Create(u);
					var response = request.GetResponse();
					url = response.ResponseUri.ToString();
				}
				catch {
					url = u;
				}
			}

			var model = new SubmitViewModel {
				Title = HttpUtility.UrlDecode(t ?? "").Trim(),
				Url = url
			};

			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Authenticate]
		[ValidateInput(false)]
		public ActionResult Submit(string Title, string Url, string Contents) {
			Post post = PostService.CreatePost(UserService.Current, Title, Url, Contents);

			if (post == null || post.UserId == UserService.Current.UserId) {
				// not a duplicate because this user submitted it
				return RedirectToAction("New");
			}
			else {
				// this is a duplicate so show existing post
				return RedirectToAction("Index", "Post", new { id = post.PostId, slug = post.Slug });
			}
		}

		[Authenticate]
		public ActionResult SignOut() {
			UserService.SignOut();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Tekpub() {
			return View();
		}

		public ActionResult FAQ() {
			return View();
		}

		public ActionResult Error() {
			return View();
		}
	}
}
