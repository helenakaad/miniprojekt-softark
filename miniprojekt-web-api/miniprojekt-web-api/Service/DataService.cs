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
            /*
			Post posts = db.Posts.FirstOrDefault()!;
			if (posts == null)
			{
				posts = new Post();
				posts = new Post("Dette er mit post", DateTime.Now, "Søren peter", 0, 0);
                posts = new Post("Hvorfor er jeg så flot", DateTime.Now, "Petersen", 1, 0);
                posts = new Post("hvorfor skal man arbejde?", DateTime.Now, "sofie", 0, 0);
                posts.Comment.Add(new Comment("Dette er en kommentar", DateTime.Today, "Jørgen Michaelsen", 0, 0));
				db.Posts.Add(posts);*/
		
            Post posts = db.Posts.FirstOrDefault()!;
            if (posts == null)
            {
                posts = new Post("vhorfor skal det være sådan", DateTime.Now, "peterpeter120", 0, 0);
                db.Posts.Add(posts);
				db.Posts.Add(new Post("Dette er mit post", DateTime.Now, "Sørenpeter22", 0, 0));
                db.Posts.Add(new Post( "dette er ikke fedt", DateTime.Now, "JørgenSørenfem", 0, 0 ));
            }

            Comment comment = db.Comments.FirstOrDefault()!;
            if (comment == null)
            {
                comment = new Comment("Hvad er det her?", DateTime.Now, "Jullemulle55", 0,0);
                db.Comments.Add(comment);
                db.Comments.Add(new Comment("Nej det er mit", DateTime.Now, "Hvemkunnevide0", 0, 0));
                db.Comments.Add(new Comment("Jo da", DateTime.Now, "femfire45", 0, 0));
            }
            db.SaveChanges();
        }




                
             
	
              

        //Post
        public List<Post> GetPosts()
		{
			return db.Posts.ToList();
		}
	
        public Post GetPost(int id)
		{
			return db.Posts.Include(c => c.Comment).FirstOrDefault(p => p.PostId == id)!;
		}

		public string CreatePost(string text, DateTime dateTime, string name, int upvote, int downvote)
		{
			db.Posts.Add(new Post(text, dateTime, name, upvote, downvote));
				db.SaveChanges();
			return "Post created";
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

