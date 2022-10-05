using Microsoft.EntityFrameworkCore;
using NewsPaper.Controllers;
using NewsPaper.Entites;
using System;
using System.Collections.Generic;
namespace NewsPaper.Models
{
    public class ModelContext :DbContext
    {
    public DbSet<User> users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public string DbPath { get; set; }

        public ModelContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "NewsPaper.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new CommentConfiguration());
           modelBuilder.ApplyConfiguration(new UserConfiguration());
           modelBuilder.ApplyConfiguration( new ArticleConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=NewsPaper;trusted_connection=true;");
        }
    }
}
