using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.Services.NotificationService;
using MFA.Services.UserService;

namespace MFA.Services.UsersCommentsService
{
    public class UsersCommentService : IUsersCommentService
    {
        INotificationService notificationService;
        public UsersCommentService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public async Task AddNewComment(UsersComment usersComment)
        {
            try
            {
                var realm = RealmService.GetRealm();
                var user = realm.All<User>().FirstOrDefault(x => x.Email == MainPageViewModel.User.Email);
                var topic = realm.All<Topic>().FirstOrDefault(x => x._id == usersComment.Topic._id);
                var comment = new UsersComment
                {
                    Content = usersComment.Content,
                    CreationDate = DateTime.Now.ToString(),
                    Topic = topic,
                    User = user,
                };
                await realm.WriteAsync(() => { realm.Add(comment); });
            }
            catch (Exception ex)
            {
                await notificationService.Notify(ex.Message);
            }
        }

        public List<UsersComment> GetAllCurrentTopicComments(Topic topic)
        {
            var realm = RealmService.GetRealm();
            var com = realm.All<UsersComment>().Where(x => x.Topic == topic).ToList();
            var likes = realm.All<UserLikes>().ToList();
            foreach (var item in com)
            {
                foreach (var like in likes)
                {
                    if (like.OwnerUser._id == MainPageViewModel.User._id && like.LikedComments._id == item._id)
                        item.LikeStatus = "icons8like24black.png";
                }
            }
            return com;
        }
    }
}
