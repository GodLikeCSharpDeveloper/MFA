using MFA.Services.DBService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LikeRepository
{
    public class LikeRepository : ILikeRepository
    {
        public LikeRepository()
        {
            
        }

        public async Task<List<UserLikes>> GetUserLikesAsync()
        {
            var realm = RealmService.GetRealm();
            var likes = realm.All<UserLikes>().Where(x=>x.OwnerUser._id==MainPageViewModel.User._id).ToList();
            return likes;
        }

        public async Task<UserLikes> AddNewLike(UsersComment comment)
        {
            var like = new UserLikes()
            {
                OwnerUser = MainPageViewModel.User,
                LikedComments = comment,
            };
            var realm = RealmService.GetRealm() ;
            await realm.WriteAsync(() =>
            {
                realm.Add(like);
            });
            return like;
        }
        public async Task<UserLikes> AddNewLike(Topic topic)
        {
            var like = new UserLikes()
            {
                OwnerUser = MainPageViewModel.User,
                LikedTopics = topic,
            };
            var realm = RealmService.GetRealm();
            await realm.WriteAsync(() =>
            {
                realm.Add(like);
            });
            return like;
        }
    }
}
