namespace MFA.Views;

public partial class UserInfoPage : ContentPage
{
	public UserInfoPage(UserInfoViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        AvatarImage.Source = ImageSource.FromStream(() =>
        {
            if (UserInfoViewModel.user?.Image != null)
                return new MemoryStream(UserInfoViewModel.user.Image.Data);
            return null;
        });
    }
}