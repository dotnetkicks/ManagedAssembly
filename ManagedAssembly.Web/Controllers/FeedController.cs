using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ManagedAssembly.Services;
using ManagedAssembly.Data;
using Argotic.Syndication;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class FeedController : ControllerBase
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

		public FeedController() {
			_postService = new PostService();
			_userService = new UserService();
		}

		public new ActionResult User(int id, bool? useLocalLinks) {
			var user = UserService.GetById(id);

			var posts =  PostService.ListSubmissionsByUser(user, 1, 30);

			RssFeed feed = new RssFeed();
			feed.Channel.Title = string.Format("Managed Assembly : {0}", user.DisplayName);
			feed.Channel.Link = new Uri("http://managedassembly.com" + Url.Action("Profile", "User", new { id = user.UserId }));
			feed.Channel.LastBuildDate = DateTime.Now;

			Build(feed, posts, useLocalLinks.GetValueOrDefault(false));
			
			return Feed(feed);
		}


		public ActionResult Popular(bool? useLocalLinks, bool? bypass) {
			bool isBot = Request.UserAgent.Contains("FeedBurner");

			if (!isBot && !useLocalLinks.GetValueOrDefault(false) && !bypass.GetValueOrDefault(false)) {
				return PermanentRedirect(Settings.Feed.PopularFeedUrl);
			}

			var posts = PostService.ListPopular(1, 30).Where(p => p.IsDeleted == false).ToList();

			RssFeed feed = new RssFeed();
			feed.Channel.Title = "Managed Assembly : Popular";
			feed.Channel.Link = new Uri("http://managedassembly.com/");
			feed.Channel.LastBuildDate = DateTime.Now;

			Build(feed, posts, useLocalLinks.GetValueOrDefault(false));

			return Feed(feed);
		}

		private static void Build(RssFeed feed, List<Post> posts, bool useLocalLinks) {
			foreach (var post in posts) {
				Uri link = new Uri(post.FeedLink);
				if (useLocalLinks) {
					link = new Uri(post.Permalink);
				}

				feed.Channel.AddItem(new RssItem {
					Author = post.DomainName,
					Description = post.FeedContents,
					PublicationDate = post.CreateDate,
					Title = post.FeedTitle,
					Link = link
				});
			}
		}

		public ActionResult New(bool? useLocalLinks, bool? bypass) {
			bool isBot = Request.UserAgent.Contains("FeedBurner");

			if (!isBot && !useLocalLinks.GetValueOrDefault(false) && !bypass.GetValueOrDefault(false)) {
				return PermanentRedirect(Settings.Feed.NewFeedUrl);
			}

			var posts = PostService.ListNew(1, 30).Where(p => p.IsDeleted == false).ToList();

			RssFeed feed = new RssFeed();
			feed.Channel.Title = "Managed Assembly : Newest";
			feed.Channel.Link = new Uri("http://managedassembly.com/");
			feed.Channel.LastBuildDate = DateTime.Now;

			Build(feed, posts, useLocalLinks.GetValueOrDefault(false));

			return Feed(feed);
		}

		public ActionResult List() {
			return View();
		}
	}
}