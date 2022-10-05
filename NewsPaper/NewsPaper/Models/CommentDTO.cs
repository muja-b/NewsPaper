namespace NewsPaper.Models
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public UserDTO? User { get; set; }
        public ArticleDTO Article { get; set; }
        public string Content { get; set; }
    }
}