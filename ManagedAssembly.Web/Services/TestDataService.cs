using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Argotic.Syndication;
using ManagedAssembly.Data;

namespace ManagedAssembly.Services
{
	public class TestDataService
	{
		public static void PopulateTestData(params string[] feedUrls)
		{
			foreach (var feedUrl in feedUrls)
			{
				Random rnd = new Random();
				RssFeed feed = RssFeed.Create(new Uri(feedUrl));

				foreach (var item in feed.Channel.Items)
				{
					// create new post if one for that url doesn't already exist
					//Post post = PostService.CreatePost(rnd.Next(2,4), item.Title, item.Link.ToString(), null);

					//post.UpVotes = rnd.Next(0, 25);
					//post.CreateDate = DateTime.Now.AddHours(-rnd.Next(0, 7*24));

					//PostRepository.Save(post);

					// assign 3 random tags
					//List<int> usedTagIds = new List<int>();
					//TagCollection tags = TagRepository.ListAll();
					//int tagId = 0;
					//for (int i = 0; i < 3; i++)
					//{
					//    do
					//    {
					//        tagId = tags[rnd.Next(0, tags.Count)].TagId;
					//    } while (usedTagIds.Contains(tagId));
												
					//    PostService.AddTagToPost(post.PostId, tagId, 1);
					//    usedTagIds.Add(tagId);
					//}
				}
			}
		}
	}
}
