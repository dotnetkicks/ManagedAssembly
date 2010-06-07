using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DotNetOpenId.RelyingParty;
using DotNetOpenId;
using ManagedAssembly.Services;
using ManagedAssembly.Data;
using RestSharp;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class UserController : ControllerBase
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

		public UserController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}
	
		public ActionResult Index(string r) {
			var protocol = Request.IsSecureConnection ? "https://" : "http://";
			var host = Request.Url.Host;
			var port = Request.Url.Port == 80 ? "" : ":" + Request.Url.Port;
			var path = Url.Action("Authenticate", "User");

			var returnUrl = string.Format("{0}{1}{2}{3}", protocol, host, port, path);

			if (!string.IsNullOrEmpty(r)) {
				// append return url
				returnUrl += "?r=" + r;
			}

			ViewData["returnUrl"] = Server.UrlEncode(returnUrl);
			return View();
		}

		public ActionResult Threads(int? id) {
			UserThreadsViewModel model = new UserThreadsViewModel();
			model.User = id.HasValue ? UserService.GetById(id.Value) : UserService.Current;
			model.TopLevelComments = PostService.ListThreadsByUser(model.User, 1, 30);

			if (UserService.IsAuthenticated) {
				model.UpVoteIds = VoteService.ListUserUpVotePostIds(UserService.Current.UserId);
				model.DownVoteIds = VoteService.ListUserDownVotePostIds(UserService.Current.UserId);
			}

			return View(model);
		}

		public ActionResult SignOut() {
			UserService.SignOut();
			return RedirectToAction("Index", "Home");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateInput(false)]
		public ActionResult Profile(string DisplayName, string Email, string Url, string Twitter, string Bio, int? StackOverflowID) {
			UserService.Update(DisplayName, Email, Url, Twitter, Bio, StackOverflowID);
			TempData["Message"] = "User profile saved.";
			return RedirectToAction("Profile", new { id = UserService.Current.UserId });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Profile(int id) {
			User user = UserService.GetById(id);
			return View(user);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Authenticate(string token, string r) {
			var request = new RestRequest(Method.POST);
			request.BaseUrl = "https://rpxnow.com";
			request.Action = "api/v2/auth_info";
			request.AddParameter("apiKey", "98616d02313ea4d202bd10e84716d20587f47cc4");
			request.AddParameter("token", token);
			request.ResponseFormat = ResponseFormat.Json;

			var client = new RestClient();

			var response = client.Execute<RPXNow.AuthInfo>(request);

			var identifier = response.Profile.Identifier;
			var friendlyIdentifier = response.Profile.PreferredUsername;

			bool isNewUser = UserService.SignIn(identifier, friendlyIdentifier);

			if (isNewUser)
				return RedirectToAction("Profile", "User", new { id = UserService.Current.UserId });

			if (!string.IsNullOrEmpty(r)) {
				string returnUrl = r;

				if (!returnUrl.StartsWith("http")) {
					return Redirect(returnUrl);
				}
			}

			return RedirectToAction("Index", "Home");
		}

		public ActionResult RecalculatePoints() {
			var users = UserService.ListLatest(1, int.MaxValue);

			foreach (var user in users) {
				UserService.RecalculatePoints(user);
			}

			return Content("Done");
		}
	}
}
