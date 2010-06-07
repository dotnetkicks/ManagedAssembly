using System;
using System.Linq;
using System.Web.Mvc;
using ManagedAssembly.Services;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class DashboardController : ControllerBase
	{
		private readonly UserService _userService;
		protected UserService UserService {
			get {
				return _userService;
			}
		}

		private readonly PostService _postService;
		protected PostService PostService {
			get {
				return _postService;
			}
		}

		private readonly VoteService _voteService;
		protected VoteService VoteService {
			get {
				return _voteService;
			}
		}

		public DashboardController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}

		[Authenticate]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Combine() {
			if (UserService.Current == null || !UserService.IsAuthenticated || !UserService.Current.IsAdmin) {
				return new EmptyResult();
			}

			return View();
		}

		[Authenticate]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Combine(string userIds) {
			if (UserService.Current == null || !UserService.IsAuthenticated || !UserService.Current.IsAdmin) {
				return new EmptyResult();
			}

			var ids = (from id in userIds.Split(',')
					   select Convert.ToInt32(id)).ToList();

			var keepUser = _userService.GetById(ids.Min());
			var newestUser = _userService.GetById(ids.Max());
			_userService.UpdateOpenId(keepUser, newestUser.ExternalKey);

			foreach (var userId in ids.Where(id => id != keepUser.UserId)) {
				var user = _userService.GetById(userId);

				// update votes
				var votes = _voteService.ListByUser(user);
				foreach (var vote in votes) {
					_voteService.ChangeUser(vote, keepUser.UserId);
				}

				// update posts
				var posts = _postService.ListAllByUser(user);
				foreach (var post in posts) {
					_postService.ChangeUser(post, keepUser.UserId);
				}

				_userService.Destroy(user);
			}

			TempData["Message"] = "Users combined.";
			return RedirectToAction("Index");
		}

		[Authenticate]
		public ActionResult Index() {
			if (UserService.Current == null || !UserService.IsAuthenticated || !UserService.Current.IsAdmin) {
				return new EmptyResult();
			}

			var model = new DashboardViewModel();
			model.LatestSubmissions = PostService.ListNew(1, 5);
			model.LatestComments = PostService.ListNewestComments(1, 5);
			model.LatestVotes = VoteService.ListLatest(1, 5);
			model.LatestUsers = UserService.ListLatest(1, 5);
			model.TopUsers = UserService.ListTop(1, 10);

			return View(model);
		}

	}
}
