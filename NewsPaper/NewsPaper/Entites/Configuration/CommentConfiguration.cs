using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPaper.Models;

namespace NewsPaper.Entites
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            var ent = new ModelContext();
            builder.ToTable("User");
            builder.HasData(
            new Comment
            {
                Id = 1,
                Article = ent.Articles.First(article => article.Id == 1),
                User = ent.users.First(user => user.Id == 1) ,
                Content = "Lorem ipsum dolor sit amet,rerit diam, eget tincidunt felis."
            },
             new Comment
             {
                 Id = 2,
                 Article = ent.Articles.First(article => article.Id == 1),
                 User = ent.users.First(user => user.Id == 2),
                 Content = "Phasellus suscipit, erat vel vehicula malesuada."
             }
            ); ;
        }
    }
}
