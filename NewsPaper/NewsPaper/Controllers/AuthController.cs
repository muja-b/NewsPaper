using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(IAuthRepo auth,IConfiguration configuration,IUserRepo userRepo,IPasswordHasher passwordHasher)
        {
            _authRepo=auth;
            _configuration = configuration;
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }
        // POST api/<AuothController>
        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthResult>> Authinticate(AuthRequestBody Auth)
        {
            var tokenRes=await _authRepo.GenerateToken(Auth);
            var RefreshToken = await _authRepo.GenerateRefreshToken(Auth);
            tokenRes.Refreshtoken = RefreshToken.Token;
            await _authRepo.SaveChanges();
            return Ok(tokenRes);
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
            ReqUser.Password = _passwordHasher.GeneratePasswordHash(ReqUser.Password);
            await _userRepo.addUsersAsync(ReqUser);
            await _userRepo.SaveChangesAsync();
            return  await _authRepo.GenerateToken(new AuthRequestBody() {
            Name = ReqUser.Name,
            Password = ReqUser.Password,
            });
        }
        
        [HttpDelete("LogOut")]
        public async Task<ActionResult> Logout(string token)
        {
            var validReq=await _authRepo.DeleteToken(token);
            if (!validReq) {
                return BadRequest();
            }
            _authRepo.SaveChanges();
            return Ok();
        }
    }    
}
