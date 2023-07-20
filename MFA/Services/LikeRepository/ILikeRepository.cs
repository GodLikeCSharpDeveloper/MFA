using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LikeRepository
{
    public interface ILikeRepository
    {
        public async Task<List<UserLikes>> GetUserLikesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserLikes> AddNewLike(UsersComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLikes> AddNewLike(Topic topic)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLike(UsersComment comment)
        {
            throw new NotImplementedException();
        }
        
    }
}
