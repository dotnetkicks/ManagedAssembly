$(document).ready(function() {
	$(".upvote").click(function() {
		var id = $(this).attr("id").split(/_/)[1];
		registerVote(id, "up");
	});

	$(".downvote").click(function() {
		var id = $(this).attr("id").split(/_/)[1];
		registerVote(id, "down");
	});

	function toggleFormBox(formbase, that, appendTo, url) {
		var id = that.attr("id").split(/_/)[1];
		var form = $(formbase + id);
		var status = that.data("status");
		if (status === "visible") {
			form.hide();
			that.data("status", "hidden");
			return;
		}

		if (status === "hidden") {
			form.show();
			that.data("status", "visible");
			return;
		}

		// else load it up fresh and show it
		$.get(url, { id: id }, function(data, status) { appendTo.append(data); }, "html");
		that.data("status", "visible");
	}

	$(".commentEdit").live("click", function(e) {
		e.preventDefault();
		toggleFormBox("#editform_", $(this), $(this).parent().parent(), "/Post/CommentEdit");
	});

	$(".commentReply").live("click", function(e) {
		e.preventDefault();
		toggleFormBox("#form_", $(this), $(this).parent().parent(), "/Post/Reply");
	});

	$(".editPost").click(function(e) {
		e.preventDefault();
		var that = $(this);
		var id = that.attr("id").split(/_/)[1];
		$("#form_" + id).toggle();

		toggleFormBox("#editform_", that, that.parent(), "/Post/PostEdit");
	});

	$(".respond").submit(function() {
		var that = $(this);
		postComment(that, $("#discussion"));
		// prevent actual form submit
		return false;
	});

	$(".replyForm input:submit").live("click", function(e) {
		e.preventDefault();
		var that = $(this).closest("form");
		postComment(that, that.parent().siblings(".kids"));
		that.fadeOut();
		that.data("status", "hidden");
		// prevent actual form submit
		return false;
	});

	$(".editForm input:submit").live("click", function(e) {
		e.preventDefault();
		var that = $(this).closest("form");
		editComment(that);
		that.fadeOut();
		that.data("status", "hidden");
		// prevent actual form submit
		return false;
	});

	$(".editPostForm input:submit").live("click", function(e) {
		e.preventDefault();
		var that = $(this).closest("form");
		editPost(that);
		that.fadeOut();
		that.data("status", "hidden");
		// prevent actual form submit
		return false;
	});

	$(".deletePost").live("click", function(e) {
		e.preventDefault();
		if (confirm('Are you sure? This cannot be undone.')) {
			var that = $(this);
			var id = that.attr("id").split(/_/)[1];
			$.post("/Post/Delete", { id: id }, function(data, status) { finishDelete(data.status, id); }, "json");
		}
	});

	$("#feedsLink").toggle(function(e) {
		e.preventDefault();
		var top = $(this).position().top + 20;
		var left = $(this).position().left;
		$("#feeds").css('top', top);
		$("#feeds").css('left', left);
		$("#feeds").show();
	},
	function() {
		$("#feeds").hide();
	});
});

function finishDelete(type, id) {
	if (type === "post")
		window.location = "/";
	else {
		$("#contents_" + id).text("[deleted]");
		$("#tools_" + id).hide();
	}
}

function registerVote(id, dir) {
	voteRegistered(id, dir);
	$.post("/Vote", { postId: id, direction: dir },
			function() {
				
			}
		);
}

function voteRegistered(id, dir) {
	$("#up_" + id).hide();
	$("#down_" + id).hide();

	$("#vote_" + dir + "_on_" + id).removeClass("hidden");

	var val = (dir == "down") ? -1 : 1;
	var count = $("#count_" + id);
	var tot = parseInt(count.text()) + val;
	count.text(tot);
}
function postComment(formSubmitted, elementToPrepend) {
	$.post("/Post/Reply", formSubmitted.serialize(), function(data, status) { elementToPrepend.prepend(data); }, "html");
	formSubmitted[0].reset();
}

function editComment(formSubmitted) {
	var id = $(":hidden", formSubmitted).val();
	$.post("/Post/CommentEdit", formSubmitted.serialize(), function(data, status) { $("#contents_" + id).html(data); }, "html");
}

function editPost(formSubmitted) {
	var id = $(":hidden", formSubmitted).val();
	$.post("/Post/PostEdit", formSubmitted.serialize(), function(data, status) { window.location = "/post/" + id; });
}
