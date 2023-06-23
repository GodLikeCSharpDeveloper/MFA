using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    private INavigationRepository navigationRepository;
    bool IsAuth;
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        
    }
    protected override async void OnAppearing()
    {
        await RealmService.Init();
        if (RealmService.app.CurrentUser==null)
            await navigationRepository.NavigateTo(nameof(LoginPage));
        IsAuth = !IsAuth;
    }

    private async void LogOut(object sender, EventArgs e)
    {
       await UserLogOut.LogoutAsync();
       await navigationRepository.NavigateTo(nameof(LoginPage));
    }

    private async void GoToTopicAdd(object sender, EventArgs e)
    {
        await navigationRepository.NavigateTo(nameof(TopicAddOrRemove));
    }

    private async void GoToUserInfo(object sender, EventArgs e)
    {
        await navigationRepository.NavigateTo(nameof(UserInfoPage));
    }
}