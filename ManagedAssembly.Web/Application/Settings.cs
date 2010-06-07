using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SubSonic.Extensions;

namespace ManagedAssembly.Web
{
	public class Settings
	{
		public class Feed
		{
			public static string PopularFeedUrl {
				get {
					return Settings.Get<string>("Feed.PopularFeedUrl");
				}
			}
			public static string NewFeedUrl {
				get {
					return Settings.Get<string>("Feed.NewFeedUrl");
				}
			}
		}

		public class Site
		{
			public static string EncryptionSalt {
				get {
					return Settings.Get<string>("Site.EncryptionSalt");
				}
			}
			public static int SavedSignInDays {
				get {
					return Settings.Get<int>("Site.SavedSignInDays");
				}
			}
		}

		public class Thresholds
		{
			public static int DownvoteCommentHours {
				get {
					return Settings.Get<int>("Thresholds.DownvoteCommentHours");
				}
			}
			public static int MinimumDownvoteRep {
				get {
					return Settings.Get<int>("Thresholds.MinimumDownvoteRep");
				}
			}
			public static int MinimumPointsToUndoNoFollow {
				get {
					return Settings.Get<int>("Thresholds.MinimumPointsToUndoNoFollow");
				}
			}
			public static int CommentDownvoteCap {
				get {
					return Settings.Get<int>("Thresholds.CommentDownvoteCap");
				}
			}
			public static int CommentEditMinutes {
				get {
					return Settings.Get<int>("Thresholds.CommentEditMinutes");
				}
			}
			public static int MinimumSubmitRep {
				get {
					return Settings.Get<int>("Thresholds.MinimumSubmitRep");
				}
			}
			public static int MinimumProfileLinkRep {
				get {
					return Settings.Get<int>("Thresholds.MinimumProfileLinkRep");
				}
			}

		}

		private static T Get<T>(string key) {
			return (T)ConfigurationManager.AppSettings[key].ChangeTypeTo<T>();
		}
	}
}