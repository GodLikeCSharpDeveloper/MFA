using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.TopicService;
using MFA.Services.UserService;
using MFA.Utility.UiHelper;
using MFA.ViewModels;
using Microsoft.Maui.Controls;
using Realms;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    INavigationRepository navigationRepository;
    ActivityIndicator loadingIndicator;
    IUserDbService userDbService;
    ITopicDBService topicDBService;
    IDbChecker dbChecker;
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository, IUserDbService userDbService, IDbChecker dbChecker)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        this.userDbService = userDbService;
        this.dbChecker = dbChecker;
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
        dbChecker.Check();
    }
}