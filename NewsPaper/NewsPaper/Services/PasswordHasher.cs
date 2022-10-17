namespace NewsPaper.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GeneratePasswordHash(string password)
        {
            return password;
        }

        public bool VerifyPassword(string password,string HashedPassword)
        {
            return password.Equals(HashedPassword);
        }
    }
}
