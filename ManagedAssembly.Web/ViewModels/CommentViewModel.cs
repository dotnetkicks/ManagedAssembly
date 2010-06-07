using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web
{
	public class CommentViewModel
	{
		public Post Comment { get; set; }
		public List<Post> Children { get; set; }
		public List<int> UpVoteIds { get; set; }
		public List<int> DownVoteIds { get; set; }
		public bool AllowReplies { get; set; }

		public CommentViewModel() {
			Children = new List<Post>();
		}
	}
}
