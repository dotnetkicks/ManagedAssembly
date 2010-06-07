using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argotic.Syndication;

namespace ManagedAssembly.Web.Controllers
{
	public abstract class ControllerBase : Controller
	{
		public RssResult Feed(RssFeed feed) {
			return new RssResult(feed);
		}

		public PermanentRedirectResult PermanentRedirect(string url) {
			return new PermanentRedirectResult(url);
		}
	}
}
