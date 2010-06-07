using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web
{
	public class DashboardViewModel
	{
		public List<Post> LatestComments { get; set; }
		public List<Post> LatestSubmissions { get; set; }
		public List<Vote> LatestVotes { get; set; }
		public List<int> UpVoteIds { get; set; }
		public List<int> DownVoteIds { get; set; }
		public List<User> LatestUsers { get; set; }
		public List<User> TopUsers { get; set; }
	}
}
