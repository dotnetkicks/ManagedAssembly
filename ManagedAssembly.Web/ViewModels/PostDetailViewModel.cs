using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web
{
	public class PostDetailViewModel : ViewModelBase
	{
		public Post Post { get; set; }
		public List<Post> TopLevelComments { get; set; }
		public List<int> UpVoteIds { get; set; }
		public List<int> DownVoteIds { get; set; }
	}
}
