using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsPaper.DpContext;
using NewsPaper.Models;

namespace NewsPaper.Services
{
    public class CommentRepo : ICommentRepo
    {
        private readonly ModelContext _con;
        private readonly IMapper _mapper;
        private ArticleRepo _article;

        public CommentRepo(ModelContext con,IMapper mapper)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
            _mapper = mapper;
            ArticleRepo myArticle = _article;
        }

        public async Task<bool> addCommentsAsync(CommentDTO value)
        {
            var myArticle =await _con.Articles.FirstOrDefaultAsync(a=>a.ArticleId==value.ArticleId);
            if(myArticle == null)return false;
            var myUser = await _con.users.FirstOrDefaultAsync(a => a.UserId == value.UserId);
            if(myUser == null)return false;
            _con.Comments.Add(_mapper.Map<Comment>(value));
            _con.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCommentsAsync(int ArticleId, int CommentId)
        {
            var toRemove=await _con.Comments.FirstOrDefaultAsync(a=>a.Id==CommentId);
            if (toRemove != null)
            {
                _con.Comments.Remove(toRemove);
                return true;
            }
            else return false;
        }

        public async Task<List<CommentDTO>> GetCommentsAsync(int id)
        {
            List<CommentDTO> myComments = new();
            var comments =await _con.Comments.Where(a => a.ArticleId == id).ToListAsync();
            foreach(Comment _comment in comments)
            {
                    myComments.Add(_mapper.Map<CommentDTO> (_comment));
            }
            return myComments;
        }

        public async Task<bool> SaveChangesAsync()
        { 
            return (await _con.SaveChangesAsync() >= 0);
        }
    }
}
