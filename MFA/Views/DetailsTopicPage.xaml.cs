namespace MFA.Views;


public partial class DetailsTopicPage : ContentPage
{
    public Topic Topic { get; set; }
    TopicDetailViewModel viewModel;
    public DetailsTopicPage(TopicDetailViewModel viewModel)
    {
        this.viewModel = viewModel;
        BindingContext = viewModel;
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        viewModel.InitializeComments();
    }
}