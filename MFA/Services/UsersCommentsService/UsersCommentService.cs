using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.Services.NotificationService;

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
                var comment = new UsersComment
                {
                    Content = usersComment.Content,
                    CreationDate = DateTime.Now.ToString(),
                    Topic = usersComment.Topic,
                    User = MainPageViewModel.User,

                };
                var realm = RealmService.GetRealm();
                realm.Write(() => { realm.Add(comment); });
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
            return com;
        }
    }
}
