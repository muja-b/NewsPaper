using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NewsPaper.Entites;
using NewsPaper.Models;
using NewsPaper.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPaper.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _authRepo;
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;

        public AuthController(IAuthRepo auth,IConfiguration configuration,IUserRepo userRepo)
        {
            _authRepo=auth;
            _configuration = configuration;
            _userRepo = userRepo;
        }
        // POST api/<AuothController>
        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResult>> Authinticate(AuthRequestBody Auth)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var userValid =await  _authRepo.ValidateUser(Auth);
            if (!userValid) return BadRequest(new AuthResult() { 
                Success = false,
                Error = new() { "User Doesnt Exist"},
            });
            var user =await  _userRepo.GetUserAsync(Auth.Name);
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
            //var refreshToken = new RefreshToken()
            //{
            //    jwtId = jwtSecurityToken.Id,
            //    IsUsed = false,
            //    IsRevoked = false,
            //    user = user,
            //    AddedDate = DateTime.UtcNow,
            //    ExpiryDate = DateTime.UtcNow.AddMonths(6),
            //    Token = _authRepo.RandomString(35) + Guid.NewGuid(),
            //    Username=user.Name
            //};
            //await _authRepo.AddAsync(refreshToken);
            //await _authRepo.SaveChanges();
            //return Ok(new AuthResult { 
            //Token = tokenToReturn,
            //Refreshtoken=refreshToken.Token,
            //Success=true
            //});
            var jwtToken = jwtTokenHandler.WriteToken(tokenToReturn);
            return Ok(new AuthResult() { 
                Token = jwtToken,
                Success = true,
            });
        }
    [HttpPost("SignUp")]
    public async Task<ActionResult<AuthResult>> SignUp([FromBody]UserDTO ReqUser)
        {
            if(await _userRepo.UserExists(ReqUser.Name))
                return BadRequest(new AuthResult()
                {
                    Token = null,
                    Success = false,
                    Error = new() {"User Exists"}
                });
            await _userRepo.addUsersAsync(ReqUser);
            await _userRepo.SaveChangesAsync();
            return  await Authinticate(new AuthRequestBody() {
            Name = ReqUser.Name,
            Password = ReqUser.Password,
            });
        }
    }    
}
