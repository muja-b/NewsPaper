using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsPaper.DpContext;
using NewsPaper.Models;
namespace NewsPaper.Services
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly ModelContext _con;
        private readonly IMapper _mapper;

        public ArticleRepo(ModelContext con,IMapper mapper)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
            _mapper = mapper;
        }

        public async Task<bool> ArticleExists(ArticleDTO article)
        {
            return await _con.Articles.AnyAsync(a => a.Title.Equals(article.Title));
        }


        public async Task<ArticleDTO?> GetArticle(int id)
        {
            var article=await _con.Articles.FirstOrDefaultAsync(a=>a.ArticleId==id);
            if (article==null)return null;
            var Writer = await _con.users.FirstOrDefaultAsync(user => user.UserId == article.UserId);
            Writer.Name = article.Writer;
            return _mapper.Map<ArticleDTO>(article);                
        }
        

        public async Task<IEnumerable<ArticleDTO>> GetArticles()
        {
            var Articles =await _con.Articles.ToListAsync();
            var ArtDTO =new List<ArticleDTO>();
            foreach (var article in Articles)
            {
                
                var Writer=await _con.users.FirstAsync(user => user.UserId == article.UserId);
                article.Writer = Writer.Name;
                ArtDTO.Add(_mapper.Map<ArticleDTO>(article));

            }

            return ArtDTO;                 
        }

        public async Task addArticleAsync(ArticleDTO value)
        {
            var Writer=await _con.users.FirstOrDefaultAsync(u => u.Name.Equals(value.Writer));
            var myArticle=new Article (){
            Content=value.Content,
            Title=value.Title,
            Writer=value.Writer,
            UserId=Writer.UserId
            };
            _con.Articles.Add(myArticle);    
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _con.SaveChangesAsync() >= 0);
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var toRemove=await GetArticle(id);
            if (toRemove != null)
            {
                var article = await _con.Articles.FirstOrDefaultAsync(u => u.ArticleId==id);
                _con.Articles.Remove(article);
                return true;
            }
            else return false;

        }

        public async Task UpdateArticle(ArticleDTO value)
        {
            var toUpdate =await  _con.Articles.FirstAsync(a => a.ArticleId == value.ArticleId);
            toUpdate.Content = value.Content;
            toUpdate.Title = value.Title;
            toUpdate.Writer = value.Writer;
            
        }

        }
    }

