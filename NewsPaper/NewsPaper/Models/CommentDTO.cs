namespace NewsPaper.Models
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string Content { get; set; }
    }
}