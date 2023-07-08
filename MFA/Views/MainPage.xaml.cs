using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.UserService;
using Microsoft.Maui.Controls;
using Realms;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    INavigationRepository navigationRepository;

    ActivityIndicator loadingIndicator;
    IUserDbService userDbService;
    ITopicDBService topicDBService;
    MainPageViewModel viewModel;
    public MainPage(MainPageViewModel viewModel, INavigationRepository navigationRepository, IUserDbService userDbService, ITopicDBService topicDBService)
    {


        InitializeComponent();
        BindingContext = viewModel;
        this.navigationRepository = navigationRepository;
        this.userDbService = userDbService;
        this.topicDBService = topicDBService;
        this.viewModel = viewModel;
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
        async Task Check()
        {
            if (viewModel.Topics!=null&&topicDBService.GetAllTopics().Count == 0 && viewModel.Topics.Count > 0)
            { 
                viewModel.Topics.Clear(); 
                viewModel.IsBusy = true;
            }
            while (topicDBService.GetAllTopics().Count == 0)
            {
                Thread.Sleep(100);
            }
            viewModel.IsBusy = false;
            viewModel.Topics = new(topicDBService.GetAllTopics().Take(30));
        }

        await Task.Factory.StartNew(Check).WaitAsync(new CancellationToken());
        viewModel.Topics = new(topicDBService.GetAllTopics().Take(30));
    }
}