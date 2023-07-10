using MFA.Services.UsersCommentsService;

namespace MFA.Views;


public partial class DetailsTopicPage : ContentPage
{
    public Topic Topic { get; set; }
    TopicDetailViewModel viewModel;
    IUsersCommentService usersCommentService;
    public DetailsTopicPage(TopicDetailViewModel viewModel, IUsersCommentService usersCommentService)
    {
        this.viewModel = viewModel;
        BindingContext = viewModel;
        this.usersCommentService = usersCommentService;
        InitializeComponent();
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
}