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
        if (UserInfoViewModel.User?.UsersImage != null && firstRun)
        {
            firstRun = false;
            AvatarImage.Source = ImageSource.FromStream(() => new MemoryStream(UserInfoViewModel.User.UsersImage.Data));
        }
        viewModel.CurrentUser = UserInfoViewModel.User;
    }
}