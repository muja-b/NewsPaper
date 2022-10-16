using NewsPaper.Entites;
using NewsPaper.Models;

namespace NewsPaper.Services
{
    public interface IAuthRepo
    {
        Task AddAsync(RefreshToken refreshToken);
        string RandomString(int v);
        Task<bool> SaveChanges();
        Task<bool> ValidateUser(AuthRequestBody Auth);
    }
}
