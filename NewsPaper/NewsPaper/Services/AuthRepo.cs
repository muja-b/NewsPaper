using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsPaper.DpContext;
using NewsPaper.Entites;
using NewsPaper.Models;
using NewsPaper.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsPaper.Services
{
    public class AuthRepo :IAuthRepo
    {
        private readonly ModelContext _con;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthRepo(ModelContext con,IPasswordHasher passwordHasher,IConfiguration configuration)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task AddAsync(Token refreshToken)
        {
            await _con.Tokens.AddAsync(refreshToken);
        }

        public async Task<AuthResult> GenerateRefreshToken(AuthRequestBody Auth)
        {
             var jwtTokenHandler = new JwtSecurityTokenHandler();
            var userValid =await ValidateUser(Auth);
            if (!userValid) return new AuthResult()
            {
                Success = false,
                Error = new() { "User Doesnt Exist" },
            };
            var user = await _con.users.FirstOrDefaultAsync(a => a.Name.Equals(Auth.Name));
            var securekey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["keys:RefreshKey"]));
            var signingCreds=new SigningCredentials(securekey,SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddMonths(6),
                SigningCredentials =signingCreds
                };
            
            var tokenToReturn = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(tokenToReturn);
            return new AuthResult()
            {
                Error = new(),
                Success = true,
                Token = jwtToken
            };


        }

        public async Task<AuthResult> GenerateToken(AuthRequestBody Auth)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var userValid =await ValidateUser(Auth);
            if (!userValid) return new AuthResult()
            {
                Success = false,
                Error = new() { "User Doesnt Exist" },
            };
            var user = await _con.users.FirstOrDefaultAsync(a => a.Name.Equals(Auth.Name));
            var securekey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["keys:SecretForKey"]));
            var signingCreds=new SigningCredentials(securekey,SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("Name", user.Name));
            claimsForToken.Add(new Claim("Jti", Guid.NewGuid().ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsForToken),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials =signingCreds
                };
            
            var tokenToReturn = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(tokenToReturn);
            _con.Tokens.Add(new Token()
            {
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddHours(6),
                IsRevoked = false,
                IsUsed = true,
                myToken = jwtToken,
                userId = user.UserId,
                Username=user.Name
            });
            return new AuthResult()
            {
                Error = new(),
                Success = true,
                Token = jwtToken
            };

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

        public async Task<bool> ValidateRefreshToken(string token)
        {
            return await _con.Tokens.AnyAsync(a => a.myToken.Equals(token));
        }

        public async Task<bool> ValidateUser(AuthRequestBody Auth)
        {
            var user= await _con.users.FirstOrDefaultAsync(a=>a.Name.Equals(Auth.Name));
            if (user == null)return false;
            var passCorrect = _passwordHasher.VerifyPassword(user.Password, Auth.Password);
            if(passCorrect)return true;
            return false;
        }
        public async Task<bool> DeleteToken(string token)
        {
            var Token = await _con.Tokens.FirstOrDefaultAsync(a => a.myToken.Equals(token));
            if(Token == null)return false;
            _con.Tokens.Remove(Token);
            return true;
        }
    }
}
