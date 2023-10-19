using System;
using Microsoft.EntityFrameworkCore;
using miniprojekt_web_api.Data;
using Shared.Model;



namespace miniprojekt_web_api.Service
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
			Post posts = db.Posts.FirstOrDefault()!;
			if (posts == null)
			{
				posts = new Post();
				posts = new Post("Dette er mit post", DateTime.Now, "Søren peter", 0, 0);
				posts.Comment.Add(new Comment("Dette er en kommentar", DateTime.Today, "Jørgen Michaelsen", 0, 0));
				db.Posts.Add(posts);
			}

            db.SaveChanges();
        }




                /*
                Post posts = db.Posts.FirstOrDefault()!;
                if (posts == null)
                {
                    posts = new Post { Name = "Søren peter", Text = "hvordan bliver ejg højere?"};
                    db.Posts.Add(posts);
                    db.Posts.Add(new Post { Name = "Sofie", Text = "Hvordan falder jeg i søvn?"});
                    db.Posts.Add(new Post { Name = "Jørgen", Text= "Hvorofor er ejg så grim" });
                    */
            
	
              

        //Post
	
        public List<Post> GetPosts()
		{
			return db.Posts.Include(c => c.Comment).ToList();
		}
	
		/*
        public List<Post> GetPosts()
        {
            return db.Posts.Include(b => b.PostId).ToList();
        }
	*/
        public Post GetPost(int id)
		{
			return db.Posts.Include(c => c.Comment).FirstOrDefault(p => p.PostId == id)!;
		}


		//Comments
        public List<Comment> GetComments(int postId)
        {
			return db.Posts.Include(c => c.Comment).FirstOrDefault(c => c.PostId == postId)!.Comment.ToList();
        }

		public Comment GetComment(int postId, int commentId)
		{
			return db.Posts.FirstOrDefault(p => p.PostId == postId)!.Comment.FirstOrDefault(c => c.CommentId == commentId)!;
		}


		public string CreateComment(string text,DateTime dateTime,string name,int upvote,int downvote, int postId)
		{
			Post post = db.Posts.FirstOrDefault(p => p.PostId == postId)!;
			Comment comments = new Comment(text,dateTime,name, upvote, downvote);
			post.Comment.Add(comments);
			return "comment created" + comments.CommentId;  

		}
    }
}

