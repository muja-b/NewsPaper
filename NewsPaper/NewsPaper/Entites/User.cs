using Microsoft.EntityFrameworkCore;

namespace NewsPaper.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public List<User>? FollowedUsers { get; set; }
        public List<Article>? FavArcticles { get; set; }   

    }
}
