using NewsPaper.Entites;
using NewsPaper.Models;

namespace NewsPaper.Services
{
    public interface IAuthRepo
    {
        Task AddAsync(Token refreshToken);
        string RandomString(int v);
        Task<bool> SaveChanges();
        Task<bool> ValidateUser(AuthRequestBody Auth);
        Task<AuthResult> GenerateToken(AuthRequestBody Auth);
        Task<AuthResult> GenerateRefreshToken(AuthRequestBody auth);
        Task<bool> ValidateRefreshToken(string token);
        Task<bool> DeleteToken(string token);   
    }
}
