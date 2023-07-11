using MFA.Services.NavigationService;
using MFA.Services.UsersCommentsService;

namespace MFA.Views;


public partial class DetailsTopicPage : ContentPage
{
    public Topic Topic { get; set; }
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
        viewModel._usersComments = new(usersCommentService.GetAllCurrentTopicComments(viewModel.Topic).ToList());
        viewModel.UsersComments = new(viewModel._usersComments.Take(20));
    }

    protected override bool OnBackButtonPressed()
    {
        Device.BeginInvokeOnMainThread(async () =>
        {
            await navigationRepository.WaitingNavigateTo("..", true);
            viewModel.UsersComments.Clear();
        });
        return true;
    }
}