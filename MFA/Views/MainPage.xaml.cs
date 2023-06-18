using MFA.Services.DBService;

namespace MFA.Views;

public partial class MainPage : ContentPage
{
    bool IsAuth;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
    }
    protected override async void OnAppearing()
    {
        await RealmService.Init();
        if (RealmService.app.CurrentUser==null)
            await Shell.Current.GoToAsync(nameof(LoginPage), true);

        IsAuth = !IsAuth;
    }

    private async void LogOut(object sender, EventArgs e)
    {
       await RealmService.app.CurrentUser.LogOutAsync();
       await Shell.Current.GoToAsync(nameof(LoginPage), true);
    }

    private async void GoToTopicAdd(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("TopicAddOrRemove", true);
    }

    private async void GoToUserInfo(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserInfoPage", true);
    }
}