namespace NewsPaper.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public Article Article { get; set; }
        public string  Content { get; set; }
    }
}