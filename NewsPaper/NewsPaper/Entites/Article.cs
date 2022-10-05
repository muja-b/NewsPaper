using Microsoft.EntityFrameworkCore;

namespace NewsPaper.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User? Writer { get; set; }
        public List<Comment>? Comments { get; set; }
        public string Content { get; set; }
    }
}