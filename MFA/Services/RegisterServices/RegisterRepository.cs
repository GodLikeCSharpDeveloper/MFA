using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using MFA.Services.UserService;

namespace MFA.Services.RegisterServices
{
    public class RegisterRepository : IRegisterRepository
    {
        IUserDbService userDbService;
        public RegisterRepository(IUserDbService userDbService)
        {
            this.userDbService = userDbService;
        }
        public async Task RegisterUser(User user)
        {
            await RealmService.app.EmailPasswordAuth.RegisterUserAsync(user.Email, user.Password);
            await RealmService.app.LogInAsync(Realms.Sync.Credentials.EmailPassword(user.Email, user.Password));
            await userDbService.AddUser(user);
        }
    }
}

