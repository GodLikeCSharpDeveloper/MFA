using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

namespace MFA.Services.LoginServices
{
    public class UserLogOut
    {
       
       
        public static async Task LogoutAsync()
        {
            await RealmService.app.CurrentUser.LogOutAsync();
            RealmService.MainThreadRealm?.Dispose();
            UserInfoViewModel.user = null;
            UserInfoViewModel viewModel = new();
            viewModel.avatarData = null;
            viewModel.AvatarData = null;
            RealmService.MainThreadRealm = null;
        }
    }
}
