using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.ViewModels;
using MFA.Views;

namespace MFA.Services.LoginServices
{
    public class UserLogOut : IUserLogOut
    {
        UserInfoViewModel viewModel;

        public UserLogOut(UserInfoViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public async Task LogoutAsync()
        {
            await RealmService.app.RemoveUserAsync(RealmService.CurrentUser);
            RealmService.MainThreadRealm?.Dispose();
            MainPageViewModel.User = null;
            viewModel.AvatarData = null;
            RealmService.MainThreadRealm = null;
        }
    }
}
