using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Dimebrain.TweetSharp.Extensions;
using Dimebrain.TweetSharp.Fluent;
using Dimebrain.TweetSharp.Model;
using SubSonic.Extensions;
using Spark.Web.Mvc;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class TwitterController : ControllerBase
	{
		public ActionResult Index() {
			return View();
		}

		public ActionResult Clear() {
			CacheManager cache = new CacheManager(new ShortTermProvider());
			cache.Destroy("last_fetch");
			cache.Destroy("last_id");
			cache.Destroy("cachedStatuses");

			return Content("cache cleared");
		}

		public ActionResult Status() {
			var request = FluentTwitter.CreateRequest().AuthenticateAs("ManagedAssembly", Password).Account().GetRateLimitStatus();
			var status = request.Request().AsRateLimitStatus();
			return Json(new { status.RemainingHits, status.HourlyLimit, status.ResetTime });
		}

		public JsonResult Following() {
			var request = FluentTwitter.CreateRequest().AuthenticateAs("ManagedAssembly", Password);
			request.Configuration.CacheUntil(10.Minutes().FromNow());
			request.Configuration.UseGzipCompression();
			request.Users().GetFriends().AsXml();

			var users = request.Request().AsUsers();

			if (users == null) {
				return new JsonResult();
			}

			var data = from u in users
					   orderby u.Name ?? u.ScreenName ?? string.Empty
					   select new {
						   Id = u.Id,
						   ScreenName = u.ScreenName,
						   Name = u.Name,
						   ProfileImageUrl = u.ProfileImageUrl,
						   FollowerCount = u.FollowersCount
					   };

			return Json(new { users = data });
		}

		public JsonResult Refresh(long? lid) {
			CacheManager cache = new CacheManager(new ShortTermProvider());

			IEnumerable<TwitterStatus> statuses = new List<TwitterStatus>();
			long maxId = 0;

			DateTime lastFetch = cache.Get<DateTime>("last_fetch");
			if (lastFetch < DateTime.Now.AddSeconds(-15)) {
				long lastCacheId = cache.Get<long>("last_id");
				if (lastCacheId == 0)
					lastCacheId = 1; // twitter returns null if you use Since(0)

				bool requestComplete = false;

				try {
					var request = FluentTwitter.CreateRequest().AuthenticateAs("ManagedAssembly", Password);
					request.Statuses().OnFriendsTimeline().Take(100).Since(lastCacheId).AsJson();

					statuses = request.Request().AsStatuses();
					requestComplete = true;
				}
				catch {
					// twitter is probably down
				}

				if (requestComplete && statuses != null) {
					foreach (var status in statuses) {
						string raw = status.Text;
						string[] words = Regex.Split(raw, @"([ \(\)\{\}\[\]])");
						StringBuilder output = new StringBuilder();
						foreach (string word in words) {
							if (word.StartsWith("#")) {
								// hashtag
								string hashtag = String.Empty;
								Match foundHashtag = Regex.Match(word, @"#(\w+)(?<suffix>.*)");
								if (foundHashtag.Success) {
									hashtag = foundHashtag.Groups[1].Captures[0].Value;
									output.Append(string.Format(@"#<a href=""http://search.twitter.com/search?q=%23{0}"" target=""_blank"">{0}</a>", hashtag));
								}
							}
							else if (word.StartsWith("@")) {
								string userName = String.Empty;
								Match foundUserName = Regex.Match(word, @"@(\w+)(?<suffix>.*)");
								if (foundUserName.Success) {
									userName = foundUserName.Groups[1].Captures[0].Value;
									output.Append(string.Format(@"@<a href=""http://twitter.com/{0}"" target=""_blank"">{0}</a>", userName));
								}
							}
							else if (word.IsURL()) {
								output.Append(string.Format(@"<a href=""{0}"" target=""_blank"">{0}</a>", word));
							}
							else {
								output.Append(word);
							}
						}

						status.Text = output.ToString();
					}

					cache.Store("last_fetch", DateTime.Now);
				}
			}

			var cachedStatuses = cache.Get<List<TwitterStatus>>("cachedStatuses");
			if (cachedStatuses == null) {
				cachedStatuses = new List<TwitterStatus>();
			}

			if (statuses != null) {
				foreach (var status in statuses) {
					if (!cachedStatuses.Contains(status)) {
						cachedStatuses.Add(status);
					}
				}
			}

			if (cachedStatuses != null && cachedStatuses.Count > 0) {
				maxId = cachedStatuses.Max(s => s.Id);
				cache.Store("last_id", maxId);
			}

			cache.Store("cachedStatuses", cachedStatuses);

			long lastClientId = lid ?? 1;

			var data = from s in cachedStatuses
					   where s.Id > lastClientId || s.Id == -1
					   orderby s.Id descending
					   select new {
						   Id = s.Id,
						   ProfileImageUrl = s.User.ProfileImageUrl,
						   Text = s.Text,
						   Source = s.Source,
						   UserName = s.User.ScreenName,
						   RelativeTime = s.CreatedDate.ToRelativeTime(false),
						   IsoTime = s.CreatedDate.ToString("s"),
						   ClientLink = s.Source,
						   IsReply = s.InReplyToStatusId != 0,
						   InReplyToId = s.InReplyToStatusId,
						   InReplyToUser = s.InReplyToScreenName,
						   Permalink = string.Format("http://twitter.com/{0}/status/{1}", s.User.ScreenName, s.Id)
					   };

			return Json(new { results = data.Take(100), max_id = maxId });
		}

		public JavascriptViewResult Tweet() {
			return new JavascriptViewResult { ViewName = "_Tweet" };
		}

		public JavascriptViewResult User() {
			return new JavascriptViewResult { ViewName = "_User" };
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Check() {
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Check(string follower, string target) {
			string response = FluentTwitter.CreateRequest()
								.Friendships().Verify(follower).IsFriendsWith(target)
								.Request().Response;

			response = Regex.Replace(response, @"<\/?friends>", "");

			bool following = false;
			bool.TryParse(response, out following);

			return Json(new { result = following });
		}

		private string Password {
			get {
				return "news@10";
			}
		}
	}
}
