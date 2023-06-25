using MFA.Services.NavigationService;

namespace MFA.Views;

public partial class UserEditPage : ContentPage
{
    INavigationRepository navigationRepository;

    public UserEditPage(UserEditViewModel viewModel, INavigationRepository navigationRepository)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
	}
    
    protected override async void OnDisappearing()
    {
        //await navigationRepository.WaitingNavigateTo("..", false);
        //await navigationRepository.WaitingNavigateTo(nameof(UserInfoPage), false);
    }
}