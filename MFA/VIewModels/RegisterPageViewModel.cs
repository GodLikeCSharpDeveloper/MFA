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

namespace MFA.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        IRegisterManager registerManager;
        INavigationRepository navigationRepository;
        public RegisterPageViewModel(IRegisterManager registerManager, INavigationRepository navigationRepository)
        {
            this.registerManager = registerManager;
            this.navigationRepository = navigationRepository;
        }

        [ObservableProperty] 
        public User currentUserForRegister = new();

        [RelayCommand]
        public async Task RegisterHandler()
        {
            try
            {
               await registerManager.RegisterUserAsync(CurrentUserForRegister);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "OK");
                await navigationRepository.NavigateTo(nameof(RegisterPage));
            }
        }

    }
}
