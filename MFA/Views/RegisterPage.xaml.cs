namespace MFA.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}