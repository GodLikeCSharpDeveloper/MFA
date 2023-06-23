using Bogus.DataSets;
using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        IUserValidator validator;
        ILoginManager loginManager;
        INavigationRepository navigationRepository;

        public LoginPageViewModel(IUserValidator validator, ILoginManager loginManager, INavigationRepository navigationRepository)
        {
            this.validator = validator;
            this.loginManager = loginManager;
            this.navigationRepository = navigationRepository;
        }

        [ObservableProperty] 
        public User currentUserInfo = new();
        

        [RelayCommand]
        public async Task LoginHandler()
        {
            await RealmService.Init();
            await loginManager.LoggingUser(CurrentUserInfo);

        }
        [RelayCommand]
        public async Task MoveToRegister()
        {
            await navigationRepository.NavigateTo(nameof(RegisterPage));
        }
        [RelayCommand]
        public async Task AskAndExit()
        {
            //TODO FINISH IT
            await navigationRepository.NavigateTo(nameof(RegisterPage));
        }
    }
}
