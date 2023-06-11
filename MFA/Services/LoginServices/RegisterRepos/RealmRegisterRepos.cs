using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;

namespace MFA.Services.LoginServices.RegisterRepos
{
    public class RealmRegisterRepos : IRegister
    {
        public static async Task RegisterUser(User user)
        {
            var realm = RealmService.GetMainThreadRealm();
            await realm.WriteAsync(() =>
            {
                realm.Add(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                });
            });
            await Shell.Current.GoToAsync("..");
            await RealmService.app.EmailPasswordAuth.RegisterUserAsync(user.Email, user.Password);
        }
    }
}

