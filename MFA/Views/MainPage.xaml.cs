using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using Microsoft.Maui.Controls;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    INavigationRepository navigationRepository;
    IUserLogOut userLogOut;
    ActivityIndicator loadingIndicator;
    public static StackLayout MainLayout { get; private set; } = new();
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository, IUserLogOut userLogOut)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        this.userLogOut = userLogOut;
    }

    protected override async void OnAppearing()
    {
       
        await RealmService.Init();
        if (RealmService.app.CurrentUser==null)
            await navigationRepository.NavigateTo(nameof(LoginPage));
       
    }

    private async void LogOut(object sender, EventArgs e)
    {
       await userLogOut.LogoutAsync();
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