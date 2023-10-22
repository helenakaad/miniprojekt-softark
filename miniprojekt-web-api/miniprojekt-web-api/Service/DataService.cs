using Microsoft.EntityFrameworkCore;
using Data;
using Shared.Model;

namespace Service
{
	public class DataService
	{
		private PostContext db { get; }

		public DataService(PostContext db)
		{
			this.db = db;
		}

		public void SeedData()
		{
			Post post = db.Post.FirstOrDefault()!;
			if (post == null)
			{
				db.Post.Add(new Post("jeg opfandt vand AMA", "vandmand"));
				db.Post.Add(new Post("hjælp til at vælge is", "ismand"));
				db.Post.Add(new Post("jeg har lige slukket nytårsbålet AITA", "brandmand"));
			}
			//if (post.Comments.FirstOrDefault() == null)
			//{
   //             List<Comments> comments = new List<Comments> { new Comments("fedt", "fedtmand"), new Comments("wwwwww", "upsmand") };
   //             db.Post.FirstOrDefault(b => b.PostId == 1).Comments = comments;
   //         }
			db.SaveChanges();
		}

		public List<Post> GetPosts()
		{
			return db.Post.Include(b => b.Comments).ToList();
		}

		public Post GetPost(int id)
		{
			return db.Post.Include(b => b.Comments).FirstOrDefault(b => b.PostId == id);
		}

		public string CreatePost(string text, string name)
		{
			db.Post.Add(new Post(text, name));
			db.SaveChanges();
			return "Post created";
		}

		public string CreateComment(string text, string name, int postId)
		{
			db.Post.FirstOrDefault(a => a.PostId == postId).Comments.Add(new Comments(text, name));
			db.SaveChanges();
			return "Comment created";
		}

		public void VoteComment(int postId, int commentId, int vote)
		{
			db.Post.FirstOrDefault(a => a.PostId == postId).Comments.FirstOrDefault(a => a.CommentsId == commentId).Votes += vote;
			db.SaveChanges();
		}

		public void VotePost(int postId, int vote)
		{
			db.Post.FirstOrDefault(a => a.PostId == postId).Votes += vote;
			db.SaveChanges();
		}
	}
}

