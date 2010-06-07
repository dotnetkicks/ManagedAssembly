using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Data
{
	public partial class VoteDirectionRepository : RepositoryBase<VoteDirection>
	{
		public VoteDirection GetById(int id) {
			return GetByKey(id);
		}
	}
}
