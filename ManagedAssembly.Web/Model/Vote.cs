using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Data
{
	public partial class Vote
	{

		private VoteDirection _voteDirection;
		public VoteDirection VoteDirection {
			get {
				if (_voteDirection == null) {
					_voteDirection = new VoteDirectionRepository().GetById(this.VoteDirectionId);
				}
				return _voteDirection;
			}
		}

		private Post _post;
		public Post Post {
			get {
				if (_post == null) {
					_post = new PostRepository().GetById(this.PostId);
				}
				return _post;
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
	}
}
