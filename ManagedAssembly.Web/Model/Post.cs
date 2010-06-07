using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Services;
using System.Text.RegularExpressions;

namespace ManagedAssembly.Data
{
	public partial class Post
	{
		private List<Tag> _tags = null;
		public List<Tag> Tags {
			get {
				if (_tags == null) {
					var repo = new TagRepository();
					_tags = repo.ListByPost(this.PostId);
				}

				return _tags;
			}
		}

		private List<Post> _children = null;
		public List<Post> Children {
			get {
				if (_children == null) {
					var repo = new PostRepository();
					_children = repo.List(this.PostId, 1, int.MaxValue, true);
				}

				return _children;
			}
		}

		private Post _parent;
		public Post Parent {
			get {
				if (_parent == null) {
					_parent = new PostRepository().GetById(this.ParentId.GetValueOrDefault(0));
				}
				return _parent;
			}
		}

		private Post _topMost;
		public Post TopMost {
			get {
				if (_topMost == null) {
					_topMost = new PostRepository().GetById(this.TopMostId.GetValueOrDefault(0));
				}
				return _topMost;
			}
		}

		private User _user;
		public User User {
			get {
				if (_user == null) {
					_user = new UserRepository().GetById(this.UserId);
				}
				return _user;
			}
		}

		public bool IsDiscussion {
			get {
				return !string.IsNullOrEmpty(this.Contents);
			}
		}

		public bool IsComment {
			get {
				return this.ParentId.HasValue;
			}
		}

		public bool IsLink {
			get {
				return !IsDiscussion;
			}
		}

		public string Permalink {
			get {
				return string.Format("http://managedassembly.com/post/{0}/{1}", PostId.ToString(), this.Slug);
			}
		}
		
		public string FeedLink {
			get {
				return IsDiscussion ? this.Permalink : Url;
			}
		}

		public string FeedTitle {
			get {
				string title = Title;

				if (IsLink)
					title = string.Format("{0} ({1})", title, DomainName);

				return title;
			}
		}

		public string FeedContents {
			get {
				string output = string.Format("<a href=\"{0}\">Comments</a>", Permalink); ;

				if (IsDiscussion)
					output = string.Format("{0}<p>{1}</p>", Contents, output);

				return output;
			}
		}
	}
}
