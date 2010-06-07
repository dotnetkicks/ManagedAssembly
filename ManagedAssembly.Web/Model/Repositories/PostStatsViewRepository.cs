using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SubSonic;

namespace ManagedAssembly.Data
{
	public class PostStatsView
	{
		public int UpVotes { get; set; }
		public int DownVotes { get; set; }
		public int Points { get; set; }
		public int Decimal { get; set; }
		public decimal? Weight { get; set; }
	}

	public class PostStatsViewRepository
	{
		public PostStatsView GetByPost(int postId)
		{
			var db = new ManagedAssemblyDB();
			var list = db.CalculatePostStats(postId).ExecuteTypedList<PostStatsView>();

			return list.FirstOrDefault();
		}
	}
}
