using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPaper.Models;

namespace NewsPaper.Entites
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("User");
            builder.HasData(
            new Article
            {
                Id = 1,
                Title = "Lone Wolf",
                Writer = null,
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus non viverra arcu, eleifend tempor neque. Mauris in tortor vitae nisi dapibus consequat in mollis elit. Phasellus ut tortor nunc. Maecenas placerat sapien in mauris suscipit convallis. Donec odio purus, iaculis quis ex nec, suscipit sagittis urna. Ut at congue tellus. Cras sit amet hendrerit diam, eget tincidunt felis."
            },
             new Article
             {
                 Id = 2,
                 Title = "How to make APIs",
                 Writer=null,
                 Content= "Phasellus suscipit, erat vel vehicula malesuada, libero ipsum accumsan dolor, a aliquet sem nisi a enim. Pellentesque sed faucibus nisl. Donec diam turpis, fringilla"
             }
            );
        }
    }
}
