using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;
using ManagedAssembly.Web;
using DotNetOpenId.RelyingParty;

namespace ManagedAssembly.Services
{
	public class UserService
	{
		private readonly UserRepository _userRepo;
		private UserRepository UserRepository {
			get {
				return _userRepo;
			}
		}

		public UserService() {
			_userRepo = new UserRepository();
		}

		public static User Current {
			get {
				CacheManager cache = new CacheManager(new RequestProvider());
				User user = cache.Get<User>("user");
				if (user != null)
					return user;

				user = LoadFromCookie();

				cache.Store("user", user);

				return user;
			}
		}

		public static bool IsAuthenticated {
			get {
				return (Current != null);
			}
		}

		public void RecalculatePoints(User user) {
			UserStatsView view = UserStatsViewRepository.GetByUser(user.UserId);
			user.UpVotes = view.UpVotes;
			user.DownVotes = view.DownVotes;
			user.PostUpVotes = view.UpVotes;
			user.PostDownVotes = view.PostDownVotes;
			user.Points = view.Points;

			UserRepository.Save(user);
		}

		public bool SignIn(string identifier, string displayName) {
			bool isNew = false;
			User user = UserRepository.GetByExternalKey(identifier);
			if (user == null) {
				// new user, so create them
				user = CreateUser(identifier, displayName);
				SetCurrent(user);
				isNew = true;
			}

			SaveToCookie(identifier);

			return isNew;
		}

		private void SetCurrent(User user) {
			CacheManager cache = new CacheManager(new RequestProvider());
			cache.Store("user", user);
		}

		protected User CreateUser(string externalKey, string displayName) {
			User user = new User();
			user.ExternalKey = externalKey;

			if (displayName != null) {
				if (displayName.StartsWith("me.yahoo.com")) {
					displayName = "unknown (yahoo)";
				}
				else if (displayName.StartsWith("www.google.com")) {
					displayName = "unknown (google)";
				}

				user.DisplayName = displayName;
			}
			else {
				user.DisplayName = "unknown";
			}

			user.JoinedOn = DateTime.Now;
			UserRepository.Save(user);

			return user;
		}

		public void SignOut() {
			DeleteCookie();
		}

		protected static User LoadFromCookie() {
			User user = null;
			HttpCookie cookie = HttpContext.Current.Request.Cookies["user"];
			if (cookie != null) {
				EncryptionHelper encryption = new EncryptionHelper(Settings.Site.EncryptionSalt);
				string key = encryption.DecryptString(cookie.Value);
				var userRepo = new UserRepository();
				user = userRepo.GetByExternalKey(key);
			}

			return user;
		}

		protected static void SaveToCookie(string key) {
			HttpCookie cookie = new HttpCookie("user");
			EncryptionHelper encryption = new EncryptionHelper(Settings.Site.EncryptionSalt);
			cookie.Value = encryption.EncryptToString(key);
			cookie.Expires = DateTime.Now.AddDays(Settings.Site.SavedSignInDays);

			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		protected static void DeleteCookie() {
			HttpCookie cookie = new HttpCookie("user");
			cookie.Expires = DateTime.Now.AddYears(-1);
			cookie.Value = null;
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public void Update(string DisplayName, string Email, string Url, string Twitter, string Bio, int? StackOverflowID) {
			User user = Current;
			user.DisplayName = HttpUtility.HtmlEncode(DisplayName);
			user.Email = HttpUtility.HtmlEncode(Email);
			user.Url = HttpUtility.HtmlEncode(Url);
			user.Twitter = HttpUtility.HtmlEncode(Twitter);
			user.BioRaw = Bio;
			user.Bio = Bio.Codify(false);
			user.StackOverflowID = StackOverflowID;
			UserRepository.Save(user);
		}

		public List<User> ListLatest(int pageNo, int pageSize) {
			return UserRepository.ListLatest(pageNo, pageSize);
		}

		public List<User> ListTop(int pageNo, int pageSize) {
			return UserRepository.ListTop(pageNo, pageSize);
		}

		public User GetById(int id) {
			return UserRepository.GetById(id);
		}

		public void Destroy(User user) {
			UserRepository.Delete(user.UserId);
		}

		public void UpdateOpenId(User user, string newExternalKey) {
			user.ExternalKey = newExternalKey;
			UserRepository.Save(user);
		}
	}
}
