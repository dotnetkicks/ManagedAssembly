using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SubSonic;

namespace ManagedAssembly.Data
{
	public class PostRepository : RepositoryBase<Post>
	{
		public List<Post> ListAll() {
			var query = from p in DB.Posts
						select p;

			return GetList(query);
		}

		public List<Post> ListSubmissionsByUser(int userId, int pageNum, int pageSize) {
			var query = from p in DB.Posts
						where p.ParentId == null
							  && p.UserId == userId
							  && p.IsDeleted == false
							  && p.IsDestroyed == false
						orderby p.CreateDate descending
						select p;

			return GetList(query, pageNum, pageSize);
		}

		public List<Post> ListCommentsByUser(int userId, int pageNum, int pageSize) {
			var query = from p in DB.Posts
			            where p.ParentId != null
			                  && p.UserId == userId
			                  && p.IsDestroyed == false 
						orderby p.CreateDate descending
						select p;

			return GetList(query, pageNum, pageSize);
		}

		public List<Post> List(int? parentId, int pageNo, int pageSize, bool includeDeleted) {
			if (includeDeleted) {
				return GetList(DB.CommentList(parentId, pageNo, pageSize)); 
			}
			else {
				return GetList(DB.PostList(parentId, pageNo, pageSize));
			}
		}

		public List<Post> ListNew(int? parentId, int pageNo, int pageSize) {
			var query = from p in DB.Posts
						where p.IsSpam == false 
							  && p.IsDestroyed == false
						select p;

			if (parentId.HasValue)
				query = query.Where(p => p.ParentId == parentId.Value);
			else
				query = query.Where(p => p.ParentId == null);

			var ordered = query.OrderByDescending(p => p.CreateDate);

			return GetList(ordered, pageNo, pageSize);
		}

		public Post GetByUrl(string url) {
			var query = from p in DB.Posts
						where p.Url == url
						select p;

			return GetFirst(query);
		}

		public Post GetById(int postId) {
			return GetByKey(postId);
		}

		public List<Post> ListNewComments(int pageNo, int pageSize) {
			var query = from p in DB.Posts
						where p.IsSpam == false
							  && p.IsDestroyed == false
							  && p.ParentId != null
					    orderby p.CreateDate descending 
						select p;

			return GetList(query, pageNo, pageSize);			
		}

		public void Save(Post post) {
			if (post.PostId == 0) {
				Add(post);
			}
			else {
				Update(post);
			}
		}

		public List<Post> ListAllByUser(int userId) {
			var query = from p in DB.Posts
						where p.UserId == userId
						select p;

			return GetList(query);
		}
	}
}
