namespace MFA.Views;


public partial class DetailsTopicPage : ContentPage
{
    public Topic Topic { get; set; }
    public DetailsTopicPage(TopicDetailViewModel viewModel)
    {
        BindingContext = viewModel;
		InitializeComponent();
	}
}