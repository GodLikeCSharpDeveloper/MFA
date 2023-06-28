using Bogus.DataSets;
using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.NotificationService;
using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        ILoginManager loginManager;
        INavigationRepository navigationRepository;
        INotificationService notificationService;
        public LoginPageViewModel(IUserValidator validator, ILoginManager loginManager, INavigationRepository navigationRepository, INotificationService notificationService)
        {
            this.loginManager = loginManager;
            this.navigationRepository = navigationRepository;
            this.notificationService = notificationService;
        }

        [ObservableProperty]
        public User currentUserInfo = new();


        [RelayCommand]
        public async Task LoginHandler()
        {
            IsBusy = true;
            await RealmService.Init();
            try
            {
                await loginManager.LoggingUser(CurrentUserInfo);
            }
            catch (Exception ex)
            {
                await notificationService.Notify(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
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
