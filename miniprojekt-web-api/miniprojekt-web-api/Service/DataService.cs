using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Data;
using Shared.Model;
using System.Collections.Generic;

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
			db.Post.Add(new Posts { Name = "SejeReje43", Text = "Am I the Asshole?" });
            db.Post.Add(new Posts { Name = "TivoliTommy", Text = "Hjælp til arbejde" });
            db.Post.Add(new Posts { Name = "SmukkeSally", Text = "Hvordan bliver jeg filmstjerne?" });
        }

		db.SaveChanges();
	}

	public List<Posts> GetPosts()
	{
		return db.Post.ToList();
	}
}

