using NewsPaper.Models;

namespace NewsPaper.Services
{
    public interface IArticleRepo
    {
        Task<IEnumerable<ArticleDTO>> GetArticles(int pageNum,int pageSize);  
        Task<ArticleDTO?> GetArticle(int id);
        Task addArticleAsync(ArticleDTO value);
        Task<bool> ArticleExists(ArticleDTO article);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteArticleAsync(int id);
        Task UpdateArticle(ArticleDTO value);
    }
}
