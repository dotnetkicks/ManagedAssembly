using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagedAssembly.Services;

namespace ManagedAssembly.Web
{
	public class Authenticate : ActionFilterAttribute
	{
		private readonly UserService _userService;
		protected UserService UserService {
			get {
				return _userService;
			}
		}

		public Authenticate() {
			_userService = new UserService();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!UserService.IsAuthenticated)
			{
				string returnUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
				string signInUrl = new UrlHelper(filterContext.RequestContext).Action("Index", "User", new { r = returnUrl });
				filterContext.HttpContext.Response.Redirect(signInUrl);
			}
			
			base.OnActionExecuting(filterContext);
		}
	}
}
