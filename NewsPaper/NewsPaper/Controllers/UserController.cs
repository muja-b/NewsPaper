using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPaper.Models;
using NewsPaper.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPaper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepo _userRepo;
        private readonly IArticleRepo _articleRepo;
        private readonly IPasswordHasher _passwordHasher;

        public UserController(IUserRepo userRepo,IArticleRepo articleRepo,IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _articleRepo = articleRepo;
            _passwordHasher = passwordHasher; 
        }
        
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            return await _userRepo.GetUsersAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var myUser=await _userRepo.GetUserAsync(id);
            if (myUser == null) return BadRequest();
            return Ok(myUser);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO value)
        {
        if(await _userRepo.UserExists(value.Name))
            return BadRequest();
        value.Password=_passwordHasher.GeneratePasswordHash(value.Password);
        await _userRepo.addUsersAsync(value);
        await _userRepo.SaveChangesAsync();
        return Ok();
        }
        //POST api/<ValuesController>/Users/follow
        [HttpPost("id/follow")]
        public async Task<ActionResult> PostFollowedUser(int UserId,[FromBody] int Followed)
        {
        var myFollowed=await _userRepo.GetUserAsync(Followed);
        if(myFollowed == null) return BadRequest();

        var myUser=await _userRepo.GetUserAsync(UserId);
        if(myUser == null) return BadRequest();

        await _userRepo.addFollower(UserId,Followed);
        await _userRepo.SaveChangesAsync();
        return Ok();
        }
        [HttpGet("id/follow/followers")]
        public async Task<ActionResult<UserDTO>> GetFollowers(int id)
        {
            var myUser=await _userRepo.GetUserAsync(id);
            if(myUser==null) return BadRequest();
            myUser.Followed =await _userRepo.GetFollowers(id);
            if (myUser == null) return BadRequest();
            return Ok(myUser.Followed);
        }
        [HttpGet("id/follow/following")]
        public async Task<ActionResult<UserDTO>> GetFollowing(int id)
        {
            var myUser=await _userRepo.GetUserAsync(id);
            if(myUser==null) return BadRequest();
            myUser.Followed =await _userRepo.GetFollowing(id);
            if (myUser == null) return BadRequest();
            return Ok(myUser.Followed);
        }
        [HttpPost("id/FavArticles")]
        public async Task<ActionResult> PostFavArticles(int UserId,[FromBody] int ArticleId)
        {
        var myFollowed=await _articleRepo.GetArticle(ArticleId);
        if(myFollowed == null) return BadRequest();

        var myUser=await _userRepo.GetUserAsync(UserId);
        if(myUser == null) return BadRequest();

        await _userRepo.addFavArticles(UserId,ArticleId);
        await _userRepo.SaveChangesAsync();
        return Ok();
        }
        [HttpGet("id/FaveArticles")]
        public async Task<ActionResult<List<ArticleDTO>>> GetFavArticles(int id)
        {
            var myArticles=await _userRepo.GetFavArticles(id);
            if(myArticles==null) return BadRequest();
            var user=await _userRepo.GetUserAsync(id);
            if(user==null) return BadRequest();
            return Ok(myArticles);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UserDTO value)
        {
            var found =await _userRepo.UpdateUserAsync(value);
            if (found == true) {
                await _userRepo.SaveChangesAsync();
                return Ok();
            } 
            return BadRequest();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isThere=await _userRepo.DeleteUserAsync(id);
            if (isThere)
            {
            await _userRepo.SaveChangesAsync();
            return Ok();
            }
            else return BadRequest();

        }
    }
}
