using AutoMapper;
using NewsPaper.Models;


namespace NewsPaper.Profiles
{
    public class ArticleProfile: Profile 
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
        }
           }
}
