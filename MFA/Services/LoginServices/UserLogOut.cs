﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

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
        await RealmService.app.CurrentUser.LogOutAsync();
        RealmService.MainThreadRealm?.Dispose();
        UserInfoViewModel.User = null;
        viewModel.AvatarData = null;
        RealmService.MainThreadRealm = null;
    }
    }
}
