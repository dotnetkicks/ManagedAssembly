using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ManagedAssembly.Data;
using ManagedAssembly.Services;

namespace ManagedAssembly.Web.Controllers
{
	[HandleErrors]
	public class PostController : ControllerBase
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

		public PostController() {
			_userService = new UserService();
			_voteService = new VoteService();
			_postService = new PostService();
		}

		public ActionResult Index(int id)
		{
			Post post = PostService.GetById(id);
			if (post.IsDestroyed) {
				return RedirectToAction("Index", "Home");
			}

			List<Post> comments = PostService.ListComments(id, 1, int.MaxValue);

			PostDetailViewModel model = new PostDetailViewModel();
			model.Post = post;
			model.TopLevelComments = comments;

			if (UserService.IsAuthenticated) {
				model.UpVoteIds = VoteService.ListUserUpVotePostIds(UserService.Current.UserId);
				model.DownVoteIds = VoteService.ListUserDownVotePostIds(UserService.Current.UserId);
			}

			model.ViewTitle = post.Title;

			return View(model);
		}

		public ActionResult New(int? page) {
			int pageNo = page.HasValue ? page.Value : 1;

			NewViewModel model = new NewViewModel();
			model.PostListViewModel = new PostListViewModel();
			model.PostListViewModel.Posts = PostService.ListNew(pageNo, 30);
			model.PageNumber = pageNo;

			if (UserService.IsAuthenticated) {
				model.PostListViewModel.UpVoteIds = VoteService.ListUserUpVotePostIds(UserService.Current.UserId);
				model.PostListViewModel.DownVoteIds = VoteService.ListUserDownVotePostIds(UserService.Current.UserId);
			}

			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public PartialViewResult PostEdit(int id) {
			var model = new PostEditViewModel();
			model.Post = PostService.GetById(id);
			return PartialView(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult PostEdit(int PostId, string Title, string Url, string Contents) {
			PostService.EditPost(UserService.Current, PostId, Title, Url, Contents);
			return Json(new { status="success" });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public PartialViewResult CommentEdit(int id) {
			var model = new CommentEditViewModel();
			model.Post = PostService.GetById(id);
			return PartialView(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CommentEdit(int PostId, string Contents) {
			Post comment = PostService.EditComment(UserService.Current, PostId, Contents);

			return Content(comment.Contents);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id) {
			string type = PostService.DeletePost(UserService.Current, id);
			return Json(new { status=type });
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public PartialViewResult Reply(int id) {
			ReplyViewModel model = new ReplyViewModel();
			model.Id = id;
			return PartialView(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public PartialViewResult Reply(int ParentId, string Contents) {
			Post parent = PostService.GetById(ParentId);
			Post comment = PostService.CreateComment(UserService.Current, parent, Contents);

			CommentViewModel model = new CommentViewModel();
			model.Comment = comment;

			return PartialView("Comment", model);
		}
	}
}
