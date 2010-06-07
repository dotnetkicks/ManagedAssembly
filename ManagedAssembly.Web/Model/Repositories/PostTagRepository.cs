using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Data
{
	public class PostTagRepository : RepositoryBase<PostTag>
	{
		public void Save(PostTag pt) {
			if (pt.PostTagId == 0) {
				Add(pt);
			}
			else {
				Update(pt);
			}
		}
	}
}
