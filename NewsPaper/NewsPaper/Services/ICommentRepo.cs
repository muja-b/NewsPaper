using NewsPaper.Models;

namespace NewsPaper.Services
{
    public interface ICommentRepo
    {
        Task<List<CommentDTO>> GetCommentsAsync(int id);
        Task<bool> addCommentsAsync(CommentDTO value);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteCommentsAsync(int ArticleId,int CommentId);
    }
}
