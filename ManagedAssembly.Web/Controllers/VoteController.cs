using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ManagedAssembly.Services;
using ManagedAssembly.Data;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class VoteController : ControllerBase
	{
		protected readonly UserService _userService;
		protected UserService UserService {
			get {
				return _userService;
			}
		}

		protected readonly VoteService _voteService;
		protected VoteService VoteService {
			get {
				return _voteService;
			}
		}

		public VoteController() {
			_userService = new UserService();
			_voteService = new VoteService();
		}
	
		[Authenticate]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(int postId, string direction)
		{
			int voteDirection = (direction == "down") ? IDs.VoteDirection.Down : IDs.VoteDirection.Up;

			VoteService.RegisterVote(UserService.Current.UserId, postId, voteDirection);

			return Json(new { result="success" }); ;
		}
	}
}
