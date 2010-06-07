using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Data
{
	public class UserRepository : RepositoryBase<User>
	{
		public User GetById(int userId)
		{
			return GetByKey(userId);
		}

		public User GetByExternalKey(string key)
		{
			var query = from u in DB.Users
						where u.ExternalKey == key
						select u;

			return GetFirst(query);
		}

		public List<User> ListLatest(int pageNo, int pageSize) {
			var query = from u in DB.Users
						orderby u.JoinedOn descending,
								u.UserId descending
						select u;

			return GetList(query, pageNo, pageSize);
		}

		public List<User> ListTop(int pageNo, int pageSize) {
			var query = from u in DB.Users
						orderby u.Points descending,
								u.UpVotes descending,
								u.JoinedOn descending
						select u;

			return GetList(query, pageNo, pageSize);
		}

		public void Save(User user) {
			if (user.UserId == 0) {
				Add(user);
			}
			else {
				Update(user);
			}
		}
	}
}