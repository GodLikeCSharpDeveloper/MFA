using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.UserService;
using Microsoft.Maui.Controls;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    INavigationRepository navigationRepository;
    IUserLogOut userLogOut;
    ActivityIndicator loadingIndicator;
    IUserDbService userDbService;
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository, IUserLogOut userLogOut, IUserDbService userDbService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        this.userLogOut = userLogOut;
        this.userDbService = userDbService;
    }

    protected override async void OnAppearing()
    {
        
        await RealmService.Init();
        if (RealmService.app.CurrentUser == null)
        {
            await navigationRepository.NavigateTo(nameof(LoginPage));
        }
        if (MainPageViewModel.User == null&& RealmService.app.CurrentUser != null)
            MainPageViewModel.User = userDbService.GetUserByEmail(RealmService.CurrentUser.Profile.Email);


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
    
    private void Button_Clicked(object sender, EventArgs e)
    {
        IsBusy = !IsBusy;
    }
}