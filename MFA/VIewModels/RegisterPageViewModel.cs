using MFA.Services.NavigationService;
using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Views;
using MFA.Utility.ImageManager;
using MFA.Services.NotificationService;

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
            }
            catch (Exception ex)
            {
                await notificationService.Notify(ex.ToString());
                await navigationRepository.NavigateTo(nameof(RegisterPage));
            }
        }

    }
}
