using Microsoft.EntityFramework;
using System;
using Shared.Model;

namespace Data
{
	public class ThreadContext : DbContext
	{
		public DbSet<Thread> Threads => Set<Board>();
		public DbSet<Comment> Comments => Set<Comment>();

		public ThreadContext (DbContextOptions<ThreadContext> options)
			: base(options)
		{

		}
	}
}

