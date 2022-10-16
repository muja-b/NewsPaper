namespace NewsPaper.Models
{
    public class UserDTO
    {
        public int? UserId { get; set; } 
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public List<UserDTO>? Followed { get; set; }
        public List<UserDTO>? Following { get; set; }

        public List<ArticleDTO>? FavArcticles { get; set; }
    }
}
