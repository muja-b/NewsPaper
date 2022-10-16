using Microsoft.EntityFrameworkCore;
using NewsPaper.Entites;
using NewsPaper.Models;
namespace NewsPaper.DpContext
{
    public class ModelContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public string DbPath { get; set; }

        public ModelContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "NewsPaper.db");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new CommentConfiguration());
        //    modelBuilder.ApplyConfiguration(new UserConfiguration());
        //    modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=NewsPaper;trusted_connection=true;");
        }
    }
}
