using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Data
{
	public class VoteRepository : RepositoryBase<Vote>
	{
		public Vote GetExisting(int postId, int userId, int? voteDirectionId)
		{
			var query = from v in DB.Votes
						where v.UserId == userId && v.PostId == postId
						select v;

			if (voteDirectionId.HasValue) {
				query = query.Where(v => v.VoteDirectionId == voteDirectionId.Value);
			}

			return GetFirst(query);
		}

		public List<int> ListUserVotePostIds(int userId, int directionId)
		{
			var query = from v in DB.Votes
			            where v.UserId == userId
			                  && v.VoteDirectionId == directionId
						select v.PostId;

			return query.ToList();
		}

		public List<Vote> ListLatest(int pageNo, int pageSize) {
			var query = from v in DB.Votes
						orderby v.CreateDate descending
						select v;

			return GetList(query, pageNo, pageSize);
		}

		public void Save(Vote vote) {
			if (vote.VoteId == 0) {
				Add(vote);
			}
			else {
				Update(vote);
			}
		}

		public List<Vote> ListByUser(int userId) {
			var query = from v in DB.Votes
						where v.UserId == userId
						select v;

			return GetList(query);
		}
	}
}
