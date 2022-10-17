namespace NewsPaper.Services
{
    public interface IPasswordHasher
    {
        string GeneratePasswordHash(string password);
        bool VerifyPassword(string password,string HashedPassword);
    }
}
