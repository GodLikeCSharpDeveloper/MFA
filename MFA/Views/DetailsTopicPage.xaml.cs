using MFA.Services.NavigationService;
using MFA.Services.UsersCommentsService;
using MFA.Utility.Converter;


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

        TopicDetailViewModel._usersComments = new(usersCommentService.GetAllCurrentTopicComments(viewModel.Topic).ToList());
        viewModel.UsersComments = new(TopicDetailViewModel._usersComments.Take(20));
        viewModel.Wrapper = new();
        foreach (var item in viewModel.UsersComments)
            viewModel.Wrapper.Add(new WrappedComments(item));
        var check = viewModel.Wrapper;
    }

    protected override bool OnBackButtonPressed()
    {
        viewModel.Wrapper = null;
        Task.Factory.StartNew(async () => await navigationRepository.WaitingNavigateTo("..", true));
        return true;
    }
}