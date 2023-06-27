using MFA.Services.DBService;
using MFA.Services.NavigationService;

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

    bool firstRun = true;
    protected override void OnAppearing()
    {
        var realm = RealmService.GetRealm();
        UserInfoViewModel.User = realm.All<User>().FirstOrDefault(x => x.Email == RealmService.CurrentUser.Profile.Email);
        viewModel.CurrentUser = UserInfoViewModel.User;
        
        if (UserInfoViewModel.User?.UsersImage != null && firstRun)
        {
            var test = UserInfoViewModel.User.UsersImage.Data;
            firstRun = false;
            AvatarImage.Source = ImageSource.FromStream(() => new MemoryStream(test));
        }
    }
}