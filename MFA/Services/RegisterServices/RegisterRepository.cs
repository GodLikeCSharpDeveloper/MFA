using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

namespace MFA.Services.RegisterServices
{
    public class RegisterRepository : IRegisterRepository
    {
        public async Task RegisterUser(User user)
        {
            await RealmService.app.EmailPasswordAuth.RegisterUserAsync(user.Email, user.Password);
            await RealmService.app.LogInAsync(Realms.Sync.Credentials.EmailPassword(user.Email, user.Password));
            var realm = RealmService.GetRealm();
            await realm.WriteAsync(() =>
            {
                realm.Add(user);
            });
        }
    }
}

