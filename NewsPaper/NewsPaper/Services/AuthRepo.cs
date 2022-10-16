using Microsoft.EntityFrameworkCore;
using NewsPaper.DpContext;
using NewsPaper.Entites;
using NewsPaper.Models;
using NewsPaper.Services;
using System;

namespace NewsPaper.Services
{
    public class AuthRepo :IAuthRepo
    {
        private readonly ModelContext _con;

        public AuthRepo(ModelContext con)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _con.refreshTokens.AddAsync(refreshToken);
        }

        public String RandomString(int v)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, v)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<bool> SaveChanges()
        {
            return (await _con.SaveChangesAsync() >= 0);

        }

        public async Task<bool> ValidateUser(AuthRequestBody Auth)
        {
            var user= await _con.users.FirstOrDefaultAsync(a=>a.Name.Equals(Auth.Name));
            if (user == null)return false;
            var passCorrect=user.Password.Equals(Auth.Password);
            if(passCorrect)return true;
            return false;
        }
    }
}
