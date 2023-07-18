using MFA.Services.NavigationService;
using MFA.Services.UsersCommentsService;
using MFA.Utility.Converter;


namespace MFA.Views;


public partial class DetailsTopicPage : ContentPage
{
    TopicDetailViewModel viewModel;
    IUsersCommentService usersCommentService;
    INavigationRepository navigationRepository;
    public DetailsTopicPage(TopicDetailViewModel viewModel, IUsersCommentService usersCommentService, INavigationRepository navigationRepository)
    {
        this.viewModel = viewModel;
        BindingContext = viewModel;
        this.usersCommentService = usersCommentService;
        InitializeComponent();
        this.navigationRepository = navigationRepository;
        
    }

   

    protected override void OnAppearing()
    {
        while (viewModel.Topic == null)
        {
            Thread.Sleep(100);
        }
        viewModel.AnimateImg = true;
        Thread.Sleep(100);

        TopicDetailViewModel._usersComments = new(usersCommentService.GetAllCurrentTopicComments(viewModel.Topic).ToList());
        Thread.Sleep(100);
        viewModel.UsersComments = new(TopicDetailViewModel._usersComments.Take(20));
        viewModel.commentsCount = 30;

    }
}