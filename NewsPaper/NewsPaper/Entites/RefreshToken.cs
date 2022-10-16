using NewsPaper.Models;

namespace NewsPaper.Entites
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string jwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public User user { get; set; }
    }
}
