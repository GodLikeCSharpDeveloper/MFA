using MFA.Services.NavigationService;
using MFA.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class UserEditViewModel : BaseViewModel
    {
        INavigationRepository navigationRepository;
        IUserDbService userDbService;

        [ObservableProperty]
        User userForUpdate = new();
        public UserEditViewModel(IUserDbService userDbService, INavigationRepository navigationRepository)
        {
            this.userDbService = userDbService;
            this.navigationRepository = navigationRepository;
        }
        [RelayCommand]
        public async Task CurrentUserUpdate()
        {
            await userDbService.UpdateUser(UserInfoViewModel.User, userForUpdate);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
