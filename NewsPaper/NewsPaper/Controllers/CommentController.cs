using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsPaper.Models;
using NewsPaper.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsPaper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
            private readonly ICommentRepo _commentRepo;
            public CommentController(ICommentRepo comment)
            {
                _commentRepo = comment;
            }
            // GET: api/<ValuesController>

            // GET api/<ValuesController>/5
            [HttpGet("{id}")]
            public async Task<ActionResult<List<CommentDTO>>> Get(int id)
            {
                return Ok(await _commentRepo.GetCommentsAsync(id));
            }

            // POST api/<ValuesController>
            [HttpPost]
            public async Task<ActionResult> Post([FromBody] CommentDTO value)
            {
                var myComment = await _commentRepo.addCommentsAsync(value);
                if (myComment == false) return BadRequest();
                return Ok();
            }

            // DELETE api/<ValuesController>/5
            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int Articleid,int CommentId)
            {
            var myComment = await _commentRepo.DeleteCommentsAsync(Articleid,CommentId);
                if (myComment == false) return BadRequest();
                await _commentRepo.SaveChangesAsync();
                return Ok();

            }
        }
    }

