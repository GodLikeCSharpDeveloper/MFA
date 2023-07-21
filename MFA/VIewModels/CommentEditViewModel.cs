using MFA.Services.NavigationService;
using MFA.Services.UsersCommentsService;
using MFA.Views;

namespace MFA.ViewModels
{
    [QueryProperty("Comment", "Comment")]
    public partial class CommentEditViewModel : BaseViewModel
    {
        [ObservableProperty]
        private UsersComment comment;

        IUsersCommentService usersCommentService;
        private INavigationRepository navigationRepository;
        public CommentEditViewModel(IUsersCommentService usersCommentService, INavigationRepository navigationRepository)
        {
            this.usersCommentService = usersCommentService;
            this.navigationRepository = navigationRepository;

        }

        [RelayCommand]
        async Task ConfirmEdit()
        {
            Comment = await usersCommentService.UpdateCommentAsync(Comment);
            await navigationRepository.NavigateTo("..");
            await navigationRepository.NavigateTo(nameof(DetailsTopicPage), new Dictionary<string, object>()
            {
                {"Topic", comment.Topic}
            });
        }

    }
}
