namespace NewsPaper.Models
{
    public class UserDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public List<UserDTO> FollowedUsers { get; set; }
        public List<ArticleDTO> FavArcticles { get; set; }
    }
}
