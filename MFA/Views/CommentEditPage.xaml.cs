using MFA.ViewModels;

namespace MFA.Views;

public partial class CommentEditPage : ContentPage
{
	public CommentEditPage(CommentEditViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}