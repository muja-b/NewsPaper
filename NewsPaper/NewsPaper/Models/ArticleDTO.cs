namespace NewsPaper.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User? Writer { get; set; }
        public List<CommentDTO>? Comments { get; set; }
        public string Content { get; set; }
    }
}