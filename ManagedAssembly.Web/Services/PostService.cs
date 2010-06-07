using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;
using System.Text.RegularExpressions;
using ManagedAssembly.Web;

namespace ManagedAssembly.Services
{
	public class PostService
	{
		private readonly PostRepository _postRepo;
		private PostRepository PostRepository {
			get {
				return _postRepo;
			}
		}

		private readonly PostTagRepository _postTagRepo;
		private PostTagRepository PostTagRepository {
			get {
				return _postTagRepo;
			}
		}

		private readonly IList<string> _multiTenantSites;
		public IList<string> MultiTenantSites {
			get {
				return _multiTenantSites;
			}
		}

		public PostService() {
			_postRepo = new PostRepository();
			_postTagRepo = new PostTagRepository();

			_multiTenantSites = new List<string> {
				"codebetter.com",
				"lostechies.com", 
				"blogs.msdn.com",
				"weblogs.asp.net",
				"devlicio.us",
				"silverlight.net",
				"msmvps.com",
				"blogs.microsoft.co.il",
				"geekswithblogs.net",
				"pluralsight.com",
				"blogs.iis.net"
			};
		}

		public Post GetById(int id) {
			return PostRepository.GetById(id);
		}

		public Post CreateComment(User user, Post parent, string contents) {
			if (parent.TopMost != null && parent.TopMost.IsDeleted)
				return null;

			var post = new Post();
			post.UserId = user.UserId;
			post.ContentsRaw = contents;
			post.CreateDate = DateTime.Now;
			post.Weight = 0;
			post.ParentId = parent.PostId;

			var topMost = GetTopMost(post);
			post.TopMostId = topMost.PostId;

			Process(post);
			PostRepository.Save(post);

			RecalculateCommentCount(topMost);

			return post;
		}

		public Post EditComment(User user, int postId, string contents) {
			var post = PostRepository.GetById(postId);

			if (user.UserId != post.UserId && !user.IsAdmin)
				return null;

			post.ContentsRaw = contents;
			Process(post);
			PostRepository.Save(post);

			return post;
		}

		public void Process(Post post) {
			bool linkify = post.IsComment;
			post.Contents = post.ContentsRaw.Codify(linkify);
			post.Slug = Tokenize(post.Title);

			if (post.IsLink) {
				post.DomainName = ExtractDomain(post.Url);
			}
		}

		public string ExtractDomain(string url) {
			if (string.IsNullOrEmpty(url))
				return string.Empty;

			var uri = new Uri(url);

			var host = uri.Host.Replace("www.", string.Empty);
			var folder = "";
			if (IsMultiTenantSite(url)) {
				var pattern = string.Format(@"{0}/(blogs/)*([^/]+)/", host);
				var match = Regex.Match(url, pattern, RegexOptions.IgnoreCase);
				folder = string.Format(" - {0}", match.Groups[2].Value);
			}

			return string.Format("{0}{1}", host, folder);
		}

		private bool IsMultiTenantSite(string url) {
			return MultiTenantSites.Any(url.Contains);
		}

		public string Tokenize(string input) {
			if (string.IsNullOrEmpty(input)) {
				return input;
			}

			var output = HttpUtility.HtmlDecode(input);
			output = Regex.Replace(output, @"(?<=^|\s)\.(NET|net)", "dotnet");
			output = Regex.Replace(output, @"(C|c)#", "csharp");
			output = Regex.Replace(output, @"(F|f)#", "fsharp");
			output = Regex.Replace(output, @"'|\?|:|’", string.Empty);
			output = Regex.Replace(output, @"[^A-Za-z0-9-]", "-");

			while (output.Contains("--")) {
				output = output.Replace("--", "-");
			}

			if (output.EndsWith("-")) {
				output = output.Substring(0, output.Length - 1);
			}

			if (output.StartsWith("-")) {
				output = output.Substring(1, output.Length - 1);
			}

			return output.ToLower();
		}

		private Post GetTopMost(Post post) {
			while (post.ParentId.HasValue) {
				post = post.Parent;
			}

			return post;
		}

		public Post CreatePost(User user, string title, string url, string contents) {
			if (string.IsNullOrEmpty(title.Trim()))
				return null;

			if (string.IsNullOrEmpty(url.Trim()) && string.IsNullOrEmpty(contents.Trim()))
				return null;

			string searchUrl = url.Trim();
			if (url.Contains('#'))
				searchUrl = url.Remove(url.LastIndexOf('#'));

			if (!string.IsNullOrEmpty(searchUrl) && !Uri.IsWellFormedUriString(searchUrl, UriKind.Absolute))
				return null;

			var post = CheckForExisting(searchUrl);

			// blank urls (discussions) will always return null
			if (post == null) {
				post = new Post();
				post.UserId = user.UserId;
				post.Title = HttpUtility.HtmlEncode(title.Trim());

				if (post.Title.Length > 255)
					post.Title = post.Title.Substring(0, 255);

				post.CreateDate = DateTime.Now;
				post.Weight = 0;

				//if (user.Points >= Settings.Thresholds.MinimumSubmitRep)
					post.IsApproved = true; // users with less than 5 rep must have new posts approved

				if (!string.IsNullOrEmpty(searchUrl)) {
					post.Url = searchUrl;
					if (post.Url.Length > 500)
						post.Url = post.Url.Substring(0, 500);
				}
				else if (!string.IsNullOrEmpty(contents)) {
					post.ContentsRaw = contents.Trim();
				}

				Process(post);
				PostRepository.Save(post);
			}
			else {
				var voteService = new VoteService();
				voteService.RegisterVote(user.UserId, post.PostId, IDs.VoteDirection.Up);
			}

			return post;
		}

