using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web
{
	public class IndexViewModel
	{
		public PostListViewModel PostListViewModel { get; set; }
		public int PageNumber { get; set; }
	}
}
