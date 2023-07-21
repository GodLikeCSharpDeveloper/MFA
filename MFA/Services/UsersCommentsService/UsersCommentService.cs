using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.Services.NotificationService;
using MFA.Services.UserService;
using MFA.ViewModels;
using Realms.Sync;
using User = MFA.Models.User;

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

        public List<CommentWrapper> GetAllCurrentTopicComments(Topic topic)
        {
            var wrap = new List<CommentWrapper>();
            var realm = RealmService.GetRealm();
            var com = realm.All<UsersComment>().Where(x => x.Topic == topic).ToList();
            var likes = realm.All<UserLikes>().ToList();
            foreach (var item in com)
            {
                var a = new CommentWrapper(item);
                wrap.Add(a);
            }

            foreach (var item in wrap)
            {
                if (item.UsersComment.User != null)
                {
                    if (item.UsersComment.User._id == MainPageViewModel.User._id)
                        item.OwnedByUser = true;
                    foreach (var like in likes)
                    {
                        if (like.OwnerUser._id == MainPageViewModel.User._id && like.LikedComments._id == item.UsersComment._id)
                        {
                            item.LikeStatus = "icons8like24black.png";
                        }
                    }
                }
            }
            return wrap;
        }

        public async Task<UsersComment> GetCommentById(UsersComment comment)
        {
            var realm = RealmService.GetRealm();
            var commentForUpdate = realm.All<UsersComment>().FirstOrDefault(x=>x._id == comment._id);
            return commentForUpdate;
        }

        public async Task<UsersComment> UpdateCommentAsync(UsersComment comment)
        {
            var realm = RealmService.GetRealm();
            var commentForUpdate = GetCommentById(comment).Result;
            await realm.WriteAsync(() =>
            {
                commentForUpdate.Content = comment.Content;
            });
            if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
            {
                await realm.Subscriptions.WaitForSynchronizationAsync();
            }
            return commentForUpdate;
        }
    }
}