		public Post CheckForExisting(string url) {
			if (string.IsNullOrEmpty(url))
				return null;

			var post = PostRepository.GetByUrl(url);
			return post;
		}

		public List<Post> ListSubmissionsByUser(User user, int pageNum, int pageSize) {
			return PostRepository.ListSubmissionsByUser(user.UserId, pageNum, pageSize);
		}

		public List<Post> ListCommentsByUser(User user, int pageNum, int pageSize) {
			return PostRepository.ListCommentsByUser(user.UserId, pageNum, pageSize);
		}

		public IEnumerable<Post> ListThreadsByUser(User user, int pageNum, int pageSize) {
			var posts = ListCommentsByUser(user, pageNum, pageSize);

			// remove any that are already part of a parent thread
			var threadIds = new List<int>();
			var childrenIds = new List<int>();

			foreach (Post post in posts) {
				threadIds.Add(post.PostId);
				childrenIds.AddRange(GetChildIds(post));
			}

			var filtered = posts.Where(p => !childrenIds.Contains(p.PostId));
			return filtered;
		}

		private IEnumerable<int> GetChildIds(Post post) {
			var ids = new List<int>();

			foreach (var child in post.Children) {
				ids.Add(child.PostId);
				ids.AddRange(GetChildIds(child));
			}

			return ids;
		}

		public List<Post> ListPopular(int pageNum, int pageSize) {
			var includeDeleted = false;
			return PostRepository.List(null, pageNum, pageSize, includeDeleted);
		}

		public List<Post> ListComments(int parentId, int pageNum, int pageSize) {
			var includeDeleted = true;
			return PostRepository.List(parentId, pageNum, pageSize, includeDeleted);
		}

		public List<Post> ListNew(int pageNum, int pageSize) {
			return PostRepository.ListNew(null, pageNum, pageSize);
		}

		public List<Post> ListNewestComments(int pageNum, int pageSize) {
			return PostRepository.ListNewComments(pageNum, pageSize);
		}

		public void AddTagToPost(int postId, int tagId, int userId) {
			var pt = new PostTag();
			pt.PostId = postId;
			pt.TagId = tagId;
			pt.UserId = userId;
			PostTagRepository.Save(pt);
		}

		public void RecalculatePoints(Post post) {
			var repo = new PostStatsViewRepository();
			var view = repo.GetByPost(post.PostId);
			post.UpVotes = view.UpVotes;
			post.DownVotes = view.DownVotes;
			post.Points = view.Points;
			post.Weight = view.Weight.GetValueOrDefault(0);
			PostRepository.Save(post);
		}

		public void RecalculateCommentCount(Post post) {
			post.CommentCount = CountComments(post);
			PostRepository.Save(post);
		}

		private int CountComments(Post post) {
			var count = 0;

			count += post.Children.Where(p => !p.IsDeleted && !p.IsSpam).Count();

			foreach (var child in post.Children) {
				count += CountComments(child);
			}

			return count;
		}

		public string DeletePost(User user, int postId) {
			var post = PostRepository.GetById(postId);

			if (post == null || post.IsDeleted)
				return "";

			if (post.UserId != user.UserId && !user.IsModerator && !user.IsAdmin)
				return "";

			var type = post.IsComment ? "comment" : "post";

			var kids = PostRepository.List(postId, 1, 1, false);

			if (kids.Count == 0) {
				post.IsDestroyed = true;
			}

			post.IsDeleted = true;

			PostRepository.Save(post);

			if (post.IsComment) {
				RecalculateCommentCount(post.TopMost);
			}

			return type;
		}

		public Post EditPost(User user, int postId, string title, string url, string contents) {
			var post = PostRepository.GetById(postId);

			if (post.IsDiscussion && user.UserId == post.UserId) {
				post.ContentsRaw = contents;
			}

			if (user.IsAdmin || user.IsModerator) {
				post.Title = title;
				post.Url = url;
				post.ContentsRaw = contents;
			}

			Process(post);
			PostRepository.Save(post);
			return post;
		}

		public List<Post> ListAllByUser(User user) {
			return PostRepository.ListAllByUser(user.UserId);
		}

		public void ChangeUser(Post post, int newUserId) {
			post.UserId = newUserId;
			PostRepository.Save(post);

			var userService = new UserService();
			var user = userService.GetById(newUserId);
			userService.RecalculatePoints(user);
		}
	}
}
