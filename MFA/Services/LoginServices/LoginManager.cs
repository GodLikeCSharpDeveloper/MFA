using MFA.Services.NavigationService;
using MFA.Services.ValidateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LoginServices
{
    public class LoginManager : ILoginManager
    {
        
        INavigationRepository navigationRepository;
        IUserValidator userValidator;
        ILoginRepos loginRepository;
        public LoginManager(INavigationRepository navigationRepository, IUserValidator userValidator, ILoginRepos loginRepository)
        {
            this.navigationRepository = navigationRepository;
            this.userValidator = userValidator;
            this.loginRepository = loginRepository;
        }

        public async Task<User> LoggingUser(User user)
        {
            if (!userValidator.Validator(user.Email, user.Password))
                return null;
            await loginRepository.LoginAsync(user);
            await navigationRepository.NavigateTo("..");
            return user;
        }

    }
}
