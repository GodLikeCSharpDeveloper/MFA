namespace MFA.Views;

public partial class TopicAddOrRemove : ContentPage
{
	public TopicAddOrRemove(TopicAddOrRemoveViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();

	}
}