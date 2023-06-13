using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

namespace MFA.Services.RegisterServices
{
    public class RealmRegisterRepos : IRegister
    {
        public async Task RegisterUser(User user)
        {
            await RealmService.app.EmailPasswordAuth.RegisterUserAsync(user.Email, user.Password);
            await RealmService.app.LogInAsync(Realms.Sync.Credentials.EmailPassword(user.Email, user.Password));
            var realm = RealmService.GetMainThreadRealm();
            await realm.WriteAsync(() =>
            {
                realm.Add(new User
                {
                    OwnerId = RealmService.app.CurrentUser.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                });
            });
        }
    }
}

