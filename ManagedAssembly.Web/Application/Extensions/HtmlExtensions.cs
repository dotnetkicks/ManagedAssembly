using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web
{
	public static class HtmlExtensions
	{
		public static string Decode(this HtmlHelper html, string input) {
			return HttpUtility.HtmlDecode(input);
		}

		public static string PostLink(this HtmlHelper html, Post post) {
			if (post.IsDeleted)
				return string.Format("{0} [deleted]", post.Title);

			string urlFormat = @"<a{2} href=""{0}"">{1}</a>";
			string nofollow = post.Points >= Settings.Thresholds.MinimumPointsToUndoNoFollow ? @" rel=""nofollow""" : "";

			UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
			string url = post.IsLink ? post.Url : urlHelper.Action("Index", "Post", new { id = post.PostId, slug = post.Slug });

			return string.Format(urlFormat, url, post.Title, nofollow);
		}

		public static string NumberWithLabel(this HtmlHelper html, int num, string singular, string plural) {
			string format = "{0} {1}";
			string label = singular;
			if (num != 1)
				label = plural;

			return string.Format(format, num, label);
		}

		public static string SimplePager(this HtmlHelper html, int currentPage) {
			string baseUrl = html.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;

			string prevLink = "";
			TagBuilder prevLinkBuilder = new TagBuilder("a");
			prevLinkBuilder.InnerHtml = "&laquo; Previous";
			if (currentPage == 2) {
				prevLinkBuilder.Attributes.Add("href", baseUrl);
				prevLink = prevLinkBuilder.ToString();
			}
			else if (currentPage > 2) {
				prevLinkBuilder.Attributes.Add("href", string.Format("{0}?page={1}", baseUrl, currentPage - 1));
				prevLink = prevLinkBuilder.ToString();
			}

			TagBuilder nextLinkBuilder = new TagBuilder("a");
			nextLinkBuilder.Attributes.Add("href", string.Format("{0}?page={1}", baseUrl, currentPage + 1));
			nextLinkBuilder.InnerHtml = "Next &raquo;";

			string separator = currentPage > 1 ? " | " : "";

			return string.Format("{0}{1}{2}", prevLink, separator, nextLinkBuilder.ToString());
		}
	}
}
