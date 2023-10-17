using System;
using Shared.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace miniprojekt_web_api.Data
{
	public class PostContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        

        public string DbPath { get; }
        public PostContext(DbContextOptions<PostContext> options)
            : base(options)
        {
            DbPath = "bin/Post.db";
        }
    }

}

