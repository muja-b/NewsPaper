using Microsoft.EntityFrameworkCore;

namespace NewsPaper.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string? Title { get; set; }
        public string Writer { get; set; }
        public int UserId { get; set; }
        public List<Comment> Commented { get; set; }
        public string Content { get; set; }
    }
}