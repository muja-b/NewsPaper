using Microsoft.EntityFrameworkCore;

namespace NewsPaper.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<User> Followed { get; set; }
        public List<User> Following { get; set; }
        public List<Article> FavArcticles { get; set; }   

    }
}
