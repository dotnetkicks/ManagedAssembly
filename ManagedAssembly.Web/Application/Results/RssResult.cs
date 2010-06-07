using System.Text;
using System.Web.Mvc;
using Argotic.Common;
using Argotic.Syndication;

namespace ManagedAssembly.Web
{
	public class RssResult : ActionResult
	{
		public RssFeed RssFeed { get; set; }

		public RssResult(RssFeed feed) {
			RssFeed = feed;
		}

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.ContentType = "application/rss+xml";
			SyndicationResourceSaveSettings settings = new SyndicationResourceSaveSettings();
			settings.CharacterEncoding = new UTF8Encoding(false);
			RssFeed.Save(context.HttpContext.Response.OutputStream, settings);
		}
	}
}
