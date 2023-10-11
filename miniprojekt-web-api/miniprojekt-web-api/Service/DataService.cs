using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Data;
using Shared.Model;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace Service;

public class DataService
{
	private PostsContext db { get; }

	public DataService(PostsContext db)
	{
		this.db = db;
	}

    //Seeder noget nyt data i databasen hvis det er nødvendigt.
	public void SeedData()
	{
		Posts posts = db.Post.FirstOrDefault()!;
		if (posts == null)
		{
			db.Post.Add(new Posts { Name = "SejeReje43", Text = "AITA for dumping water on a fish?", Header = "AITA" });
            db.Post.Add(new Posts { Name = "TivoliTommy", Text = "Hjælp til arbejde", Header = "HJÆLP" });
            db.Post.Add(new Posts { Name = "SmukkeSally", Text = "Hvordan bliver jeg filmstjerne?", Header = "Tips" });
        }

        Comments comments = db.Comments.FirstOrDefault()!;
        if (comments == null)
        {
            db.Comments.Add(new Comments { Name = "FlotteHans", Text = "Yes, YATH"});
            db.Comments.Add(new Comments { Name = "FrækkeFrederik", Text = "Gør det selv :)"});
            db.Comments.Add(new Comments { Name = "GrimmeGert", Text = "Det bliver du nok ikke"});
        }

        db.SaveChanges();
	}

	public List<Posts> GetPosts()
    {
        return db.Post.ToList();
    }

	public Posts GetSinglePost(int id)
	{
		return db.Post.Include(b => b.Comments).FirstOrDefault(a => a.PostsId == id);
	}

    public string CreatePost(string name, string text, string header)
    {
        db.Post.Add(new Posts(name, text, header));
		db.SaveChanges();
		return "Post created";
	}

	public string createComment(string name, string text, long postsId)
	{
		Posts posts = db.Post.FirstOrDefault(a => a.PostsId == postsId);
		posts.Comments.Add(new Comments { Name = name, Text = text });
		db.Update(posts);
		db.SaveChanges();
		return "Comment created";
	}
}
