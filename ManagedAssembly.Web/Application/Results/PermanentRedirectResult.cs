using System.Text;
using System.Web.Mvc;
using Argotic.Common;
using Argotic.Syndication;

namespace ManagedAssembly.Web
{
	public class PermanentRedirectResult : ActionResult
	{
		private string _url;

		public PermanentRedirectResult(string url) {
			_url = url;
		}

		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.StatusCode = 301;
			context.HttpContext.Response.RedirectLocation = _url;
		}
	}
}
