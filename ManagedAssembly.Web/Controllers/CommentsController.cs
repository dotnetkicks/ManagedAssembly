using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ManagedAssembly.Services;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
    public class CommentsController : Controller
    {		
		
		protected readonly PostService _postService;
		protected PostService PostService {
			get {
				return _postService;
			}
		}

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

		public CommentsController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}

        public ActionResult Index()
        {
			var model = new CommentsIndexViewModel();
			model.Comments = PostService.ListNewestComments(1, 30);
			if (UserService.IsAuthenticated) {
				model.UpVoteIds = VoteService.ListUserUpVotePostIds(UserService.Current.UserId);
				model.DownVoteIds = VoteService.ListUserDownVotePostIds(UserService.Current.UserId);
			}

        	return View(model);
        }

    }
}
