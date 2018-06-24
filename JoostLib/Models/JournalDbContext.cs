using JoostLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace JoostLib
{
	class JournalDbContext : DbContext
	{
		static readonly String ConnectionString =
			"Trusted_Connection=True;Data Source=(Local);Database=Joost;MultipleActiveResultSets=true";
		//public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) { }
		public JournalDbContext() : base(ConnectionString) { }

		//protected override void OnModelCreation(ModelBuilder modelBuilder)
		//{
		//}

		public DbSet<Note> Notes { get; set; }
		public DbSet<Tag> Tags { get; set; }
	}
}
