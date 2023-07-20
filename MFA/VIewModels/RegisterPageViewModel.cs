using MFA.Services.NavigationService;
using MFA.Services.NotificationService;
using MFA.Services.RegisterServices;
using MFA.Utility.ImageManager;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        IRegisterManager registerManager;
        INavigationRepository navigationRepository;
        INotificationService notificationService;

        public RegisterPageViewModel(IRegisterManager registerManager, INavigationRepository navigationRepository, INotificationService notificationService)
        {
            this.registerManager = registerManager;
            this.navigationRepository = navigationRepository;
            this.notificationService = notificationService;
        }

        [ObservableProperty]
        public User currentUserForRegister = new User()
        {

        };



        [RelayCommand]
        public async Task RegisterHandler()
        {
            try
            {
                CurrentUserForRegister.UsersImage = new ImageData { Data = ImageManager.ReadTextFile().Result };
                await registerManager.RegisterUserAsync(CurrentUserForRegister);
                CurrentUserForRegister = new();
            }
            catch (Exception ex)
            {
                await notificationService.Notify(ex.ToString());
                await navigationRepository.NavigateTo(nameof(RegisterPage));
            }
        }

    }
}
