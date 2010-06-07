using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Net;
using ManagedAssembly.Services;
using Dimebrain.TweetSharp.Fluent;
using Bitly;

namespace ManagedAssembly.Web.Controllers
{
	[Authenticate]
	public class ShareController : Controller
	{
		private readonly PostService _postService;
		private readonly UserService _userService;
		public ShareController() {
			_postService = new PostService();
			_userService = new UserService();
		}

		public ActionResult Index(string t, string u) {
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

			var model = new ShareViewModel {
				Title = HttpUtility.UrlDecode(t ?? "").Trim(),
				Url = url
			};

			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Submit(string title, string twitterTitle, string twitterUser, string url, bool ManagedAssembly, bool dotnetlinks, bool dotnetvideos, bool dotnetpodcasts) {
			if (string.IsNullOrEmpty(twitterTitle)) 
				twitterTitle = title;

			if (ManagedAssembly) {
				_postService.CreatePost(UserService.Current, title, url, null);
			}
			if (dotnetlinks) {
				PostToTwitter("dotnetlinks", twitterTitle, twitterUser, url);
			}
			if (dotnetpodcasts) {
				PostToTwitter("dotnetpodcasts", twitterTitle, twitterUser, url);
			}
			if (dotnetvideos) {
				PostToTwitter("dotnetvideos", twitterTitle, twitterUser, url);
			}

			TempData["Message"] = "Posted";
			return RedirectToAction("Index");
		}

		private void PostToTwitter(string twitterAccount, string twitterTitle, string twitterUser, string url) {
			var text = twitterTitle;
			var user = twitterUser;

			if (!string.IsNullOrEmpty(user) && text.Length + user.Length + 24 < 120) { // 20 for bitly link, 4 for " by "
				text = text + " by " + user;
			}

			var bitly = new BitlyApi("managedassembly", "R_190969547a66bb6744e5c026ef3fd499");
			var shortUrl = bitly.Shorten(url).Results[url].ShortUrl;

			text = text + " " + shortUrl;

			var result = FluentTwitter.CreateRequest().AuthenticateAs(twitterAccount, "news@10")
									  .Statuses().Update(text).Request();
		}
	}
}
