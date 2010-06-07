using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;

namespace ManagedAssembly.Data
{
	public class TagRepository : RepositoryBase<Tag>
	{
		public List<Tag> ListByPost(int postId)
		{
			var query = from t in DB.Tags
						join pt in DB.PostTags on t.TagId equals pt.TagId
						where pt.PostId == postId
						select t;

			return GetList(query);
		}

		public List<Tag> ListAll()
		{
			var query = from t in DB.Tags
						orderby t.Label
						select t;

			return GetList(query);
		}
	}
}
