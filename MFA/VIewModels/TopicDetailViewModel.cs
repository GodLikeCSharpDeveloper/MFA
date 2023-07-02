using MFA.Services.DBService;
using MFA.Services.UsersCommentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.ViewModels
{
    [QueryProperty(nameof(Topic), "Topic")]
    public partial class TopicDetailViewModel : BaseViewModel
    {
        IUsersCommentService usersCommentService;
        public TopicDetailViewModel(IUsersCommentService usersCommentService)
        {
            this.usersCommentService = usersCommentService;
            InitializeComments();
        }
        [ObservableProperty]
        Topic topic;

        [ObservableProperty]
        List<UsersComment> usersComments;

        [RelayCommand]
        public void AddNewComment()
        {
            var newComment = new UsersComment()
            {
                Content = usersComment.Content,
                CreationDate = DateTime.Now.ToString(),
                Topic = topic,
            };
            UsersComments.Add(newComment);
        }

        public void InitializeComments()
        {
            UsersComments = usersCommentService.GetAllCurrentTopicComments(topic);
        }
    }
}
