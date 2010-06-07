using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ManagedAssembly.Services;
using Spark.Web.Mvc;

namespace ManagedAssembly.Web
{
	public class Application : System.Web.HttpApplication
	{
		private static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("elmah.axd");
			routes.IgnoreRoute("favicon.ico");
			routes.IgnoreRoute("robots.txt");

			// /
			routes.MapRoute("Home", "", new { controller = "Home", action = "Index" });

			// /user
			routes.MapRoute("SignInOrRegister", "user", new { controller = "User", action = "Index" });

			// /signout
			routes.MapRoute("SignOut", "signout", new { controller = "User", action = "SignOut" });

			// /authenticate
			routes.MapRoute("Authenticate", "authenticate", new { controller = "User", action = "Authenticate" });

			// /new
			routes.MapRoute("New", "new", new { controller = "Post", action = "New" });

			// /tekpub
			routes.MapRoute("Tekpub", "tekpub", new { controller = "Home", action = "Tekpub" });

			// /feed, /feed/new
			routes.MapRoute("Feed", "feed/{action}/{id}", new { controller = "Feed", action = "Popular", id = "" });

			// dashboard
			routes.MapRoute("Dashboard", "dashboard/{action}", new { controller = "Dashboard", action = "Index" });

			// share
			routes.MapRoute("Share", "Share/{action}", new { controller = "Share", action = "Index" });

			// /Comments
			routes.MapRoute("Comments", "comments", new { controller = "Comments", action = "Index" });

			// /twitter
			routes.MapRoute("Twitter", "twitter/{action}", new { controller = "Twitter", action = "Index" });

			// /live/action - deprecated
			routes.MapRoute("Live", "live/{action}", new { controller = "Twitter", action = "Index" });

			// /vote
			routes.MapRoute("Voting", "vote/{action}", new { controller = "Vote", action = "Index" });

			// /submit
			routes.MapRoute("Actions", "{action}", new { controller = "Home", action = "" });

			// /admin/{action}
			routes.MapRoute("Admin", "admin/{action}/{*id}", new { controller = "Admin", action = "Index", id = "" });

			// /user/123
			routes.MapRoute("UserDetail", "user/{id}/{action}", new { controller = "User", action = "Profile", id = "" }, new { id = @"\d+" });

			// /user/action
			routes.MapRoute("User", "user/{action}", new { controller = "User" });

			// /post/123
			routes.MapRoute("PostDetail", "post/{id}/{*slug}", new { controller = "Post", action = "Index", id = "", slug = "" }, new { id = @"\d+" });

			// /post/reply|edit
			routes.MapRoute("PostReply", "post/{action}", new { controller = "Post", action = "Reply" });
		}

		private void RegisterViewEngines(ViewEngineCollection engines) {
			engines.Clear();

			Spark.SparkSettings settings = new Spark.SparkSettings();
			settings
				.AddAssembly("ManagedAssembly.Web")
				.AddAssembly("System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35")
				.AddAssembly("System.Web.Routing, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35")
				.AddNamespace("System")
				.AddNamespace("System.Web")
				.AddNamespace("System.Web.Mvc")
				.AddNamespace("System.Web.Mvc.Html")
				.AddNamespace("System.Linq")
				.AddNamespace("System.Collections.Generic")
				.AddNamespace("Microsoft.Web.Mvc")
				.AddNamespace("SubSonic.Extensions")
				.AddNamespace("ManagedAssembly.Web")
				.AddNamespace("ManagedAssembly.Data")
				.AddNamespace("ManagedAssembly.Services");

			SparkEngineStarter.RegisterViewEngine(engines, settings);
		}

		protected void Application_Start() {
			RegisterViewEngines(ViewEngines.Engines);
			RegisterRoutes(RouteTable.Routes);
		}

	}
}