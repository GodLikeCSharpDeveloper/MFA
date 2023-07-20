using MFA.Services.DBService;
using MFA.Services.NavigationService;
using MFA.Services.UserService;
using MFA.ViewModels;

namespace MFA.Views;

public partial class UserInfoPage : ContentPage
{
    UserInfoViewModel viewModel;
    INavigationRepository navigationRepository;
    
    public UserInfoPage(UserInfoViewModel viewModel, INavigationRepository navigationRepository)
    {
        BindingContext = viewModel;
        InitializeComponent();
        this.viewModel = viewModel;
        this.navigationRepository = navigationRepository;
    }
    
    protected override void OnAppearing()
    {
        var realm = RealmService.GetRealm();
        viewModel.CurrentUser = MainPageViewModel.User;
    }
}