using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ManagedAssembly.Services;
using ManagedAssembly.Data;
using SubSonic.Extensions;

namespace ManagedAssembly.Web
{
	public static class VoteControls
	{
		public static string PostVoteControls(this HtmlHelper html, Post post, List<int> upVotes) {
			var url = new UrlHelper(html.ViewContext.RequestContext);
			var request = html.ViewContext.HttpContext.Request;
			var output = new StringBuilder();

			if (!UserService.IsAuthenticated) {
				var link = new TagBuilder("a");
				link.Attributes.Add("href", url.Action("Index", "User", new { r = request.Url.AbsolutePath }));
				output.Append(link.ToString(TagRenderMode.StartTag));
				output.Append(Image(url.Content("~/Media/Images/up.png"), "Log in to vote"));
				output.Append(link.ToString(TagRenderMode.EndTag));
				return output.ToString();
			}

			var isOwn = UserService.IsAuthenticated && post.UserId == UserService.Current.UserId;
			if (isOwn) {
				return Image(url.Content("~/Media/Images/star.png"), "Submitted by you");
			}

			if (upVotes.Contains(post.PostId)) {
				return Image(url.Content("~/Media/Images/upon.png"), "Already voted");
			}

			output.Append(Image(url.Content("~/Media/Images/up.png"), "Vote Up", new { id = "up_" + post.PostId, @class = "upvote" }));
			output.Append(Image(url.Content("~/Media/Images/upon.png"), "Already voted", new { id = "vote_up_on_" + post.PostId, @class = "hidden" }));

			return output.ToString();
		}

		public static string CommentVoteControls(this HtmlHelper html, Post comment, List<int> upVotes, List<int> downVotes) {
			var url = new UrlHelper(html.ViewContext.RequestContext);
			var request = html.ViewContext.HttpContext.Request;

			var output = new StringBuilder();

			if (!UserService.IsAuthenticated) {
				var link = new TagBuilder("a");
				link.Attributes.Add("href", url.Action("Index", "User", new { r = request.Url.AbsolutePath }));
				output.Append(link.ToString(TagRenderMode.StartTag));
				output.Append(Image(url.Content("~/Media/Images/bullet_arrow_up.png"), "Vote Up"));
				output.Append(link.ToString(TagRenderMode.EndTag));
				return output.ToString();
			}

			var isOwn = UserService.IsAuthenticated && comment.UserId == UserService.Current.UserId;
			if (isOwn) {
				return Image(url.Content("~/Media/Images/bullet_star.png"), "Submitted by you");
			}

			if (upVotes.Contains(comment.PostId)) {
				return Image(url.Content("~/Media/Images/bullet_arrow_up_on.png"), "Already voted");
			}

			if (downVotes.Contains(comment.PostId)) {
				return Image(url.Content("~/Media/Images/bullet_arrow_down_on.png"), "Already voted");
			}

			output.Append(Image(url.Content("~/Media/Images/bullet_arrow_up.png"), "Vote Up", new { id = "up_" + comment.PostId, @class = "upvote" }));
			output.Append(Image(url.Content("~/Media/Images/bullet_arrow_up_on.png"), "Already voted", new { id = "vote_up_on_" + comment.PostId, @class = "hidden" }));
			output.Append(Image(url.Content("~/Media/Images/bullet_arrow_down_on.png"), "Already voted", new { id = "vote_down_on_" + comment.PostId, @class = "hidden" }));

			var canDownvote = UserService.IsAuthenticated;
			canDownvote = canDownvote && UserService.Current.Points > Settings.Thresholds.MinimumDownvoteRep;
			canDownvote = canDownvote && comment.CreateDate > DateTime.Now.AddHours(-Settings.Thresholds.DownvoteCommentHours);
			canDownvote = canDownvote && comment.Points > Settings.Thresholds.CommentDownvoteCap;

			var isFirstLevelRestriction = !comment.Parent.IsComment && comment.Parent.UserId == UserService.Current.UserId;

			if (canDownvote && !isFirstLevelRestriction) {
				output.Append(Image(url.Content("~/Media/Images/bullet_arrow_down.png"), "Vote Down", new { id = "down_" + comment.PostId, @class = "downvote" }));
			}

			return output.ToString();
		}

		private static string Image(string src, string alt, object attributes) {
			var image = new TagBuilder("img");
			image.Attributes.Add("src", src);
			image.Attributes.Add("alt", alt);
			if (attributes != null) {
				image.MergeAttributes(attributes.ToDictionary());
			}
			return image.ToString(TagRenderMode.SelfClosing);
		}

		private static string Image(string src, string alt) {
			return Image(src, alt, null);
		}
	}
}
