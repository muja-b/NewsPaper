using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsPaper.DpContext;
using NewsPaper.Models;

namespace NewsPaper.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly ModelContext _con;
        private readonly IMapper _mapper;

        public UserRepo(ModelContext con, IMapper mapper)
        {
            _con = con ?? throw new ArgumentNullException(nameof(con));
            _mapper = mapper;
        }

        public async Task addUsersAsync(UserDTO value)
        {
        var myUser=_mapper.Map<User>(value);
            _con.users.Add(myUser);   
        }

        public async Task<UserDTO?> GetUserAsync(int id)
        {
            var user = await _con.users.FirstOrDefaultAsync(a => a.UserId == id);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<User> GetUserAsync(string name)
        {
            var user = await _con.users.FirstOrDefaultAsync(a => a.Name.Equals(name));
            return user;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _con.users.ToListAsync();
            var usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                usersDTO.Add(_mapper.Map<UserDTO>(user));
            }
            return usersDTO;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _con.SaveChangesAsync() >= 0);
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _con.users.AnyAsync(a => a.Name.Equals(userName));
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
             var toRemove=await GetUserAsync(id);
            if (toRemove != null)
            {
                var myUser = await _con.users.FirstOrDefaultAsync(u => u.UserId==id);
                _con.users.Remove(myUser);
                return true;
            }
            else return false;

        }
        public async Task<bool> UpdateUserAsync(UserDTO value)
        {
            var toUpdate =await  _con.users.FirstOrDefaultAsync(a => a.UserId == value.UserId);
            if(toUpdate == null) return false;
            toUpdate.Email = value.Email;
            toUpdate.Name = value.Name;
            toUpdate.Password = value.Password;
            return true;    
        }
        public async Task addFollower(int followerId,int followedId)
        {
            var FollowedUser = await _con.users.Include(a=>a.Followed).FirstOrDefaultAsync(a=>a.UserId==followedId);
            var FollowerUser = await _con.users.Include(a=>a.Following).FirstOrDefaultAsync(a=>a.UserId==followerId);
            if (FollowerUser.Following == null)
            {
                FollowerUser.Following = new();
            }
            if (FollowedUser.Following == null)
            {
                FollowedUser.Followed = new();
            }
            FollowerUser.Following.Add(FollowedUser);
            FollowedUser.Followed.Add(FollowerUser);
            return;
        }
        public async Task<List<UserDTO>>GetFollowers(int id)
        {
        var FollowedUser =await _con.users.Include(a=>a.Followed).FirstOrDefaultAsync(a=>a.UserId==id);
        List<UserDTO>users=new();
        if(FollowedUser == null)return users;
            var followed=FollowedUser.Followed;
        foreach (User user in followed) 
            {
                
                users.Add(new UserDTO
                {
                    UserId=user.UserId,
                    Email=user.Email,
                    Name=user.Name,
                });
             }
        return users;
        }
        public async Task<List<UserDTO>>GetFollowing(int id)
        {
        var FollowingUser =await _con.users.Include(a=>a.Following).FirstOrDefaultAsync(a=>a.UserId==id);
        List<UserDTO>users=new();
        if(FollowingUser == null)return users;
            var followed=FollowingUser.Following;
        foreach (User user in followed) 
            {
                
                users.Add(new UserDTO
                {
                    UserId=user.UserId,
                    Email=user.Email,
                    Name=user.Name,
                });
             }
        return users;
        }
        public async Task<List<ArticleDTO>>GetFavArticles(int id)
        {
        var User =await _con.users.Include(a=>a.FavArcticles).FirstOrDefaultAsync(a=>a.UserId==id);
        List<ArticleDTO>articles=new();
        if(User == null)return articles;
            var fav=User.FavArcticles;
        foreach (Article art in fav) 
            {
                
                articles.Add(_mapper.Map<ArticleDTO>(art));
             }
        return articles;
        }
        public async Task addFavArticles(int UserId,int ArticleId)
        {
            var User = await _con.users.Include(a=>a.FavArcticles).FirstOrDefaultAsync(a=>a.UserId==UserId);
            var Article = await _con.Articles.FirstOrDefaultAsync(a=>a.ArticleId==ArticleId);
            if (User.FavArcticles == null)
            {
                User.FavArcticles =new();
            }
            User.FavArcticles.Add(Article);
            return;
        }

               
    }
}
