using MFA.Services.DBService;
using MFA.Services.NavigationService;
using MFA.Services.UserService;

namespace MFA.Views;

public partial class UserInfoPage : ContentPage
{
    UserInfoViewModel viewModel;
    INavigationRepository navigationRepository;
    IUserDbService userDbService;
    public UserInfoPage(UserInfoViewModel viewModel, INavigationRepository navigationRepository, IUserDbService userDbService)
    {
        BindingContext = viewModel;
        InitializeComponent();
        this.viewModel = viewModel;
        this.navigationRepository = navigationRepository;
        this.userDbService = userDbService;
    }

   
    protected override void OnAppearing()
    {
        var realm = RealmService.GetRealm();
        UserInfoViewModel.User = userDbService.GetUserByEmail(RealmService.CurrentUser.Profile.Email);
        viewModel.CurrentUser = UserInfoViewModel.User;
        
        if (UserInfoViewModel.User?.UsersImage != null)
        {
            var test = UserInfoViewModel.User.UsersImage.Data;
            AvatarImage.Source = ImageSource.FromStream(() => new MemoryStream(test));
        }
    }
}