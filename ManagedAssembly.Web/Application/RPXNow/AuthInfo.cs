using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Web.RPXNow
{
	public class AuthInfo
	{
		public Profile Profile { get; set; }
		public string Stat { get; set; }
	}

	public class Profile
	{
		public string DisplayName { get; set; }
		public string PreferredUsername { get; set; }
		public string Url { get; set; }
		public string ProviderName { get; set; }
		public string Identifier { get; set; }
	}
}
