using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SubSonic;

namespace ManagedAssembly.Data
{
	public class UserStatsView
	{
		public int UpVotes { get; set; }
		public int DownVotes { get; set; }
		public int PostUpVotes { get; set; }
		public int PostDownVotes { get; set; }
		public int Points { get; set; }
	}

	public class UserStatsViewRepository
	{
		public static UserStatsView GetByUser(int userId)
		{
			var db = new ManagedAssemblyDB();
			var list = db.CalculateUserStats(userId).ExecuteTypedList<UserStatsView>();

			return list.FirstOrDefault();
		}
	}
}
