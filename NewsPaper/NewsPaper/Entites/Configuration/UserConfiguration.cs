using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPaper.Models;

namespace NewsPaper.Entites
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasData(
               new User
               {
                   Id = 1,
                   Email = "SomeOne@gmail.com",
                   FavArcticles = null,
                   FollowedUsers = new List<User> { },
                   Name = "muja"
               },
                new User
                {
                    Id = 2,
                    Email = "two@gmail.com",
                    FavArcticles = null,
                    FollowedUsers = new List<User> { },
                    Name = "muja"
                }
               );
        }
    }
}
