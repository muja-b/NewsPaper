using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPaper.Models;
using NewsPaper.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPaper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]    
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepo _articleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public ArticleController(IArticleRepo article, IMapper mapper, IUserRepo userRepo)
        {
            _articleRepo = article;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDTO>>> Get()
        {
            var Artlist =await _articleRepo.GetArticles();
            return Ok(Artlist);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDTO>> Get(int id)
        {
            var article= await _articleRepo.GetArticle(id);
            return Ok(article);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ArticleDTO value)
        {
            if(await _articleRepo.ArticleExists(value))
                return BadRequest();
            if(!await _userRepo.UserExists(value.Writer))
                return BadRequest();
            await _articleRepo.addArticleAsync(value);
            await _articleRepo.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] ArticleDTO value)
        {
        if(await _articleRepo.GetArticle(value.ArticleId)==null)
            return BadRequest();
        var isUpdated=_articleRepo.UpdateArticle(value);
        await _articleRepo.SaveChangesAsync();
        return Ok();
        
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isThere=await _articleRepo.DeleteArticleAsync(id);
            if (isThere)
            {
            await _articleRepo.SaveChangesAsync();
            return Ok();
            }
            else return BadRequest();
        }
    }
}
