using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.UserService;
using Microsoft.Maui.Controls;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    INavigationRepository navigationRepository;

    ActivityIndicator loadingIndicator;
    IUserDbService userDbService;
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository, IUserDbService userDbService)
    {


        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        this.userDbService = userDbService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (RealmService.app.CurrentUser == null)
        {
            await navigationRepository.NavigateTo(nameof(LoginPage));
        }

        if (MainPageViewModel.User == null && RealmService.app.CurrentUser != null)
            MainPageViewModel.User = userDbService.GetUserByEmail(RealmService.CurrentUser.Profile.Email);
    }
}