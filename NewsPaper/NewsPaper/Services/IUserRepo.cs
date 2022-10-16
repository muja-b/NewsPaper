using NewsPaper.Models;

namespace NewsPaper.Services
{
    public interface IUserRepo 
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserAsync(int id);
        Task<User> GetUserAsync(string name);
        Task addUsersAsync(UserDTO value);
        Task<bool> UserExists(string userName);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(UserDTO value);
        Task addFollower(int follower,int followed);
        Task <List<UserDTO>> GetFollowers(int id);
        Task <List<UserDTO>> GetFollowing(int id);
        Task<List<ArticleDTO>> GetFavArticles(int id);
        Task addFavArticles(int UserId,int artId);

    }
}
